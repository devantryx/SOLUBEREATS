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
        [Route("api/pago/ObtieneListaPago")]
        public IEnumerable<tipo_pagodt> ObtieneListaPago()
        {
            return pago.ObtenerListaTiposPago();
        }

        [HttpPost]
        [Route("api/pago/RealizarPago")]
        public pagodt InsertaPedido(pagodt pPagodt)
        {
            return pago.RealizarPago(pPagodt);

        }

    }
}
