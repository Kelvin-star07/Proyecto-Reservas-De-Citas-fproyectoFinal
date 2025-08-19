using System.Globalization;
using System.Security.Claims;
using Aplicacion.DTOs;
using Aplicacion.Servicios;
using Infraestructura.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion_reservas_citasss.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionReservaController : ControllerBase
    {



        private readonly ConfiguracionReservaServicio servicio;




        public ConfiguracionReservaController(ConfiguracionReservaServicio servicio)
        {
        
            this.servicio = servicio;   
        
        
        }



        [HttpPost("Crear-configuracion")]
        public string CrearConfiguracion([FromBody] ConfiguracionDTO configuracion)
        {
            try
            {
                var nombre = User.FindFirstValue("Nombre");
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Error al encontral el nombre del admin");



              
                return servicio.crearConfiguracion(configuracion, nombre);

            }
            catch (Exception ex)
            {

                return "Hubo un error al crear la configuracion " + ex.Message;

            }

        }



        [HttpPut("Modificar-configuracion")]
        public string ModificarConfiguracion([FromBody] ConfiguracionDTO config)
        {
            try
            {
                var nombre = User.FindFirstValue("Nombre");
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Error al encontral el nombre del admin");


                return servicio.actualizarConfiguracion(config,nombre);
            }
            catch (Exception ex)
            {

                return "Hubo un error al modificar la configuracion " + ex.Message;

            }

        }



        [HttpGet("Obtener-configuracion")]
        public ActionResult<ConfiguracionDTO> GetConfiguracion(DateOnly fecha, string turno)
        {

            try
            {

                var nombre = User.FindFirstValue("Nombre");
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Error al encontral el nombre del admin");


                var config = servicio.obtenerConfiguracion(fecha,turno,nombre); 
                return Ok(config);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);


            }

        }








    }
}
