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
    
    public partial class ubicacion_cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ubicacion_cliente()
        {
            this.pedidos_cab = new HashSet<pedidos_cab>();
        }
    
        public int id { get; set; }
        public Nullable<int> cliente_id { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string coordenadas { get; set; }
    
        public virtual clientes clientes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedidos_cab> pedidos_cab { get; set; }
    }
}