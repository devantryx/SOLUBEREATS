﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using WebApiUberEats.Transfers;

namespace WebApiUberEats.Models
{
    public partial class usuariocliente
    {
       
        public static usuariodt ObtenerUsuarioClienteRegistrado(int idusuario)       
        {
            //ObtenerUsuarioClienteRegistrado -> no mostrarlo en la presentacion por que no fue enviado.
            bdubereatsEntities4 db = new bdubereatsEntities4();
            var obj = db.Usuario.Select(b => // Obtiene lista de usuario cliente
                new usuariodt()
                {   //Propiedad de la clase Usuario
                    idusuario   = b.idusuario,
                    nombre      = b.nombre,
                    apellidos   = b.apellidos,
                    telefono    = b.telefono,
                    correo      = b.correo,
                    direccion   = b.direccion

                }).SingleOrDefault(b => b.idusuario == idusuario); // devuelvo elemento si cumple con la condicion
            return obj;
        }
        public static comerciodt ObtenerUsuarioComercioRegistrado(int idcomercio)       
        {
            //ObtenerUsuarioComercioRegistrado -> no mostrarlo en la presentacion por que no fue enviado.
            bdubereatsEntities4 db = new bdubereatsEntities4();
            var obj = db.Comercio.Select(b => // Obtiene lista de usuario comercio
                new comerciodt()
                {   //Propiedad de la clase Usuario
                    idusuario = (int)b.idusuario,
                    idcomercio = (int)b.idcomercio,
                    //idcategoria_comercio = (int)b.idcategoria_comercio,
                    usuariocomerciodt = new usuariocomerciodt()
                    { //instancia usuariodt para obtener sus propiedades
                        razonsocial = b.Usuario.razonsocial,
                        categoria_Comerciodt = new categoria_comerciodt()
                        {
                            descripcion = b.Categoria_Comercio.descripcion
                        }
                    }

                }).SingleOrDefault(b => b.idusuario == idcomercio); // devuelvo elemento si cumple con la condicion
            return obj; //lista el obj con sus propiedades 
        }

        public static tarjetadt ObtenerTarjetaRegistrado(int? idusuario) {
            //ObtenerTarjetaRegistrado -> no mostrarlo en la presentacion por que no fue enviado.
            bdubereatsEntities4 db = new bdubereatsEntities4();
            var obj = db.Tarjeta.Select(b => 
            new tarjetadt()
            {
                idusuario = (int)b.idusuario,
                numero_tarjeta = b.numero_tarjeta,
                usuariotarjetadt = new usuariotarjetadt() { 
                    nombre = b.Usuario.nombre,
                    apellidos = b.Usuario.apellidos
                }
                


            }).SingleOrDefault(b => b.idusuario == idusuario);
            return obj;
        }

