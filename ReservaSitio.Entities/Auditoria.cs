using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSitio.Abstraction;

namespace ReservaSitio.Entities
{
    public class Auditoria : IEntity
    {
        [Key]
        public int seguc_iid { get; set; }
        public int? seguc_iid_usuario_registra { get; set; }
        public DateTime? seguc_dfecha_registra { get; set; }
        public int? seguc_iid_usuario_modifica { get; set; }
        public DateTime? seguc_dfecha_modifica { get; set; }
        public int? seguc_iid_usuario_elimina { get; set; }
        public DateTime? seguc_dfecha_elimina { get; set; }
    }
}
