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
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.Pedido = new HashSet<Pedido>();
        }

        public int idproducto { get; set; }
        public string nombreproducto { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> stock { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public Nullable<decimal> precio { get; set; }
        public Nullable<decimal> porcentajedsc { get; set; }
        public string nombrefotoproducto { get; set; }
        public Nullable<int> idcomercio { get; set; }
        public Nullable<int> idcategoria_producto { get; set; }
    
        public virtual Categoria_Producto Categoria_Producto { get; set; }
        public virtual Comercio Comercio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
