using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            BdUberEatsEntities db = new BdUberEatsEntities();
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

            BdUberEatsEntities db = new BdUberEatsEntities();
            var obj = db.Producto.Select(b => 
                new productodt()
                {   
                    idproducto = b.idproducto,
                    nombreproducto = b.nombreproducto,
                    descripcion = b.descripcion,
                    stock = (int)b.stock,
                    precio = (decimal)b.precio,
                    porcentajedsc =(decimal) b.porcentajedsc,
                    idcomercio = (int)b.idcomercio,
                    nombrefotoproducto = b.nombrefotoproducto,
                    idcategoria_producto =(int) b.idcategoria_producto

                }).SingleOrDefault(b => b.idproducto == idproducto); 
            return obj;
        }
        
        public static IEnumerable<productocategoriadt> ListarProductoCategoria(int idcategoria_producto)
        {
            BdUberEatsEntities db = new BdUberEatsEntities();            //regla 2: lista de forma descendente de mayor a menor el precio
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
            BdUberEatsEntities db = new BdUberEatsEntities();            //regla 1: valida datos unicos (idproducto)
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
            BdUberEatsEntities db = new BdUberEatsEntities();
            var list = from b in db.Comercio
                       join s in db.Producto on b.idcomercio equals s.idcomercio 
                       where
                       b.idcomercio == idcomercio &&
                       s.idcategoria_producto != 3
                       //.Where(b => b.idcomercio == idcomercio)
                       select new usuariocomercioproductosdt()
                       {
                           idcomercio                  = b.idcomercio,
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
                               idproducto = s.idproducto,
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
            BdUberEatsEntities db = new BdUberEatsEntities();
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
            BdUberEatsEntities db = new BdUberEatsEntities();
            int[] ids = {3,7,8 };
            var list = from b in db.Categoria_Producto                      
                       where ids.Contains(b.idcategoria_producto)
                      
                       select new categoria_productodt()
                       {
                           descripcion_categoria = b.descripcion
                       };

            return list;

        }

        //Buscar producto por nombre
        public static IEnumerable<productodt> BuscarProductoPorNombre(string nombreproducto, int idcomercio)
        {

            BdUberEatsEntities db = new BdUberEatsEntities();

            var lista = from b in db.Producto.Where(p => p.nombreproducto.ToLower().Trim() == nombreproducto.ToLower().Trim() & p.idcomercio == idcomercio)
                        select new productodt()
                        {
                            idproducto = b.idproducto,
                            nombreproducto       = b.nombreproducto,
                            descripcion          = b.descripcion,
                            stock                = (int)b.stock,
                            precio               = (decimal)b.precio,
                            porcentajedsc        = (decimal)b.porcentajedsc,
                            idcomercio           = (int)b.idcomercio,
                            nombrefotoproducto   = b.nombrefotoproducto,
                            idcategoria_producto = (int)b.idcategoria_producto

                        };

            return lista;

        }

        //Obtener producto del comercio 
        public static productodt ObtenerProductoId(int idproducto,int idcomercio)
        {

            BdUberEatsEntities db = new BdUberEatsEntities();
            var obj = db.Producto.Select(b =>
                new productodt()
                {
                    idproducto = b.idproducto,
                    nombreproducto = b.nombreproducto,
                    descripcion = b.descripcion,
                    stock = (int)b.stock,
                    precio = (decimal)b.precio,
                    porcentajedsc = (decimal)b.porcentajedsc,
                    idcomercio = (int)b.idcomercio,
                    nombrefotoproducto = b.nombrefotoproducto,
                    idcategoria_producto = (int)b.idcategoria_producto

                }).SingleOrDefault(b => b.idproducto == idproducto && b.idcomercio == idcomercio);
            return obj;
        }

        //Actualizar Producto existente
        public static productodt ActualizarProducto(int idproducto,int idcomercio, productodt productodt)
        {
            BdUberEatsEntities db = new BdUberEatsEntities();

            //valida que el idproducto y idcomercio existan en bd
            var vidproducto = db.Producto.Where(p => p.idproducto == idproducto & p.idcomercio == idcomercio).Count();
            
                if (vidproducto != 1)
                return null;

                Producto productos               = db.Producto.Find(idproducto);
                productos.nombreproducto         = productodt.nombreproducto;
                productos.descripcion            = productodt.descripcion;
                productos.stock                  = productodt.stock;
                productos.precio                 = productodt.precio;
                productos.idcomercio             = productodt.idcomercio;
                productos.idcategoria_producto   = productodt.idcategoria_producto;    
                

         
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return producto.ObtenerProductoModificado(productos.idproducto);
        }

        //Eliminar producto por idproducto
        public static bool EliminarProducto(int idproducto,int idcomercio)
        {
            BdUberEatsEntities db = new BdUberEatsEntities();

            //valida que el idproducto y idcomercio existan en bd
            var vidproducto = db.Producto.Where(p => p.idproducto == idproducto & p.idcomercio == idcomercio).Count();
            
            if (vidproducto != 1)
                return false;

           
            Producto productos = db.Producto.Find(idproducto);
            db.Producto.Remove(productos);
            db.SaveChanges();
            return true;
        }

        public static productodt ObtenerProductoModificado(int idproducto)
        {

            BdUberEatsEntities db = new BdUberEatsEntities();
            var obj = db.Producto.Select(b =>
                new productodt()
                {
                    idproducto = b.idproducto,
                    nombreproducto = b.nombreproducto,
                    descripcion = b.descripcion,
                    stock = (int)b.stock,
                    precio = (decimal)b.precio,
                    idcomercio = (int)b.idcomercio,                   
                    idcategoria_producto = (int)b.idcategoria_producto

                }).SingleOrDefault(b => b.idproducto == idproducto);
            return obj;
        }

        public static IEnumerable<categoria_productodt> ObtenerListaCategoriaProducto()
        {
            BdUberEatsEntities db = new BdUberEatsEntities();           
            var list = from b in db.Categoria_Producto
                       select new categoria_productodt()
                       {
                           idcategoria_producto = (int)b.idcategoria_producto,
                           descripcion_categoria = b.descripcion,
                          
                       };

            return list;

        }

        
    }
}
