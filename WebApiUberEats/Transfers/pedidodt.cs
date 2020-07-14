using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiUberEats.Transfers
{
    public class pedidodt
    {
        public int      idpedido                        { get; set; }
        public string   instrucciones_especiales_pedido { get; set; }
        public int      cantidad_pedido                 { get; set; }
        public string   direccion_entrega               { get; set; }
        public int      idcomercio                      { get; set; }
        public int      idproducto                      { get; set; }
        public int      estado                          { get; set; }

    }
}