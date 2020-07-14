using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiUberEats.Transfers
{
    public class pedidodetalledt
    {
        public int idpedido { get; set; }
        public int cantidad_pedido { get; set; }
        public string direccion_entrega { get; set; }
        public estadopedidodt estadopedidodt { get; set; }
        public usuariocomerciodt usuariocomerciodt { get; set; }
        public productocategoriadt productocategoriadt{ get; set; }
        
    }
}