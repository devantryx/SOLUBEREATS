using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiUberEats.Transfers
{
    public class productodt
    {
        public int idproducto { get; set; }
        public string nombreproducto { get; set; }
        public string descripcion { get; set; }
        public int stock { get; set; }
        public decimal precio { get; set; }
        public decimal porcentajedsc { get; set; }
        public string nombrefotoproducto { get; set; }
        public int idcomercio { get; set; }
        public int idcategoria_producto { get; set; }
    }
}