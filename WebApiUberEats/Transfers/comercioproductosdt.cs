using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiUberEats.Transfers
{
    public class comercioproductosdt
    {
        public int idproducto { get; set; }
        public string nombreproducto { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public string nombrefotoproducto { get; set; }       
        public categoria_productodt categoria_Productodt { get; set; }
       
    }
}