using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Usuario;
using ReservaSitio.DTOs.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction.IApplication.Util
{
  public   interface IUtilAplication
    {
        public Task<ResultDTO<bool>> envioMailPlantilla(int plantilla, int[] idusuario);
        public Task<ResultDTO<bool>> envioMailPlantillaRClave(int idplantilla, UsuarioDTO usuario, string token);
        public Task<ResultDTO<bool>> envioMail(EmailDTO email);
        public MemoryStream CreateExcel<T>(List<T> model, String nombreReporte, string param);
    }
}
