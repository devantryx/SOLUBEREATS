using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using WebApiUberEats.Transfers;

namespace WebApiUberEats.Models
{
  
    public partial class paises
    {
        //Obtiene lista de paises
        public static IEnumerable<paisesdt> ObtenerListaPaises()
        {
            bdubereatsEntities db = new bdubereatsEntities();

            var list = from b in db.paises.Where(p => p.id == p.id)
                       select new paisesdt()
                       {
                           id = b.id,
                           codigo_pais = b.codigo_pais,
                           nombre = b.nombre
                       };
            return list;

        }
    }
}