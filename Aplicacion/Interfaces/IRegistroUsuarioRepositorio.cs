using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructura.Modelos;

namespace Aplicacion.Interfaces
{
    public interface IRegistroUsuarioRepositorio
    {

        string RegistroUsuario(RegistroUsuario usuario);

        RegistroUsuario LoginUsuario(string nombre, string contraseña);


        bool BuscarUsuario(int id);

        int? buscarIdUsuario(string correo);


    }
}
