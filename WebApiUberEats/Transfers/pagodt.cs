using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiUberEats.Transfers
{
    public class pagodt
    {
        public int? idpago { get; set; }
        public int? idtipopago { get; set; }
        public int? idtarjeta { get; set; }
        public int? idcliente { get; set; }
        public int? idpedido { get; set; }

    }
}