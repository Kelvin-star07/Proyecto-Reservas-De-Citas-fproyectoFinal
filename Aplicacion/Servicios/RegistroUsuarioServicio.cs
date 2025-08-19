using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Infraestructura.Modelos;

namespace Aplicacion.Servicios
{
    public class RegistroUsuarioServicio : IRegistroUsuarioServicio
    {

        private readonly IRegistroUsuarioRepositorio repo;


        public RegistroUsuarioServicio(IRegistroUsuarioRepositorio repo)
        {


            this.repo = repo;

        }




        public string AgregarUsuario(RegistroUsuarioDTO usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario.Nombre))
                    throw new Exception("El campo nombre es obligatiro");

                if (string.IsNullOrEmpty(usuario.Apellido))
                    throw new Exception("El campo apellido es obligatiro");

                if (string.IsNullOrEmpty(usuario.Sexo))
                    throw new Exception("El campo sexo es obligatorio");

                if (string.IsNullOrEmpty(usuario.Cedula))
                    throw new Exception("El campo cedula es obligatorio");

                if (usuario.Dia < 0 || usuario.Dia > 31)
                    throw new Exception("El valor del dia debe ser positivo y correcto");

                if (usuario.Mes < 0 || usuario.Mes > 12)
                    throw new Exception("El valor del mes debe ser positivo y menor o igual que 12");

                if (usuario.Año < 1930 || usuario.Año > DateTime.Now.Year)
                    throw new Exception("El valor del anio debe ser correcto");

                if (string.IsNullOrEmpty(usuario.Contraseña) || usuario.Contraseña.Length > 10)
                    throw new Exception("El campo contraseñá es obligatorio y menor de 10 digitos");

                if (string.IsNullOrEmpty(usuario.Correo) || !usuario.Correo.Contains("@"))
                {
                    LoggerServicio.getInstancia().Error($"Fallo al intentar registrase {usuario.Nombre} no ingreso bien el correo");
                    throw new Exception("El campo correo es obligatorio y debe tener formato valido");
                }


                if (repo.buscarIdUsuario(usuario.Correo) != null)
                {
                    LoggerServicio.getInstancia().Error($"Error al registrase usuario {usuario.Nombre} ya tenia una cuenta");
                    throw new Exception("No se pudo registrar el usuario porque ya existe");
                }


                var _usuario = new RegistroUsuario
                {

                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Edad = usuario.Edad,
                    Cedula = usuario.Cedula,
                    Correo = usuario.Correo,
                    Contraseña = usuario.Contraseña,
                    Sexo = usuario.Sexo,
                    Dia = usuario.Dia,
                    Mes = usuario.Mes,
                    Año = usuario.Año,
                    Rol = false
                };


                LoggerServicio.getInstancia().Info($"El usuario {usuario.Nombre} se registro correctamente fecha:{DateTime.Now}");
                return repo.RegistroUsuario(_usuario);

            }
            catch (Exception ex) 
            {

                throw new Exception("Hubo un error al registrarser el usuario " + ex.Message);
            
            
            }

        }

       

      

        public LoginUsuarioDTO ValidarLoginUsuario(string nombre, string contraseña)
        {

            try
            {
                var usuario = repo.LoginUsuario(nombre, contraseña);

                if (usuario == null)
                {
                    LoggerServicio.getInstancia().Error($"Fallo al intentar registrase usuario {nombre} ingreso mal sus credenciales");
                    throw new Exception("Usuario o contraseña incorrecta");
                }


                if (usuario.Rol && !usuario.Rol)
                {
                    LoggerServicio.getInstancia().Warning("Intento fallido de autenticacion por usuario no registrado");
                    throw new Exception("Error al autenticarte el usuario no es registrado");
                }

                var usuarioLoguiado = new LoginUsuarioDTO
                {    
                    
                    Id = usuario.Id,    
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Correo = usuario.Correo,
                    Rol = usuario.Rol

                };

                LoggerServicio.getInstancia().Info($"El usuario {nombre} se autentico correctamente a las {TimeOnly.FromDateTime(DateTime.Now)} el dia {DateOnly.FromDateTime(DateTime.Today)}");
                return usuarioLoguiado;
            }
            catch (Exception ex) 
            {


                throw new Exception("Hubo un error al logiarse " + ex.Message);
            
            
            }
        }
            
    }














}
    



































