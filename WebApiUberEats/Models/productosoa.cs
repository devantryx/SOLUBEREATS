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
       
        public static IEnumerable<productocomerciodt> ListarProductosComercio(int idcomercio)
        {
            //Lista doble comercio cosa q esta mal.
            //servicio se encuentra en lo enviado.
            bdubereatsEntities db = new bdubereatsEntities();
            var list = from b in db.Producto.Where(b => b.idcomercio == idcomercio)
                       select new productocomerciodt()
                       {
                           idcomercio = (int)b.idcomercio,                          
                           nombreproducto = b.nombreproducto,
                           descripcion = b.descripcion,
                           precio = (decimal)b.precio,
                           nombrefotoproducto = b.nombrefotoproducto,

                           usuariocomerciodt = new usuariocomerciodt() { 
                           
                           },

                           categoria_Comerciodt = new categoria_comerciodt()
                           {


                               descripcion = b.Categoria_Producto.descripcion,
                           }
                       };

            return list;
            
        }

        public static productodt ObtenerProductoRegistrado(int idproducto)       
        {
            //ObtenerProductoRegistrado -> no mostrarlo en la presentacion por que no fue enviado
            bdubereatsEntities db = new bdubereatsEntities();
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
            bdubereatsEntities db = new bdubereatsEntities();
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
            bdubereatsEntities db = new bdubereatsEntities();
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

        //Lista comercio seleccionado con todos sus productos menos las bebidas
        public static IEnumerable<usuariocomercioproductosdt> ListarComercioProductos(int idcomercio)
        {
            bdubereatsEntities db = new bdubereatsEntities();
            var list = from b in db.Comercio
                       join s in db.Producto on b.idcomercio equals s.idcomercio 
                       where
                       b.idcomercio == idcomercio &&
                       s.idcategoria_producto != 3
                       //.Where(b => b.idcomercio == idcomercio)
                       select new usuariocomercioproductosdt()
                       {
                           //idcomercio                  = b.idcomercio,
                           //idusuario                   = (int)b.idusuario,
                           //idcategoria_comercio        = (int)b.idcategoria_comercio,

                           usuariocomerciodt = new usuariocomerciodt() {
                               razonsocial = b.Usuario.razonsocial,
                               nombrefoto = b.Usuario.nombrefoto,
                               categoria_Comerciodt = new categoria_comerciodt() {
                                   descripcion = b.Categoria_Comercio.descripcion
                               }
                           },
                           comercioproductosdt = new comercioproductosdt()
                           {
                               nombreproducto = s.nombreproducto,
                               descripcion = s.descripcion,
                               precio =(decimal)s.precio,
                               nombrefotoproducto = s.nombrefotoproducto,

                               categoria_Productodt = new categoria_productodt() {
                                    descripcion_categoria = s.Categoria_Producto.descripcion
                               }
                           },
                       };

            return list;

        }


        //Lista comercio seleccionado producto bebidas
        public static IEnumerable<usuariocomercioproductosdt> ListarComercioProductosBebidas(int idcomercio)
        {
            bdubereatsEntities db = new bdubereatsEntities();
            var list = from b in db.Comercio
                       join s in db.Producto on b.idcomercio equals s.idcomercio
                       where
                       b.idcomercio == idcomercio &&
                       s.idcategoria_producto == 3
                       select new usuariocomercioproductosdt()
                       {
                          
                           comercioproductosdt = new comercioproductosdt()
                           {
                               nombreproducto = s.nombreproducto,
                               descripcion = s.descripcion,
                               precio = (decimal)s.precio,
                               nombrefotoproducto = s.nombrefotoproducto,

                               categoria_Productodt = new categoria_productodt()
                               {
                                   descripcion_categoria = s.Categoria_Producto.descripcion
                               }
                           }
                       };

            return list;

        }

        //Lista categoria producto Adicionales: bebidas,guarniciones,platos especiales
        public static IEnumerable<categoria_productodt> ListarComercioProductosAdicionales()
        {
            bdubereatsEntities db = new bdubereatsEntities();
            int[] ids = {3,7,8 };
            var list = from b in db.Categoria_Producto                      
                       where ids.Contains(b.idcategoria_producto)
                      
                       select new categoria_productodt()
                       {
                           descripcion_categoria = b.descripcion
                       };

            return list;

        }

    }
}
