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
    public class TarjetaController : ApiController
    {
        //debio estar en el UsuarioclienteController pero se esta poniendo aca porque ya fue enviado
        
        [HttpGet]
        [Route("api/tarjeta/ObtenerTarjetaRegistrado")]
        public tarjetadt ObtenerTarjetaRegistrado(int idusuario)
        {
            return usuariocliente.ObtenerTarjetaRegistrado(idusuario);
        }

        [HttpPost]
        [Route("api/tarjeta/InsertarTarjeta")]
        public tarjetadt InsertarTarjeta(tarjetadt tarjetadt)
        {
            return usuariocliente.InsertarTarjeta(tarjetadt);
        }
    }
}
