using Aplicacion.DTOs;
using System.Security.Claims;
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
    public class EstacionController : ControllerBase
    {


        private readonly EstacionServicio servicio;


        public EstacionController(EstacionServicio servicio) 
        {
           this.servicio = servicio;
        
        }



        [HttpPost("Agregar-Estacion")]
        public string AgregarEstacion(EstacionDTO estacion)
        {


            try
            {
                var nombre = User.FindFirstValue("Nombre");
                if (nombre == null)
                {
                    throw new Exception("Hubo un error al confirmar el nombre del admin");
                }



                return servicio.agregarEstacion(estacion,nombre);

            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error  al agregar la estacion " + ex.Message);

            }




        }



        [HttpPut("Modificar-estacion")]
        public string ModificarEstacion(EstacionDTO estacion)
        {
            try
            {

                var nombre = User.FindFirstValue("Nombre");
                if (nombre == null)
                {
                    throw new Exception("Hubo un error al confirmar el nombre del admin");
                }
          
                return servicio.ModificarEstacion(estacion,nombre);


            }
            catch (Exception ex)
            {



                throw new Exception("Hubo un error al editar la estacion " + ex.Message);




            }




        }



        [HttpGet("Obtener-estaciones")]
        public List<EstacionDTO> obtenerEstaciones()
        {
            try
            {

                var nombre = User.FindFirstValue("Nombre");
                if (nombre == null)
                {
                    throw new Exception("Hubo un error al confirmar el nombre del admin");
                }

                return servicio.ObtenerEstaciones(nombre);
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al obtener la estaciones " + ex.Message);

            }


        }












    }
}
