using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IApplication.Util;
using ReservaSitio.Abstraction.IService.LogError;
using ReservaSitio.Abstraction.IService.ParametrosAplicacion;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.ParametroAplicacion;
using ReservaSitio.DTOs.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Application.Util
{
    public class UtilAplication : IUtilAplication
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

           
            smtp.CredencialesPorDefecto = true;
            smtp.Puerto = Convert.ToInt32( this.iIConfiguration["PuertoSMTP"].ToString() );
            smtp.Servidor = this.iIConfiguration["ServidorSMTP"].ToString();
            smtp.Usuario = this.iIConfiguration["ServidorSMTPUsuario"].ToString();
            smtp.Password = this.iIConfiguration["ServidorSMTPPass"].ToString();
        }

        public async Task<ResultDTO<bool>> envioMailPlantilla(int idplantilla,int[] idusuario)
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

        public async Task<ResultDTO<bool>> envioMail(EmailDTO email)
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
                    client.UseDefaultCredentials = true;
                else
                {
                    client.UseDefaultCredentials = false;
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
                lg.vdescripcion = e.Message.ToString();
                lg.vcodigo_mensaje = e.Message.ToString();
                lg.vorigen = this.ToString();
                await this.iILogErrorServices.RegisterLogError(lg);
            }

            return res;
        }



    }
}
