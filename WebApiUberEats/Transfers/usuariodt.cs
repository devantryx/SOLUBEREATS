using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiUberEats.Transfers
{
    public class usuariodt
    {
        public int      idusuario   { get; set; }
        public string   nombre      { get; set; }       
        public string   apellidos   { get; set; }       
        public string   telefono    { get; set; }       
        public string   correo      { get; set; }
        public string   clave       { get; set; }
        public string   direccion   { get; set; }
        public string   nombrefoto  { get; set; }
        public int      idpais      { get; set; }        
        public string   razonsocial { get; set; }
        public int      idcategoria_comercio { get; set; }
       
    }
}