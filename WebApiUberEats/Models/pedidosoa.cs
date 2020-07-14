using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using WebApiUberEats.Transfers;

namespace WebApiUberEats.Models
{
    public partial class pedido
    {
        public static pedidodt ObtenerPedidoRegistrado(int idpedido)
        {
            //ObtenerPedidoRegistrado -> no mostrarlo en la presentacion por que no fue enviado
            bdubereatsEntities4 db = new bdubereatsEntities4();
            var obj = db.Pedido.Select(b => // Obtiene lista de Pedido
                new pedidodt()
                {   //Propiedad de la clase Pedido
                    idpedido = b.idpedido,
                    instrucciones_especiales_pedido = b.instrucciones_especiales_pedido,
                    cantidad_pedido                 = (int)b.cantidad_pedido,
                    direccion_entrega               = b.direccion_entrega,
                    idcomercio                      = (int)b.idcomercio,
                    idproducto                      = (int)b.idproducto,
                    estado                          = (int)b.estado

                }).SingleOrDefault(b => b.idpedido == idpedido); // devuelvo elemento si cumple con la condicion
            return obj;
        }

        public static pedidodt InsertaPedido(pedidodt pedidodt) {
            bdubereatsEntities4 db = new bdubereatsEntities4();
            //regla 3: valida datos unicos (idpedido)
            var vidpedido = db.Pedido.Where(p => p.idpedido == pedidodt.idpedido).Count();
            //regla 2: valida la cantidad de pedido debe ser mayor  o igual a 1
            var vcantidad_p = db.Pedido.Where(p => 1 > pedidodt.cantidad_pedido).Count(); //si 1 > 0 (cantidad de ingreso) devuelve 1

            if (vidpedido > 0)//si la variable vidpedido tiene valor 1  retornara null,caso contrario continua.
                return null;
            
            if (vcantidad_p > 0)//si la variable vcantidad_p tiene valor 1  retornara null,caso contrario continua.
                return null;

            
            //regla 1: Valida datos obligatorios 
            //implementado en la clase Pedido a travez del DataAnnotations

            Pedido pedidos = new Pedido()
            {
                instrucciones_especiales_pedido = pedidodt.instrucciones_especiales_pedido,
                cantidad_pedido = pedidodt.cantidad_pedido,
                direccion_entrega = pedidodt.direccion_entrega,
                idcomercio = pedidodt.idcomercio,
                idproducto = pedidodt.idproducto,
                estado = pedidodt.estado
            };

            try
            {
                db.Pedido.Add(pedidos); //Agrega el objeto pedidos a la clase Pedido
                db.SaveChanges();//guarda cambios.

               
                Producto producto = db.Producto.Find(pedidodt.idproducto);
                //trae stock
                var list = from b in db.Producto.Where(p => p.idproducto == pedidodt.idproducto)
                           select new productodt()
                           {
                              stock=(int)b.stock
                           };
                
                //actualizar el stock
                producto.stock -= pedidodt.cantidad_pedido;
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();   


            }
            //captura los campos que estan vacios y muestra mensaje segun configuracion en la clase Pedido
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
            return pedido.ObtenerPedidoRegistrado(pedidos.idpedido);
        }

        public static pedidodetalledt ObtieneListaDetallePedido(int idpedido) {
            bdubereatsEntities4 db = new bdubereatsEntities4();
            var obj = db.Pedido.Select(b => // Obtiene lista de Pedido
                new pedidodetalledt()
                {   
                    idpedido            = b.idpedido,
                   cantidad_pedido      = (int)b.cantidad_pedido,
                   direccion_entrega    = b.direccion_entrega,
                    estadopedidodt      = new estadopedidodt() { descripcion = b.Estados.descripcion},
                    productocategoriadt = new productocategoriadt(){nombreproducto = b.Producto.nombreproducto,
                                                                    precio = (decimal)b.Producto.precio,
                                                                    nombrefotoproducto =b.Producto.nombrefotoproducto,},
                    usuariocomerciodt   = new usuariocomerciodt() {razonsocial = b.Comercio.Usuario.razonsocial}
                  

                }).SingleOrDefault(b => b.idpedido == idpedido); 
                return obj;
        }

