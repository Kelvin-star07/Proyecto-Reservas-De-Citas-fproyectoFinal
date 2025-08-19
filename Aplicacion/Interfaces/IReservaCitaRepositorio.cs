using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructura.Modelos;

namespace Aplicacion.Interfaces
{
    public interface IReservaCitaRepositorio
    {
        string ReservarCita(ReservaCita reserva);

        ReservaCita GetReservaCita(int idUsario);

        bool CitaActiva(int idUsuario, DateOnly fecha);

        int contarReservasPorSlot(DateOnly fecha, TimeOnly hora, string turno);

        ConfiguracionReserva  obtenerConfiguracionPorTurno(DateOnly fecha, string turno);

        List<Estacione> obtenerTodasLasEstaciones();

        List<int> obtenerEstacionesOcupadas(DateOnly fecha, TimeOnly hora, string turno);

    }
}
