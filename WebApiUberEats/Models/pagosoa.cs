using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using WebApiUberEats.Transfers;

namespace WebApiUberEats.Models
{
    public partial class pago
    {
        public static IEnumerable<tipo_pagodt> ObtenerListaTiposPago()
        {

            bdubereatsEntities db = new bdubereatsEntities();

            var list = from r in db.Tipo_Pago
                       select new tipo_pagodt()
                       {
                           idtipopago = r.idtipopago,
                           descripcion = r.descripcion
                       };

            return list;
        }

        public static pagodt RealizarPago(pagodt pagodt)
        {
            //regla 0: validar que se tengan todos los datos
            if(pagodt.idpedido == null ||pagodt.idcliente == null || pagodt.idtipopago == null || (pagodt.idtipopago == 1 && pagodt.idtarjeta == null))
            {
                return null;
            }

            bdubereatsEntities db = new bdubereatsEntities();
            //regla1: validar que el pedido existe
            var vCountPedido = db.Pedido.Where(p => p.idpedido == pagodt.idpedido).Count();
            if(vCountPedido != 1)
            {
                return null;
            }
            //regla2: validar que el pedido este en estado 2 Confirmado
            var vCountPedido2 = db.Pedido.Where(p => p.idpedido == pagodt.idpedido & p.estado == 2).Count();
            if (vCountPedido2 != 1)
            {
                return null;
            }
            //regla3: validar que no exista mas pagos para el pedido
            var vCountPagoPedido = db.Pago.Where(p => p.idpedido == pagodt.idpedido).Count();
            if (vCountPagoPedido != 0)
            {
                return null;
            }


            //regla 4 validar que exista la forma de pago
            var vCountFormaPago = db.Tipo_Pago.Where(p => p.idtipopago == pagodt.idtipopago).Count();
            if (vCountFormaPago != 1)
            {
                return null;
            }
            //regla 5 validar que la tarjeta exista si es forma de pago tarjeta
            if (pagodt.idtarjeta == 1)
            {
                var vCountTarjeta = db.Tarjeta.Where(p => p.idtarjeta == pagodt.idtarjeta).Count();
                if (vCountTarjeta != 1)
                {
                    return null;
                }
            }
            //regla 6 validar que el cliente exista
            var vCountCliente = db.Cliente.Where(p => p.idcliente == pagodt.idcliente).Count();
            if (vCountCliente != 1)
            {
                return null;
            }
            //regla 7 validar que el usuario exista
            var vCountUsuario = db.Usuario.Where(p => p.idusuario == pagodt.idcliente).Count();
            if (vCountUsuario != 1)
            {
                return null;
            }
            
            if (pagodt.idtarjeta == 1)
            {
                //regla 8 validar que la tarjeta este asociada al usuario
                var vIdUsuario = db.Cliente.Find(pagodt.idcliente).idusuario;
                var vCountTarjetaCliente = db.Tarjeta.Where(p => p.idusuario == vIdUsuario).Count();
                if (vCountUsuario != 1)
                {
                    return null;
                }
            }
            Pago pedido = new Pago()
            {
                idpedido = pagodt.idpedido,
                idtipopago = pagodt.idtipopago,
                idtarjeta = pagodt.idtarjeta,
                idcliente = pagodt.idcliente
            };

            try
            {
                db.Pago.Add(pedido); //Agrega el objeto pedidos a la clase Pedido
                Pedido vPedido = db.Pedido.Find(pagodt.idpedido);
                vPedido.estado = 5;
                db.Entry(vPedido).State = EntityState.Modified;
                db.SaveChanges();//guarda cambios
                enviarCorreoComprobanteUberEats();

            }
            //captura los campos que estan vacios y muestra mensaje segun configuracion en la clase Pedido
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
            Pago vPago = db.Pago.Find(pedido.idpago);
            pagodt vReturn = new pagodt()
            {
                idpago = vPago.idpago,
                idpedido = vPago.idpedido,
                idtipopago = vPago.idtipopago,
                idtarjeta = vPago.idtarjeta,
                idcliente = vPago.idcliente
            };

            return vReturn;
        }

        public static void enviarCorreoComprobanteUberEats()
        {

        }
    }

}