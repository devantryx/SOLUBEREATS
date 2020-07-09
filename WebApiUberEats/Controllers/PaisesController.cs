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
    public class PaisesController : ApiController
    {

        [HttpGet]
        [Route("api/paises/ObtenerListaPaises")]
        public IEnumerable<paisesdt> ObtenerListaPaises()
        {
            return paises.ObtenerListaPaises();
        }
    }
}
