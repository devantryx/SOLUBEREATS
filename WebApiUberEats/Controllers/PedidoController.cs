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

        [HttpPut]
        [Route("api/pedido/ConfirmarPedido")]
        public estadopedidodt ConfirmarPedido(int idpedido, pedidodt pedidodt) {
            return pedido.ConfirmarPedido(idpedido,pedidodt);
        }

        [HttpGet]
        [Route("api/pedido/PedidoConfirmado")]
        public estadopedidodt PedidoConfirmado(int idpedido)
        {
            return pedido.PedidoConfirmado(idpedido);

        }

        [HttpPut]
        [Route("api/pedido/CancelarPedido")]
        public estadopedidodt CancelarPedido(int idpedido, pedidodt pedidodt)
        {
            return pedido.CancelarPedido(idpedido, pedidodt);
        }

        [HttpGet]
        [Route("api/pedido/PedidoCancelado")]
        public estadopedidodt PedidoCancelado(int idpedido)
        {
            return pedido.PedidoCancelado(idpedido);

        }

        [HttpGet]
        [Route("api/paises/ObtenerListaTipopago")]
        public IEnumerable<tipo_pagodt> ObtenerListaTipopago()
        {
            return pedido.ObtenerListaTipopago();
        }


    }
}
