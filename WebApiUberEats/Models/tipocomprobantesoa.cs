using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using WebApiUberEats.Transfers;

namespace WebApiUberEats.Models
{
    public partial class tipocomprobante
    {
        public static IEnumerable<tipo_comprobantedt> ObtieneListaTipoComprobante()
        {

            bdubereatsEntities db = new bdubereatsEntities();

            var list = from r in db.Tipo_Comprobante
                       select new tipo_comprobantedt()
                       {
                           idtipocomprobante = r.idtipocomprobante,
                           descripcion = r.descripcion
                       };

            return list;
        }
    }

}