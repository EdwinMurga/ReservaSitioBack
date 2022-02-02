using Microsoft.Extensions.Configuration;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ReservaSitio.Abstraction.IApplication.Util;
using ReservaSitio.Abstraction.IService.LogError;
using ReservaSitio.Abstraction.IService.ParametrosAplicacion;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
using ReservaSitio.DTOs.Usuario;
using ReservaSitio.DTOs.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Application.Util
{
    public  class UtilAplication : IUtilAplication
    {
        private smtpDTO smtp = new smtpDTO(); 
        private readonly IConfiguration iIConfiguration;
        private readonly IPlantillaCorreoServices iIPlantillaCorreoServices;
        private readonly ILogErrorServices iILogErrorServices;
        public UtilAplication(IConfiguration IConfiguration
            , IPlantillaCorreoServices IPlantillaCorreoServices
            , ILogErrorServices ILogErrorServices) {

            this.iILogErrorServices = ILogErrorServices;
            this.iIPlantillaCorreoServices = IPlantillaCorreoServices;
            this.iIConfiguration = IConfiguration;

           
            smtp.CredencialesPorDefecto = Convert.ToBoolean(this.iIConfiguration["CredencialesSTMPPorDefecto"].ToString());
            smtp.Puerto = Convert.ToInt32( this.iIConfiguration["PuertoSMTP"].ToString() );
            smtp.Servidor = this.iIConfiguration["ServidorSMTP"].ToString();
            smtp.Usuario = this.iIConfiguration["ServidorSMTPUsuario"].ToString();
            smtp.Password = this.iIConfiguration["ServidorSMTPPass"].ToString();
        }

        public  async Task<ResultDTO<bool>> envioMailPlantilla(int idplantilla,int[] idusuario)
        {
            ResultDTO<bool> res = new ResultDTO<bool>();

            EmailDTO email = new EmailDTO();
            PlantillaCorreoDTO plantilla =new PlantillaCorreoDTO();
           // plantilla.iid_empresa =
             plantilla.iid_plantilla_correo = idplantilla;

            ResultDTO< PlantillaCorreoDTO > resplantilla = await this.iIPlantillaCorreoServices.GetPlantillaCorreo(plantilla);

            email.Mensaje = resplantilla.item.vcuerpo_correo;
            email.Titulo = resplantilla.item.vtitulo_correo;


            res =  await envioMail(email);


            return res;

        }

        public  async Task<ResultDTO<bool>> envioMail(EmailDTO email)
        {
            ResultDTO<bool> res =new ResultDTO<bool>();

            email.De = new System.Net.Mail.MailAddress(smtp.Usuario);            

            try
            {
                SmtpClient client = new SmtpClient();

                client.Host = smtp.Servidor;
                client.Port = smtp.Puerto;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;



                if (smtp.CredencialesPorDefecto) 
                {

                    client.UseDefaultCredentials = true;
                    client.EnableSsl = true;
                }
                else
                {
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential(smtp.Usuario, smtp.Password);
                }
             
                MailMessage mensajeEmail = new MailMessage();
                mensajeEmail.From = email.De;
                string destinatariosPara = "";
                if (email.Para != null)
                    foreach (string correo in email.Para)
                    {
                       
                            mensajeEmail.To.Add(new MailAddress(correo));
                            destinatariosPara = destinatariosPara + correo + ";";
                       
                    }
                string destinatariosConCopia = "";
                if (email.ConCopia != null)
                    foreach (string correo in email.ConCopia)
                    {
                       
                            mensajeEmail.CC.Add(new MailAddress(correo));
                            destinatariosConCopia = destinatariosConCopia + correo + ";";
                       
                    }
                string destinatariosConCopiaOculta = "";
                if (email.ConCopiaOculta != null)
                    foreach (string correo in email.ConCopiaOculta)
                    {
                      
                            mensajeEmail.Bcc.Add(new MailAddress(correo));
                            destinatariosConCopiaOculta = destinatariosConCopiaOculta + correo + ";";
                         
                    }

                if (string.IsNullOrEmpty(destinatariosPara) && string.IsNullOrEmpty(destinatariosConCopia) && string.IsNullOrEmpty(destinatariosConCopiaOculta))
                    throw new Exception("Ninguno de los correos especificados es válido");

                mensajeEmail.IsBodyHtml = true;
                mensajeEmail.Subject = email.Titulo;
                mensajeEmail.Body = email.Mensaje;

                List<string> detalleAdjuntos = new List<string>();
                if (email.Adjuntos != null)
                    foreach (string adjunto in (email.Adjuntos))
                    {
                        if (File.Exists(adjunto))
                        {
                            mensajeEmail.Attachments.Add(new Attachment(adjunto));
                            detalleAdjuntos.Add(adjunto + ": Adjuntado con éxito");
                        }
                        else
                            throw new Exception(adjunto + ": El archivo no existe o es inaccesible");
                        //detalleAdjuntos.Add(adjunto + ": El archivo no existe o es inaccesible");
                    }
                client.Send(mensajeEmail);

                res.IsSuccess = true;
                res.Message = "Mensaje Enviado";
              
            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = "Mensaje no Enviado";
                res.MessageExeption = e.Message.ToString();

                LogErrorDTO lg = new LogErrorDTO();
                lg.iid_usuario_registra = email.iid_usuario_registra;
                lg.iid_opcion = 1;
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                await this.iILogErrorServices.RegisterLogError(lg);
            }

            return res;
        }


        public MemoryStream CreateExcel<T>(List<T> model, String nombreReporte, string param)
        {
            string fileName = nombreReporte + ".xlsx";
            string modelplantilla = "";
            string nomhoja = "";
            int nrowsIni = 0;
            Int32 nrows = 0;
            if (nombreReporte == "ValidacionMaterial")
            {
                modelplantilla = iIConfiguration["ModelExcelPlantillaCotizacion"];
                nomhoja = "ValidacionMaterial";
                nrows = 0;
                nrowsIni = 0;
            }
            else
            {
                modelplantilla = iIConfiguration["ModelExcelReporte"];
                nomhoja = "Reporte";
                nrows = 7;
                nrowsIni = 1;
            }


            string url = Path.Combine(Environment.CurrentDirectory + "\\" + iIConfiguration["UploadFileTemp"], modelplantilla);

            var memoryStream = new MemoryStream();
            FileStream fs;

            HSSFWorkbook hssfwb;
            using (FileStream file = new FileStream(url, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new HSSFWorkbook(file);
            }

            //using ( fs = new FileStream(url, FileMode.OpenOrCreate, FileAccess.Write))
            //  {
            //IWorkbook workbook = new XSSFWorkbook();
            //ISheet excelSheet = workbook.CreateSheet(nombreHoja);
            // ISheet excelSheet = hssfwb.CreateSheet("Reporte");


            ISheet excelSheet = hssfwb.GetSheet(nomhoja);
            IRow row = null;


            var font = hssfwb.CreateFont();
            font.FontHeightInPoints = 11;
            font.FontName = "Calibri";
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            //titulo
            ICellStyle tStyle = hssfwb.CreateCellStyle();
            tStyle.SetFont(font);


            //cabecera
            ICellStyle cStyle = hssfwb.CreateCellStyle();
            cStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
            cStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cStyle.FillPattern = FillPattern.SolidForeground;
            cStyle.Alignment = HorizontalAlignment.Center;
            cStyle.VerticalAlignment = VerticalAlignment.Center;
            cStyle.BorderBottom = BorderStyle.Medium;
            cStyle.BorderLeft = BorderStyle.Medium;
            cStyle.BorderRight = BorderStyle.Medium;
            cStyle.BorderTop = BorderStyle.Medium;
            cStyle.SetFont(font);


            //detalle izq
            ICellStyle diStyle = hssfwb.CreateCellStyle();
            //cStyle.BorderBottom = FillPattern.SolidForeground;
            diStyle.BorderBottom = BorderStyle.Thin;
            diStyle.BorderLeft = BorderStyle.Thin;
            diStyle.BorderRight = BorderStyle.Thin;
            diStyle.BorderTop = BorderStyle.Thin;

            //detalle der
            ICellStyle ddStyle = hssfwb.CreateCellStyle();
            //cStyle.BorderBottom = FillPattern.SolidForeground;
            ddStyle.BorderBottom = BorderStyle.Thin;
            ddStyle.BorderLeft = BorderStyle.Thin;
            ddStyle.BorderRight = BorderStyle.Thin;
            ddStyle.BorderTop = BorderStyle.Thin;

            if (nombreReporte != "ValidacionMaterial")
            {
                row = excelSheet.CreateRow(3);
                // row.get(nrowsIni + 2);
                // row.CreateCell(nrowsIni+2).SetCellValue(nombreReporte + " " + param);

                ICell cell = row.CreateCell(nrowsIni + 2);
                cell.SetCellValue(nombreReporte + " " + param);
                cell.CellStyle = tStyle;
                //  var _row =  row.GetCell(nrowsIni + 2);
                // _row.SetCellValue(nombreReporte + " " + param); 
            }

            PropertyInfo[] pr = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // Int32 nrows = 7;
            // Int32 ncolsc = 0;

            row = excelSheet.CreateRow(nrows);
            row.Height = 400;
            //genera cabecera
            foreach (PropertyInfo prop in pr)
            {
                // row.GetCell(nrowsIni).SetCellValue(prop.Name);
                //  row.CreateCell(nrowsIni).SetCellValue(prop.Name);
                ICell cell = row.CreateCell(nrowsIni);
                cell.SetCellValue(prop.Name.ToString());
                cell.CellStyle = cStyle;
                nrowsIni++;
            }
            //genera detalle
            if (nombreReporte == "ValidacionMaterial") { nrows = 1; nrowsIni = 0; }
            else { nrows = 8; nrowsIni = 1; }
            //nrows = 8;
            foreach (T item in model)
            {
                row = excelSheet.CreateRow(nrows);
                var values = new object[pr.Length];
                for (int i = 0; i <= (pr.Length - 1); i++)
                {
                    values[i] = pr[i].GetValue(item, null);
                    if (values[i] != null)
                    {
                        // row.CreateCell(nrowsIni+i).SetCellValue(values[i].ToString());
                        ICell cell = row.CreateCell(nrowsIni + i);

                        //var columnDataType = values[i].GetType().ToString();// (IsNumeric(values[i].ToString()) == true ? "System.Decimal" : values[i].GetType().ToString());// column.DataType.ToString();

                        if (string.IsNullOrWhiteSpace(values[i].ToString()))
                        {
                            cell.SetCellValue(String.Empty);
                            cell.CellStyle = diStyle;
                            //short dateFormat = hssfwb.CreateDataFormat().GetFormat("dd/MM/yyyy HH:mm");
                            //cell.CellStyle.DataFormat = dateFormat;

                        }
                        else
                        {

                            if (Util.ParseString(values[i].ToString()) == "System.DateTime")
                            {
                                DateTime vDate = Convert.ToDateTime(values[i].ToString());
                                short dateFormat = 0;
                                if (vDate != null && vDate.Hour > 0)
                                    dateFormat = hssfwb.CreateDataFormat().GetFormat("dd/MM/yyyy HH:mm:ss");
                                else
                                    dateFormat = hssfwb.CreateDataFormat().GetFormat("dd/MM/yyyy");

                                ddStyle.Alignment = HorizontalAlignment.Left;
                                cell.CellStyle.DataFormat = dateFormat;

                                cell.SetCellValue((values[i].ToString()));
                                cell.CellStyle = ddStyle;
                            }
                            else if (Util.ParseString(values[i].ToString()) == "System.Int32" || Util.ParseString(values[i].ToString()) == "System.Int64")
                            {
                                short dateFormat = hssfwb.CreateDataFormat().GetFormat("#,##0");
                                cell.CellStyle.DataFormat = dateFormat;
                                ddStyle.Alignment = HorizontalAlignment.Right;

                                cell.SetCellValue(Convert.ToInt64(values[i].ToString()));
                                cell.CellStyle = ddStyle;
                            }
                            else if (Util.ParseString(values[i].ToString()) == "System.Decimal")
                            {
                                short dateFormat = hssfwb.CreateDataFormat().GetFormat("###,###,##0.00");
                                cell.CellStyle.DataFormat = dateFormat;
                                ddStyle.Alignment = HorizontalAlignment.Right;

                                cell.SetCellValue(Convert.ToDouble(values[i].ToString()));
                                cell.CellStyle = ddStyle;
                            }
                            else
                            {
                                diStyle.Alignment = HorizontalAlignment.Left;

                                cell.SetCellValue(values[i].ToString());
                                cell.CellStyle = diStyle;
                            }

                        }
                    }
                }
                nrows++;
            }
            // workbook.Write(fs);
            hssfwb.Write(memoryStream);
            //  }
            //byte[] buffer = System.IO.File.ReadAllBytes(filePath);

            /*
            using (var fileStream = new FileStream(Path.Combine(url, fileName), FileMode.Open))
            {
                 fileStream.CopyToAsync(memoryStream);               
            }  
            */

            return memoryStream;
        }

        public async Task<ResultDTO<bool>> envioMailPlantillaRClave(int idplantilla, UsuarioDTO usuario, string token)
        {
            ResultDTO<bool> res = new ResultDTO<bool>();

            EmailDTO email = new EmailDTO();
            PlantillaCorreoDTO plantilla = new PlantillaCorreoDTO();
            // plantilla.iid_empresa =
            plantilla.iid_plantilla_correo = idplantilla;

            ResultDTO<PlantillaCorreoDTO> resplantilla = await this.iIPlantillaCorreoServices.GetPlantillaCorreo(plantilla);

            var msjcuerpo =  resplantilla.item.vcuerpo_correo.Replace("[usuario]", usuario.vnombres + " " + usuario.vapellido_paterno + " " + usuario.vapellido_materno);
            msjcuerpo = msjcuerpo.Replace("[codigo]", token);

            email.Mensaje = msjcuerpo; 

            email.Titulo = resplantilla.item.vtitulo_correo;

            var lstemail = new List<string>();
            lstemail.Add(usuario.vcorreo_electronico);

            email.Para = lstemail;



            res = await envioMail(email);


            return res;
        }
    }
}
