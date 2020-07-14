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
    public class EntregaController : ApiController
    {
        [HttpGet]
        [Route("api/entrega/ObtieneListaPedidosConfirmados")]
        public pedidosestado2dt ObtieneListaPedidosConfirmados(int estado)
        {
            return pedido.ObtieneListaPedidosConfirmados(estado);

        }
    }
}
