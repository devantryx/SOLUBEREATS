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
    public class UsuarioclienteController : ApiController
    {

        [HttpPost]
        [Route("api/Usuariocliente/InsertarUsuarioCliente")]
        public usuariodt InsertarUsuarioCliente(usuariodt usuariodt)
        {
            return usuariocliente.InsertarUsuarioCliente(usuariodt);
        
        }

        [HttpPost]
        [Route("api/Usuariocliente/InsertarUsuarioComercio")]
        public usuariodt InsertarUsuarioComercio(usuariodt usuariodt)
        {
            return usuariocliente.InsertarUsuarioComercio(usuariodt);

        }

        [HttpGet]
        [Route("api/Usuariocliente/ObtenerUsuarioCliente")]
        public usuariodt ObtenerUsuarioCliente(int idusuario)
        {
            return usuariocliente.ObtenerUsuarioClienteRegistrado(idusuario);
        }

        [HttpGet]
        [Route("api/Usuariocliente/ObtenerUsuarioComercio")]
        public comerciodt ObtenerUsuarioComercio(int idcomercio)
        {
            return usuariocliente.ObtenerUsuarioComercioRegistrado(idcomercio);
        }

        //Lista todos los comercios
        [HttpGet]
        [Route("api/Usuariocliente/ListarComercios")]
        public IEnumerable<comerciodt> ListarComercios()
        {
            return usuariocliente.ListarComercios();
        }

        [HttpGet]
        [Route("api/Usuario/IniciarSession")]
        public usuariodt IniciarSession(string correo, string clave)
        {
            return usuariocliente.IniciarSession(correo, clave);
        }
        
    }
}
