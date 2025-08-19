using Aplicacion.DTOs;
using Aplicacion.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion_reservas_citasss.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GeneracionSlotController : ControllerBase
    {


        private readonly GeneracionSlotServicio servicio;


        public GeneracionSlotController(GeneracionSlotServicio servicio)
        {

           this.servicio = servicio;

        }


        [HttpGet("obtener-slots")]
        public IActionResult ObtenerSlots(DateOnly fecha, string turno)
        {

            try
            {
                var slots = servicio.GenerarSlots(fecha, turno);

                if (slots == null || !slots.Any())
                    return NotFound("No se encontró configuración o no hay slots.");


                return Ok(slots);
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al generaser los slots " + ex.Message);

            }
        }
















    }
}
