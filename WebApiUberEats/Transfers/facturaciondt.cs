using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiUberEats.Transfers
{
    public class facturaciondt
    {
        public int? idfacturacion { get; set; }
        public int? idtipocomprobante { get; set; }
        public int? idtipodocumento { get; set; }
        public int? idpedido { get; set; }
        public string nombre { get; set; }
        public string documento { get; set; }
        public string direccion { get; set; }

    }
}