        public static estadopedidodt ConfirmarPedido(int idpedido, pedidodt pedidodt) {
            
            bdubereatsEntities4 db = new bdubereatsEntities4();
            //regla 1: valida que el id pedido exista en bd
            var vidpedidoexiste = db.Pedido.Where(p => p.idpedido != idpedido).Count();//si es true devuelve 1,caso contrario devuelve 0
            //regla 2: valida que el pedido se encuentre en estado generado(1)
            var vestadogenerado = db.Pedido.Where(p => p.idpedido == idpedido && p.estado != 1).Count();//si es true devuelve 1, caso contrario devuelve 0


            if (vidpedidoexiste > 0)
                return null;

            if (vestadogenerado > 0)
                return null;

            Pedido pedidos= db.Pedido.Find(idpedido);
            pedidos.estado = pedidodt.estado;
            db.Entry(pedidos).State = EntityState.Modified;
            db.SaveChanges();
            return pedido.PedidoConfirmado(idpedido);
        }

        public static estadopedidodt PedidoConfirmado(int idpedido) {
            //trae lista del pedido que se ha confirmado
            bdubereatsEntities4 db = new bdubereatsEntities4();
            var obj = db.Pedido.Select(b => // Obtiene lista de Pedido
                new estadopedidodt()
                {
                    idpedido    = b.idpedido,
                    descripcion = b.Estados.descripcion

                }).SingleOrDefault(b => b.idpedido == idpedido);
            return obj;
        }


        public static estadopedidodt CancelarPedido(int idpedido, pedidodt pedidodt)
        {

            bdubereatsEntities4 db = new bdubereatsEntities4();
            //regla 1: valida que el id pedido exista en bd
            var vidpedidoexiste = db.Pedido.Where(p => p.idpedido != idpedido).Count();//si es true devuelve 1,caso contrario devuelve 0
            //regla 2: valida que el pedido se encuentre en estado generado(1)
            var vestadogenerado = db.Pedido.Where(p => p.idpedido == idpedido && p.estado != 1).Count();//si es true devuelve 1, caso contrario devuelve 0


            if (vidpedidoexiste > 0)
                return null;

            if (vestadogenerado > 0)
                return null;

            Pedido pedidos = db.Pedido.Find(idpedido);
            pedidos.estado = pedidodt.estado;
            db.Entry(pedidos).State = EntityState.Modified;
            db.SaveChanges();
            return pedido.PedidoCancelado(idpedido);
        }

        public static estadopedidodt PedidoCancelado(int idpedido)
        {
            bdubereatsEntities4 db = new bdubereatsEntities4();
            var obj = db.Pedido.Select(b => // Obtiene lista de Pedido
                new estadopedidodt()
                {
                    idpedido = b.idpedido,
                    descripcion = b.Estados.descripcion

                }).SingleOrDefault(b => b.idpedido == idpedido);
            return obj;
        }

        public static pedidosestado2dt ObtieneListaPedidosConfirmados(int estado) {
            
            bdubereatsEntities4 db = new bdubereatsEntities4();

            //regla 1: valida el estado en confirmado (2)
            var vestado2 = db.Pedido.Where(p => p.estado != estado).Count();//si es true devuelve 1,caso contrario devuelve 0

            if (vestado2 > 0) //condicional  : si vestado2 > 0 ,termina proceso y retorna null
                return null;

            var obj = db.Pedido.Select(b => // Obtiene lista de Pedido
                new pedidosestado2dt()
                {
                    idpedido = b.idpedido,
                    estado = (int)b.estado,
                    pedidoproductodt = new pedidoproductodt
                    {
                        nombreproducto = b.Producto.nombreproducto,
                        precio = (decimal)b.Producto.precio
                    },
                    cantidad_pedido =(int) b.cantidad_pedido,
                    direccion_entrega = b.direccion_entrega

                }).SingleOrDefault(b => b.estado == estado);//si cumple condicional retorna data del obj  
            return obj;//retorna obj
        }

    }
}