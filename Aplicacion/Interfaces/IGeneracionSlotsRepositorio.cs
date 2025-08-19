using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructura.Modelos;

namespace Aplicacion.Interfaces
{
    public interface IGeneracionSlotsRepositorio 
    {

        ConfiguracionReserva obtenerConfiguracion(DateOnly fecha, string turno);
        List<ReservaCita> ObtenerReservasPorFechaYTurno(DateOnly fecha, string turno);

    }


}
