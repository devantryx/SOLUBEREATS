using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiUberEats.Transfers;

namespace WebApiUberEats.Models
{
    public partial class pago
    {
        public static IEnumerable<tipo_pagodt> ObtieneListaPago()
        {
            bdubereatsEntities4 db = new bdubereatsEntities4();
            //regla 1: lista tipo de pago 
            var list = from b in db.Tipo_Pago
                       select new tipo_pagodt()
                       {
                           idtipopago = b.idtipopago,
                          descripcion = b.descripcion
                       };

            return list;

        }
    }
}