using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class LoginUsuarioDTO
    {
        
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo {  get; set; } = string.Empty; 
        public bool Rol { get; set; } = false;

    }
}
