using ReservaSitio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.Opciones
{
    public class PerfilDTO : AuditoriaDTO
    {
        public int iid_perfil { get; set; }
        public string vnombre_perfil { get; set; }
        public string vdescripcion_perfil { get; set; }
        public string vestado { get; set; }
    }
}
