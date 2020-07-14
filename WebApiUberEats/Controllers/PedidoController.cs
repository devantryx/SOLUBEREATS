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
    public class PedidoController : ApiController
    {

        [HttpGet]
        [Route("api/pedido/ObtenerPedidoRegistrado")]
        public pedidodt ObtenerPedidoRegistrado(int idpedido)
        {
            return pedido.ObtenerPedidoRegistrado(idpedido);
        }


        [HttpPost]
        [Route("api/pedido/InsertaPedido")]
        public pedidodt InsertaPedido(pedidodt pedidodt)
        {
            return pedido.InsertaPedido(pedidodt);

        }

        [HttpGet]
        [Route("api/pedido/ObtieneListaDetallePedido")]
        public pedidodetalledt ObtieneListaDetallePedido(int idpedido)
        {
            return pedido.ObtieneListaDetallePedido(idpedido);
        }

        


    }
}
