using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using WebApiUberEats.Transfers;

namespace WebApiUberEats.Models
{
    public partial class facturacion
    {
        public static facturaciondt InsertarSolicitudComprobante(facturaciondt facturaciondt)
        {
            //regla 0: validar que se tengan todos los datos
            if(facturaciondt.idtipocomprobante == null || facturaciondt.idtipodocumento == null || facturaciondt.idpedido == null || string.IsNullOrWhiteSpace(facturaciondt.nombre) || string.IsNullOrWhiteSpace(facturaciondt.documento) || string.IsNullOrWhiteSpace(facturaciondt.direccion))
            {
                return null;
            }

            BdUberEatsEntities1 db = new BdUberEatsEntities1();
            //regla1: validar que el pedido existe
            var vCountPedido = db.Pedido.Where(p => p.idpedido == facturaciondt.idpedido).Count();
            if (vCountPedido != 1)
            {
                return null;
            }
            //regla2: validar que el pedido exista con el estado 5 Pagado
            var vCountPedido5 = db.Pedido.Where(p => p.idpedido == facturaciondt.idpedido & p.estado == 5).Count();
            if (vCountPedido5 != 1)
            {
                return null;
            }

            //regla3: validar que no exista mas comprobantes para el pedido
            var vCountFacturaPedido = db.Facturacion.Where(p => p.idpedido == facturaciondt.idpedido).Count();
            if (vCountFacturaPedido != 0)
            {
                return null;
            }

            //regla4: validar que los tipos de comprobante existan
            var vCountTipoComprobante = db.Tipo_Comprobante.Where(p => p.idtipocomprobante == facturaciondt.idtipocomprobante).Count();
            if(vCountPedido != 1)
            {
                return null;
            }

            //regla5: validar que los tipos de documento exista
            var vCountTipoDocumento = db.Tipo_Documento.Where(p => p.idtipodocumento == facturaciondt.idtipodocumento).Count();
            if (vCountTipoDocumento != 1)
            {
                return null;
            }
            

            Facturacion facturacion = new Facturacion()
            {
                idtipocomprobante = facturaciondt.idtipocomprobante,
                idtipodocumento = facturaciondt.idtipodocumento,
                idpedido = facturaciondt.idpedido,
                nombre = facturaciondt.nombre,
                documento = facturaciondt.documento,
                direccion = facturaciondt.direccion
            };

            try
            {
                db.Facturacion.Add(facturacion); 
                Pedido vPedido = db.Pedido.Find(facturaciondt.idpedido);
                vPedido.estado = 6;
                db.Entry(vPedido).State = EntityState.Modified;
                db.SaveChanges();

            }

            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
            Facturacion vFacturacion = db.Facturacion.Find(facturacion.idfacturacion);
            facturaciondt vReturn = new facturaciondt()
            {
                idfacturacion = vFacturacion.idfacturacion,
                idtipocomprobante = vFacturacion.idtipocomprobante,
                idtipodocumento = vFacturacion.idtipodocumento,
                idpedido = vFacturacion.idpedido,
                nombre = vFacturacion.nombre,
                documento = vFacturacion.documento,
                direccion = vFacturacion.direccion
            };

            return vReturn;
        }

    }

}