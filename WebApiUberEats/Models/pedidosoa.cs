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
            
            bdubereatsEntities db = new bdubereatsEntities();
            var obj = db.Pedido.Select(b => 
                new pedidodt()
                {  
                    idpedido = b.idpedido,
                    instrucciones_especiales_pedido = b.instrucciones_especiales_pedido,
                    item                 = (int)b.item,
                    direccion_entrega               = b.direccion_entrega,
                    idcomercio                      = (int)b.idcomercio,
                    idproducto                      = (int)b.idproducto,
                    estado                          = (int)b.estado

                }).SingleOrDefault(b => b.idpedido == idpedido); 
            return obj;
        }

        public static pedidodt InsertaPedido(pedidodt pedidodt) {
            bdubereatsEntities db = new bdubereatsEntities();
            //regla 3: valida datos unicos (idpedido)
            var vidpedido = db.Pedido.Where(p => p.idpedido == pedidodt.idpedido).Count();
            //regla 2: valida la cantidad de pedido debe ser mayor  o igual a 1
           
            if (vidpedido > 0)
                return null;
            
            //regla 1: Valida datos obligatorios 
            //implementado en la clase Pedido a travez del DataAnnotations

            Pedido pedidos = new Pedido()
            {
                instrucciones_especiales_pedido = pedidodt.instrucciones_especiales_pedido,
                item = pedidodt.item,
                direccion_entrega = pedidodt.direccion_entrega,
                idcomercio = pedidodt.idcomercio,
                idproducto = pedidodt.idproducto,
                estado = pedidodt.estado
            };

            try
            {
                db.Pedido.Add(pedidos); 
                db.SaveChanges();

                Producto producto = db.Producto.Find(pedidodt.idproducto);
                //trae stock
                var list = from b in db.Producto.Where(p => p.idproducto == pedidodt.idproducto)
                           select new productodt()
                           {
                              stock=(int)b.stock
                           };
                
                //actualizar el stock
                producto.stock -= pedidodt.item;
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();   


            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
            return pedido.ObtenerPedidoRegistrado(pedidos.idpedido);
        }

        public static pedidodetalledt ObtieneListaDetallePedido(int idpedido) {
            bdubereatsEntities db = new bdubereatsEntities();
            var obj = db.Pedido.Select(b => 
                new pedidodetalledt()
                {   
                    idpedido            = b.idpedido,
                   item      = (int)b.item,
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
            
            bdubereatsEntities db = new bdubereatsEntities();
            //regla 1: valida que el id pedido exista en bd
            var vidpedidoexiste = db.Pedido.Where(p => p.idpedido != idpedido).Count();
            //regla 2: valida que el pedido se encuentre en estado generado(1)
            var vestadogenerado = db.Pedido.Where(p => p.idpedido == idpedido && p.estado != 1).Count();


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
            bdubereatsEntities db = new bdubereatsEntities();
            var obj = db.Pedido.Select(b =>
                new estadopedidodt()
                {
                    idpedido    = b.idpedido,
                    descripcion = b.Estados.descripcion

                }).SingleOrDefault(b => b.idpedido == idpedido);
            return obj;
        }

        public static estadopedidodt CancelarPedido(int idpedido, pedidodt pedidodt)
        {

            bdubereatsEntities db = new bdubereatsEntities();
            //regla 1: valida que el id pedido exista en bd
            var vidpedidoexiste = db.Pedido.Where(p => p.idpedido != idpedido).Count();
            //regla 2: valida que el pedido se encuentre en estado generado(1)
            var vestadogenerado = db.Pedido.Where(p => p.idpedido == idpedido && p.estado != 1).Count();

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
            bdubereatsEntities db = new bdubereatsEntities();
            var obj = db.Pedido.Select(b => 
                new estadopedidodt()
                {
                    idpedido = b.idpedido,
                    descripcion = b.Estados.descripcion

                }).SingleOrDefault(b => b.idpedido == idpedido);
            return obj;
        }

        public static pedidosestado2dt ObtieneListaPedidosConfirmados(int estado) {
            
            bdubereatsEntities db = new bdubereatsEntities();

            //regla 1: valida el estado en confirmado (2)
            var vestado2 = db.Pedido.Where(p => p.estado != estado).Count();

            if (vestado2 > 0) 
                return null;

            var obj = db.Pedido.Select(b => 
                new pedidosestado2dt()
                {
                    idpedido = b.idpedido,
                    estado = (int)b.estado,
                    pedidoproductodt = new pedidoproductodt
                    {
                        nombreproducto = b.Producto.nombreproducto,
                        precio = (decimal)b.Producto.precio
                    },
                    item =(int) b.item,
                    direccion_entrega = b.direccion_entrega

                }).SingleOrDefault(b => b.estado == estado);
            return obj;
        }

        public static estadopedidodt PedidoEntregado(int idpedido, pedidodt pedidodt)
        {
            bdubereatsEntities db = new bdubereatsEntities();

            //regla 1: el pedido debe estar en estado confirmado (2)
            var vpedidoconfirmado2 = db.Pedido.Where(p => p.idpedido == idpedido && p.estado != 2).Count();

            if (vpedidoconfirmado2 > 0)
                return null;

            Pedido pedidos = db.Pedido.Find(idpedido);
            pedidos.estado = pedidodt.estado;
            db.Entry(pedidos).State = EntityState.Modified;
            db.SaveChanges();
            return pedido.PedidoConfirmado(idpedido);

        }

        public static IEnumerable<tipo_pagodt> ObtenerListaTipopago()
        {

            bdubereatsEntities db = new bdubereatsEntities();

            var list = from b in db.Tipo_Pago.Where(p => p.idtipopago == p.idtipopago)
                       select new tipo_pagodt()
                       {
                          idtipopago = b.idtipopago,
                          descripcion = b.descripcion
                       };

            return list;

        }
        

    }
}