        public static usuariodt InsertarUsuarioCliente(usuariodt usuariodt)
        {
            bdubereatsEntities4 db = new bdubereatsEntities4();
            //regla 1: valida datos unicos (correo,numero telefono)

            var vcorreo = db.Usuario.Where(u => u.correo.ToLower().Trim()   == usuariodt.correo.ToLower().Trim()).Count();
            var vnrotel = db.Usuario.Where(u => u.telefono.ToLower().Trim() == usuariodt.telefono.ToLower().Trim()).Count();

        
            if (vcorreo > 0) //si el correo ingresado existe en bd no permitira el registro y retornara null.
                return null;

            if (vnrotel > 0) //si el nùmero telefono ingresado existe en bd no permitira el registro y retornara null.
                return null;

            //regla 2: Valida datos obligatorios 
            //implementado en la clase Usuario a travez del DataAnnotations

            //Instancias la clase Usuario y sus propiedades que seran insertados
            Usuario usuario = new Usuario()
            {   //Propiedades de la clase Usuario
             
                nombre      = usuariodt.nombre,
                apellidos   = usuariodt.apellidos,
                telefono    = usuariodt.telefono,
                correo      = usuariodt.correo,
                clave       = usuariodt.clave,
                direccion   = usuariodt.direccion,
                nombrefoto  = usuariodt.nombrefoto,
                idpais      = usuariodt.idpais,
                razonsocial = usuariodt.razonsocial,
                idcategoria_comercio = usuariodt.idcategoria_comercio
            };

            //Instancias la clase Cliente y su propiedad que sera insertado
            Cliente cliente = new Cliente()
            {   //Propiedad de la clase Cliente
               
                idusuario = usuariodt.idusuario
            };
            try
            {
                db.Usuario.Add(usuario); //Agrega el objeto usuario a la clase Usuario
                db.SaveChanges();
                cliente.idusuario = usuario.idusuario; //Obtiene el idusuario insertado para que luego lo inserte en la tabla cliente
                db.Cliente.Add(cliente); //Agrega el objeto cliente a la clase Cliente
                db.SaveChanges();
               
            }
            //captura los campos que estan vacios y muestra mensaje segun configuracion en la clase Usuario
            catch (DbEntityValidationException ex){
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
           
            return usuariocliente.ObtenerUsuarioClienteRegistrado(usuario.idusuario); // Si el registro es exitoso, pasa como parametro el idusuario
        }
        public static usuariodt InsertarUsuarioComercio(usuariodt usuariodt)
        {

            bdubereatsEntities4 db = new bdubereatsEntities4();
            //regla 1: valida datos unicos (correo,numero telefono)
            var vcorreo = db.Usuario.Where(u => u.correo.ToLower().Trim() == usuariodt.correo.ToLower().Trim()).Count();
            var vnrotel = db.Usuario.Where(u => u.telefono.ToLower().Trim() == usuariodt.telefono.ToLower().Trim()).Count();

            if (vcorreo > 0) //si el correo ingresado existe en bd no permitira el registro y retornara null.
                return null;

            if (vnrotel > 0) //si el nùmero telefono ingresado existe en bd no permitira el registro y retornara null.
                return null;

            //regla 2: Valida datos obligatorios 
            //implementado en la clase Usuario a travez del DataAnnotations

            //Instancias la clase Usuario y sus propiedades que seran insertados
            Usuario usuario = new Usuario()
            {   //Propiedades de la clase Usuario

                nombre = usuariodt.nombre,
                apellidos = usuariodt.apellidos,
                telefono = usuariodt.telefono,
                correo = usuariodt.correo,
                clave = usuariodt.clave,
                direccion = usuariodt.direccion,
                nombrefoto = usuariodt.nombrefoto,
                idpais = usuariodt.idpais,
                razonsocial = usuariodt.razonsocial,
                idcategoria_comercio = usuariodt.idcategoria_comercio
            };


            //Instancias la clase Comercio y su propiedad que sera insertado
            Comercio comercio = new Comercio()
            {   //Propiedad de la clase Comercio

                idusuario = usuariodt.idusuario,
                idcategoria_comercio = usuariodt.idcategoria_comercio

            };


            try
            {
                db.Usuario.Add(usuario); //Agrega el objeto usuario a la clase Usuario
                db.SaveChanges();
                comercio.idusuario = usuario.idusuario; //Obtiene el idusuario insertado para que luego lo inserte en la tabla Comercio
                db.Comercio.Add(comercio); //Agrega el objeto cliente a la clase Comercio
                db.SaveChanges();

            }
            //captura los campos que estan vacios y muestra mensaje segun configuracion en la clase Usuario
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
            return usuariocliente.ObtenerUsuarioClienteRegistrado(usuario.idusuario); // Si el registro es exitoso, pasa como parametro el idusuario 
        }

        public static tarjetadt InsertarTarjeta(tarjetadt tarjetadt) {

            bdubereatsEntities4 db = new bdubereatsEntities4();
            //regla 1: valida datos unicos (nùmero tarjeta)
            var vnrotarjeta = db.Tarjeta.Where(t => t.numero_tarjeta == tarjetadt.numero_tarjeta).Count();//si el numero de tarjeta ingresada existe en bd la variable vnrotarjeta sera 1
            //regla 2: valida que el numero de tarjeta este asociado a una persona registrada
            var vidusuario = db.Tarjeta.Where(t => t.idusuario == tarjetadt.idusuario).Count(); // si el idusuario ingresado es igual al idusuario registrado en bd retornara valor 0 vidusuario

            if (vnrotarjeta > 0) //si la variable vnrotarjeta > a 0 retornara null.
                return null;

            if (vidusuario > 0) //si la variable vidusuario > 0 retorna null.
                return null;

            Tarjeta tarjeta = new Tarjeta()
            {
                nombre_tarjeta = tarjetadt.nombre_tarjeta,
                numero_tarjeta = tarjetadt.numero_tarjeta,
                codigo_tarjeta = tarjetadt.codigo_tarjeta,
                fechaven_tarjeta = tarjetadt.fechaven_tarjeta,
                idusuario = tarjetadt.idusuario
            };

            try
            {
                db.Tarjeta.Add(tarjeta); //Agrega el objeto tarjeta a la clase Tarjeta
                db.SaveChanges();           

            }
           
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
            return usuariocliente.ObtenerTarjetaRegistrado(tarjeta.idusuario);
        }
    }
}