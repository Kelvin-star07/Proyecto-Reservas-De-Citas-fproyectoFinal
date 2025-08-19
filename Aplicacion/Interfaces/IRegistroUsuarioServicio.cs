using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Infraestructura.Modelos;

namespace Aplicacion.Interfaces
{
    public interface IRegistroUsuarioServicio
    {

        string AgregarUsuario(RegistroUsuarioDTO usuario);

        LoginUsuarioDTO ValidarLoginUsuario(string nombre, string contraseña);



    }
}
