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
            var obj = db.Producto.Select(b => // Obtiene lista de usuario cliente
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

        public static productodt InsertarProducto(productodt productodt) 
        {
            bdubereatsEntities4 db = new bdubereatsEntities4();
            //regla 1: valida datos unicos (correo,numero telefono)
            var idp = db.Producto.Where(p => p.idproducto == productodt.idproducto).Count();
            var istock = db.Producto.Where(p => productodt.stock <= 0).Count(); //valida el stock ingresado si es 0 devuelve 1
            var iprecio = db.Producto.Where(p => productodt.precio <= 0).Count(); //valida el precio ingresado si es 0 devuelve 1
   

            if (idp > 0) //si el idproducto ingresado existe en bd no permitira el registro y retornara null.
                return null;

            if (istock > 0) //si la variable istock tiene valor 1  retornara null,caso contrario continua.
                return null;

            if (iprecio > 0) //si la variable iprecio tiene valor 1  retornara null,caso contrario continua.
                return null;

            //regla 2: Valida datos obligatorios 
            //implementado en la clase Producto a travez del DataAnnotations

            //Instancias la clase Producto y sus propiedades que seran insertados
            Producto productos = new Producto()
            {
                //Propiedades de la clase Producto
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
                db.Producto.Add(productos); //Agrega el objeto usuario a la clase Producto
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
        //ObtenerUsuarioCliente -> no mostrarlo en la presentacion por que no fue enviado.
        {
            bdubereatsEntities4 db = new bdubereatsEntities4();
            var obj = db.Producto.Select(b => // Obtiene lista de usuario cliente
                new productocomerciodt()
                {
                    idcomercio          =(int)b.idcomercio,
                   nombreproducto       = b.nombreproducto,
                   descripcion          = b.descripcion,
                   precio               = (decimal) b.precio,
                   nombrefotoproducto   = b.nombrefotoproducto,                  
                        
                   categoria_Comerciodt = new categoria_comerciodt()
                        {
                         
                          descripcion = b.Categoria_Producto.descripcion,
                        }                    

                }).SingleOrDefault(b => b.idcomercio == idcomercio); // devuelvo elemento si cumple con la condicion
            return obj;
        }

    }
}