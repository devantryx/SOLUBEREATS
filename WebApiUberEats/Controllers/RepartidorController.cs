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
    public class RepartidorController : ApiController
    {
        [HttpGet]
        [Route("api/repartidor/ObtenerRepartidorDisponible")]
        public repartidorestadodt ObtenerRepartidorDisponible(int estado)
        {
            return repartidor.ObtenerRepartidorDisponible(estado);

        }
        
    }
}
