﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BdUberEatsEntities : DbContext
    {
        public BdUberEatsEntities()
            : base("name=BdUberEatsEntities")        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Categoria_Comercio> Categoria_Comercio { get; set; }
        public virtual DbSet<Categoria_Producto> Categoria_Producto { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Comercio> Comercio { get; set; }
        public virtual DbSet<Detalle_Pedido> Detalle_Pedido { get; set; }
        public virtual DbSet<Entrega> Entrega { get; set; }
        public virtual DbSet<Estado_Repartidor> Estado_Repartidor { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<Facturacion> Facturacion { get; set; }
        public virtual DbSet<Metodo_Entrega> Metodo_Entrega { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Repartidor> Repartidor { get; set; }
        public virtual DbSet<Tarjeta> Tarjeta { get; set; }
        public virtual DbSet<Tipo_Comprobante> Tipo_Comprobante { get; set; }
        public virtual DbSet<Tipo_Documento> Tipo_Documento { get; set; }
        public virtual DbSet<Tipo_Pago> Tipo_Pago { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
