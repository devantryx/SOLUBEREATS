using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiUberEats.Transfers
{
    public class productocomerciodt
    {
        public string nombreproducto { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public string nombrefotoproducto { get; set; }
        public int idcomercio { get; set; }
        public usuariocomerciodt usuariocomerciodt { get; set; }
        public categoria_comerciodt categoria_Comerciodt { get; set; }

    }
}