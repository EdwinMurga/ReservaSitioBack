using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Entities
{
    public class Enum
    {
        public enum TiposUsuario
        {
            restringido = 1,
            administrador = 2,
            vendedor = 3,
            soporteComercial = 4,
            tesoreria = 5,
            usuarioExterno = 6,

        }

        public enum Usuarios
        {
            interno = 1,
            externo = 2,
            soporteComercial = 3,
            vendedor = 4,
        }

        public static class UtilBE
        {
            public static string IndFechaVencida = "V";

            public static string IndFechaNormal = "N";

            public static string IndFechaNotiene = "S";

            public static string IndLineaDisponible = "D";

            public static string IndLineaNoDisponible = "N";

            public static string IndLineaSaturada = "S";
        }

        public static class UtilMensajes
        {
            public static string strInformnacionGrabada = "Datos Registrados";
            public static string strInformnacionNoGrabada = "Error Datos no Registrados";
            public static string strInformnacionEncontrada = "Información Encontrada ";
            public static string strInformnacionNoEncontrada = "Error Información no Encontrada ";
            public static string strExcepcion = "Error, Comuniquese con Soporte";
        }
     }
}
