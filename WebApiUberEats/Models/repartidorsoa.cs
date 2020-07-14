using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiUberEats.Transfers;

namespace WebApiUberEats.Models
{
    public partial class repartidor
    {
        public static repartidorestadodt ObtenerRepartidorDisponible(int estado)//parametro de entrada 1
        {

            bdubereatsEntities4 db = new bdubereatsEntities4();

            //regla 1: valida si esta disponible el repartidor estado en(1)
            var vrepestado1 = db.Repartidor.Where(p => p.estado != estado).Count();//si es true devuelve 1,caso contrario devuelve 0

            if (vrepestado1 > 0) //condicional  : si vrepestado1 > 0 ,termina proceso y retorna null
                return null;

            var obj = db.Repartidor.Select(b =>
                new repartidorestadodt()
                {
                    idrepartidor = b.idrepartidor,
                  estado = (int)b.estado

                }).SingleOrDefault(b => b.estado == estado);//si cumple condicional retorna data del obj  
            return obj;//retorna obj
        }

    }
}