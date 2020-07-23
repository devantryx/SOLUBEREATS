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
    public class ProductoController : ApiController
    {
      
        [HttpGet]
        [Route("api/Producto/ObtenerProductoRegistrado")]
        public productodt ObtenerUsuarioCliente(int idproducto)
        {
            return producto.ObtenerProductoRegistrado(idproducto);
        }
        [HttpGet]
        [Route("api/Producto/ListarProductoCategoria")]
        public IEnumerable<productocategoriadt> ListarProductoCategoria(int idcategoria_producto) {
            return producto.ListarProductoCategoria(idcategoria_producto);
        }

        [HttpGet]
        [Route("api/Producto/ListarProductosComercio")]
        public IEnumerable<productocomerciodt> ListarProductosComercio(int idcomercio)
        {
            return producto.ListarProductosComercio(idcomercio);
        }

        [HttpPost]
        [Route("api/Producto/InsertarProducto")]
        public productodt InsertarProducto(productodt productodt)
        {
            return producto.InsertarProducto(productodt);

        }

    }
}
