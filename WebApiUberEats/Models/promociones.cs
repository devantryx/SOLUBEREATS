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
    
    public partial class promociones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public promociones()
        {
            this.pedidos_cab = new HashSet<pedidos_cab>();
        }
    
        public int id { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_fin { get; set; }
        public Nullable<decimal> porcentaje { get; set; }
        public Nullable<decimal> precio_fijo { get; set; }
        public string tipo { get; set; }
        public Nullable<decimal> consumo_min { get; set; }
        public Nullable<decimal> descuento_max { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedidos_cab> pedidos_cab { get; set; }
    }
}
