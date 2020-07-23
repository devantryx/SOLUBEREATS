using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiUberEats.Transfers
{
    public class pedidosestado2dt
    {
        public int idpedido { get; set; }    
        public int estado { get; set; }
        public int item { get; set; }
        public string direccion_entrega { get; set; }
        public pedidoproductodt pedidoproductodt { get; set; }
     
    }
}