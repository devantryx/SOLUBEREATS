﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiUberEats.Models;

namespace WebApiUberEats.Transfers
{
    public class productocategoriadt
    {
        public int idcategoria_producto { get; set; }
        public string nombreproducto { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public string nombrefotoproducto { get; set; }

        public categoria_productodt categoria_productodt { get; set; }
    }
}