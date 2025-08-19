using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Aplicacion.Interfaces;

namespace Aplicacion.Servicios
{
    public class GeneracionSlotServicio : IGeneracionSlotServicio
    {

        private readonly IGeneracionSlotsRepositorio repo;



        public GeneracionSlotServicio(IGeneracionSlotsRepositorio repo) 
        {
           
             this.repo = repo;
        
        }






        public List<SlotDTO> GenerarSlots(DateOnly fecha, string turno)
        {


            try
            {   
                var config = repo.obtenerConfiguracion(fecha, turno);
                if (config == null)
                    throw new ArgumentException($"No se encontró configuración para fecha {fecha} y turno {turno}");
                   

                var reservas = repo.ObtenerReservasPorFechaYTurno(fecha, turno);

                var slots = new List<SlotDTO>();
                var horaActual = config.HoraInicio;

                while (horaActual < config.HoraFin)
                {
                    var slotFin = horaActual.AddMinutes(config.DuracionCitas);
                    var reservasCount = reservas.Count(r => r.Hora == horaActual);

                    slots.Add(new SlotDTO
                    {
                        HoraInicio = horaActual,
                        HoraFin = slotFin,
                        CupoDisponible = config.CantidadEstaciones - reservasCount
                    });

                    horaActual = slotFin;
                }

                return slots;
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al generalse los slots " + ex.Message);


            }



        }
    }
}
