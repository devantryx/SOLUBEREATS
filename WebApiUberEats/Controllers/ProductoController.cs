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


        [HttpGet]
        [Route("api/Producto/ListarComercioProductos")]
        public IEnumerable<usuariocomercioproductosdt> ListarComercioProductos(int idcomercio)
        {
            return producto.ListarComercioProductos(idcomercio);
        }

        [HttpGet]
        [Route("api/Producto/ListarComercioProductosBebidas")]
        public IEnumerable<usuariocomercioproductosdt> ListarComercioProductosBebidas(int idcomercio)
        {
            return producto.ListarComercioProductosBebidas(idcomercio);
        }

        [HttpGet]
        [Route("api/Producto/ListarComercioProductosAdicionales")]
        public IEnumerable<categoria_productodt> ListarComercioProductosAdicionales()
        {
            return producto.ListarComercioProductosAdicionales();
        }

        [HttpGet]
        [Route("api/Producto/BuscarProductoPorNombre")]
        public productodt BuscarProductoPorNombre(string nombreproducto, int idcomercio)
        {
            return producto.BuscarProductoPorNombre(nombreproducto, idcomercio);
        }

        [HttpPut]
        [Route("api/Producto/ActualizarProducto")]
        public productodt ActualizarProducto(int idproducto,int idcomercio, productodt productodt)
        {
            return producto.ActualizarProducto(idproducto,idcomercio, productodt);
        }

        [HttpGet]
        [Route("api/Producto/ObtenerProductoModificado")]
        public productodt ObtenerProductoModificado(int idproducto)
        {
            return producto.ObtenerProductoModificado(idproducto);
        }

        [HttpDelete]
        [Route("api/Producto/EliminarProducto")]
        public bool EliminarProducto(int idproducto, int idcomercio)
        {
            return producto.EliminarProducto(idproducto, idcomercio);
        }

    }
}
