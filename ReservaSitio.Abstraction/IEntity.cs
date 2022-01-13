using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction
{
    public interface IEntity
    {
        int seguc_iid { get; set; }
        int? seguc_iid_usuario_registra { get; set; }
        DateTime? seguc_dfecha_registra { get; set; }
        int? seguc_iid_usuario_modifica { get; set; }
        DateTime? seguc_dfecha_modifica { get; set; }
        int? seguc_iid_usuario_elimina { get; set; }
        DateTime? seguc_dfecha_elimina { get; set; }
    }
}
