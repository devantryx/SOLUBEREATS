//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiUberEats.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Detalle_Pedido
    {
        public int iddetalle_pedido { get; set; }
        public Nullable<int> idpedido { get; set; }
        public Nullable<int> cantidad_producto { get; set; }
        public string nombre_producto { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<decimal> SubTotal { get; set; }
        public Nullable<decimal> total { get; set; }
    
        public virtual Pedido Pedido { get; set; }
    }
}
