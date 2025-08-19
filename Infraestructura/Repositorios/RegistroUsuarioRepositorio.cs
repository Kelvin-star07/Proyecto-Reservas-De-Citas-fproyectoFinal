using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Interfaces;
using Infraestructura.Contexto;
using Infraestructura.Modelos;

namespace Infraestructura.Repositorios
{
    public class RegistroUsuarioRepositorio : IRegistroUsuarioRepositorio
    {
        private readonly ReservaCitasDbContext context;


        public RegistroUsuarioRepositorio(ReservaCitasDbContext context)
        {

            this.context = context;

        }


        public RegistroUsuario LoginUsuario(string nombre, string contraseña)
        {
            var usuario = context.RegistroUsuarios
                       .FirstOrDefault(u => u.Nombre == nombre && u.Contraseña == contraseña);

            if (usuario == null) {

                throw new Exception("El usuario no esta registrados");
            }

            return usuario;
        }


        public string RegistroUsuario(RegistroUsuario usuario)
        {
            context.RegistroUsuarios.Add(usuario);
            context.SaveChanges();

            return "Registrado correctamente";
        }





        public int? buscarIdUsuario(string correo)
        {

            var user = context.RegistroUsuarios.FirstOrDefault(u => u.Correo == correo);

            if (user == null)
            {

                return null;
            }

            return user.Id;

        }

        public bool BuscarUsuario(int id)
        {

            var usuario = context.RegistroUsuarios.FirstOrDefault(x => x.Id == id);

            if (usuario == null)
            {

                return false;

            }

            return true;

        }




    }
}
