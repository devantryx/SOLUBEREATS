using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using WebApiUberEats.Transfers;

namespace WebApiUberEats.Models
{
    public partial class producto
    {
       
        public static productodt ObtenerProductoRegistrado(int idproducto)       
        {
            //ObtenerProductoRegistrado -> no mostrarlo en la presentacion por que no fue enviado
            bdubereatsEntities4 db = new bdubereatsEntities4();
            var obj = db.Producto.Select(b => // Obtiene lista de Producto
                new productodt()
                {   //Propiedad de la clase Producto
                    idproducto = b.idproducto,
                    nombrefotoproducto = b.nombreproducto,
                    descripcion = b.descripcion,
                    stock = (int)b.stock,
                    precio = (decimal)b.precio,
                    porcentajedsc =(decimal) b.porcentajedsc,
                    idcomercio = (int)b.idcomercio,
                    idcategoria_producto =(int) b.idcategoria_producto

                }).SingleOrDefault(b => b.idproducto == idproducto); // devuelvo elemento si cumple con la condicion
            return obj;
        }
        public static IEnumerable<productocategoriadt> ListarProductoCategoria(int idcategoria_producto)
        {
            bdubereatsEntities4 db = new bdubereatsEntities4();
            //regla 2: lista de forma descendente de mayor a menor el precio
            var list = from b in db.Producto.Where(p => p.idcategoria_producto == idcategoria_producto).OrderByDescending(p => p.precio)
                       select new productocategoriadt()
                       {
                           nombreproducto = b.nombreproducto,
                           descripcion = b.descripcion,
                           precio = (decimal)b.precio,
                           nombrefotoproducto = b.nombrefotoproducto,
                           categoria_productodt = new categoria_productodt()
                           {
                               descripcion_categoria = b.Categoria_Producto.descripcion
                           }
                       };

            return list;

        }     
        public static productodt InsertarProducto(productodt productodt) 
        {
            bdubereatsEntities4 db = new bdubereatsEntities4();
            //regla 1: valida datos unicos (idproducto)
            var idproducto = db.Producto.Where(p => p.idproducto == productodt.idproducto).Count();
            var istock = db.Producto.Where(p => productodt.stock <= 0).Count(); //regla 2:   valida el stock ingresado si es 0 devuelve 1
            var iprecio = db.Producto.Where(p => productodt.precio <= 0).Count(); //regla 3: valida el precio ingresado si es 0 devuelve 1
   

            if (idproducto > 0) 
                return null;

            if (istock > 0) 
                return null;

            if (iprecio > 0)
                return null;

            //regla 2: Valida datos obligatorios 
            //implementado en la clase Producto a travez del DataAnnotations

         
            Producto productos = new Producto()
            {
 
                idproducto              = productodt.idproducto,
                nombreproducto          = productodt.nombreproducto,
                descripcion             = productodt.descripcion,
                stock                   = productodt.stock,
                precio                  = productodt.precio,
                porcentajedsc           = productodt.porcentajedsc,
                nombrefotoproducto      = productodt.nombrefotoproducto,
                idcomercio              = productodt.idcomercio,
                idcategoria_producto    = productodt.idcategoria_producto

            };

            try
            {
                db.Producto.Add(productos);
                db.SaveChanges();

            }
            
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }

            return producto.ObtenerProductoRegistrado(productos.idproducto);

        }
        public static productocomerciodt ListarProductosComercio(int idcomercio)
        {
           
            bdubereatsEntities4 db = new bdubereatsEntities4();

            var obj = db.Producto.Select(b =>
                new productocomerciodt()
                {
                    idcomercio = (int)b.idcomercio,
                    nombreproducto = b.nombreproducto,
                    descripcion = b.descripcion,
                    precio = (decimal)b.precio,
                    nombrefotoproducto = b.nombrefotoproducto,

                    categoria_Comerciodt = new categoria_comerciodt()
                    {

                        descripcion = b.Categoria_Producto.descripcion,
                    }

                }).SingleOrDefault(b => b.idcomercio == idcomercio);
            return obj;
        }
    }
}