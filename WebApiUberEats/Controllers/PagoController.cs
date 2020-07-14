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
    public class PagoController : ApiController
    {
        [HttpGet]
        [Route("api/Pago/ObtieneListaPago")]
        public IEnumerable<tipo_pagodt> ObtieneListaPago()
        {
            return pago.ObtieneListaPago();
        }
    }
}
