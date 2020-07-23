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
    public class FacturacionController : ApiController
    {

        [HttpPost]
        [Route("api/comprobante/InsertarSolicitudComprobante")]
        public facturaciondt InsertaPedido(facturaciondt pFacturaciondt)
        {
            return facturacion.InsertarSolicitudComprobante(pFacturaciondt);

        }

    }
}
