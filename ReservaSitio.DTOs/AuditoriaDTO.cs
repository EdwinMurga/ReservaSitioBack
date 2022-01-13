using System;

namespace ReservaSitio.DTOs
{
    public class AuditoriaDTO: PaginacionDTO
    {

        public int? iid_usuario_modifica { get; set; }
        public DateTime? dfecha_modifica { get; set; }

        public int? iid_estado_registro { get; set; }

        public int? iid_usuario_registra { get; set; }
        public DateTime? dfecha_registra { get; set; }
        public int? iid_usuario_elimina { get; set; }
        public DateTime? dfecha_elimina { get; set; }
    }
}
