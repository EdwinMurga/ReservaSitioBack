using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.DTOs.ParametroAplicacion
{
    public class TablaDetalleParametroDTO : AuditoriaDTO
    {

        public int iid_tabla_detalle { set; get; }
        public int iid_tabla_auxiliar { set; get; }
        public int iid_registro_tabla { set; get; }
        public string iid_codigo_descripcion { set; get; }
        public string vvalor_texto_corto { set; get; }
        public string vvalor_texto_largo { set; get; }
        public int nvalor_entero { set; get; }
        public decimal nvalor_decimal { set; get; }
        public string vestado { get; set; }
    }
}
