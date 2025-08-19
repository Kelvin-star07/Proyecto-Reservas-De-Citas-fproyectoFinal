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
    public class GeneracionSlotRepositorio : IGeneracionSlotsRepositorio
    {



        private readonly ReservaCitasDbContext context;



        public GeneracionSlotRepositorio(ReservaCitasDbContext  context)
        {

            this.context = context;
        }


        public ConfiguracionReserva obtenerConfiguracion(DateOnly fecha, string turno)
        {
            var configuracion = context.ConfiguracionReservas.FirstOrDefault(x => x.Fecha == fecha && x.Turno == turno);
            return configuracion;

        }



        public List<ReservaCita> ObtenerReservasPorFechaYTurno(DateOnly fecha, string turno)
        {

            return context.ReservaCitas
                .Where(r => r.Fecha == fecha && r.Turno == turno)
                .ToList();
        }



    }
}
