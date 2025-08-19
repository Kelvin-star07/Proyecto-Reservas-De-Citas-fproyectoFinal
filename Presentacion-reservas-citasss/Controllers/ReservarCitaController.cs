using Aplicacion.DTOs;
using Infraestructura.Modelos;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.Servicios;

namespace Presentacion_reservas_citasss.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservarCitaController : ControllerBase
    {


        private readonly ReservaCitasServicio servicio;



        public ReservarCitaController(ReservaCitasServicio servicio)
        {
            this.servicio = servicio;
       }


        [HttpPost("Registra-Reserva")]
        public async Task<IActionResult> ReservarCita([FromBody] ReservaCitasDTO dTO)
        {
            try
            {
                var correo = User.FindFirstValue("Correo");
                if(correo == null)
                    throw new Exception("Correo no encontrado");


                var nombre = User.FindFirstValue("Nombre");
                  if(nombre == null)
                    throw new Exception("Nombre no encontrado");

                int idUsuario = int.Parse(User.FindFirstValue("id"));
                    if(idUsuario <=0)
                    throw new Exception("Usuario no autenticado");

                var resultado = await servicio.ReservarCita(correo, nombre, idUsuario, dTO);

                if (resultado is string msg && msg.Contains("Ya tiene una reserva activa"))
                    return BadRequest(new { message = msg });

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest( new { message = ex.Message });
            }
        }





        [HttpGet("Ver-mi-Reserva")]
        public IActionResult VerReserva()
        {
            try
            {
                int idUsuario = int.Parse(User.FindFirstValue("id") ?? throw new Exception("Usuario no autenticado"));
                string nombre = User.FindFirstValue("Nombre") ?? throw new Exception("Nombre no encontrado");

                var cita = servicio.GetReservaCita(nombre, idUsuario);
                if (cita == null)
                    return NotFound("No tiene reservas activas");

                return Ok(cita);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
