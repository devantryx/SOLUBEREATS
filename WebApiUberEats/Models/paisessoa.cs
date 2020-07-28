using Microsoft.AspNetCore.Mvc;
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

        public static IEnumerable<paisesdt> ObtenerListaPaises()
        {

            BdUberEatsEntities db = new BdUberEatsEntities();
            var list = from b in db.Pais.Where(p => p.idpais == p.idpais).OrderBy(p => p.codigo_pais)
                       select new paisesdt()
                       {
                           idpais       = b.idpais,
                           nombre = b.nombrepais,
                           codigo_pais  = b.codigo_pais,                           
                           nombrefoto   =b.nombrefoto
                       };

            return list;

        }
    }
}