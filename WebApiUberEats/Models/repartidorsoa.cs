using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiUberEats.Transfers;

namespace WebApiUberEats.Models
{
    public partial class repartidor
    {
        public static repartidorestadodt ObtenerRepartidorDisponible(int estado)
        {

            BdUberEatsEntities db = new BdUberEatsEntities();

            //regla 1: valida si esta disponible el repartidor estado en(1)
            var vrepestado1 = db.Repartidor.Where(p => p.estado != estado).Count();

            if (vrepestado1 > 0) 
                return null;

            var obj = db.Repartidor.Select(b =>
                new repartidorestadodt()
                {
                    idrepartidor = b.idrepartidor,
                  estado = (int)b.estado

                }).SingleOrDefault(b => b.estado == estado);
            return obj;
        }

    }
}