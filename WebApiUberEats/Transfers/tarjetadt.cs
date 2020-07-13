using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiUberEats.Transfers
{
    public class tarjetadt
    {
        public int      idtarjeta        { get; set; }
        public string   nombre_tarjeta   { get; set; }
        public string   numero_tarjeta   { get; set; }
        public string   codigo_tarjeta   { get; set; }
        public string   fechaven_tarjeta { get; set; }
        public int      idusuario        { get; set; }

        public usuariotarjetadt usuariotarjetadt{ get; set; }
    }
}