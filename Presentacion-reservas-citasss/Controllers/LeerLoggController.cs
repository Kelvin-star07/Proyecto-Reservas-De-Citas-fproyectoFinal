using System.Runtime.CompilerServices;
using System.Security.Claims;
using Aplicacion.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion_reservas_citasss.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeerLoggController : ControllerBase
    {

        private readonly LeerLogServicio servicio;



        public LeerLoggController(LeerLogServicio servicio)
        {

            this.servicio = servicio;
        
        }



        [HttpGet("consultar-logg")]
        public string getLoogActividades() 
        {
            try
            {
                var nombre = User.FindFirstValue("Nombre");
                if (string.IsNullOrEmpty(nombre))
                {
                    throw new Exception("hubo un error al consultar el nombre del admin");
                }

                return servicio.LeerLogg(nombre);
                


            }catch(Exception ex) 
            {

                throw new Exception("Hubo un error al consultar el logg de actividades " + ex.Message);
            
            
            
            }
        
    
        }








    }
}
