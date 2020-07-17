using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiUberEats.Models;
using WebApiUberEats.Transfers;

namespace WebApiUberEats.Controllers
{
    public class TipoComprobanteController : ApiController
    {

        [HttpGet]
        [Route("api/comprobante/ObtieneListaTipoComprobante")]
        public IEnumerable<tipo_comprobantedt> ObtieneListaTipoComprobante()
        {
            return tipocomprobante.ObtieneListaTipoComprobante();
        }

    }
}
