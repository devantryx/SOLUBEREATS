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
    
    public partial class Repartidor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Repartidor()
        {
            this.Entrega = new HashSet<Entrega>();
        }
    
        public int idrepartidor { get; set; }
        public string fotoperfil { get; set; }
        public string fotolicenciaconducir { get; set; }
        public string fotoantecedentes { get; set; }
        public string fotosoat { get; set; }
        public string coordenadas { get; set; }
        public Nullable<int> idmetodoentrega { get; set; }
        public Nullable<int> idusuario { get; set; }
        public Nullable<int> estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entrega> Entrega { get; set; }
        public virtual Metodo_Entrega Metodo_Entrega { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
