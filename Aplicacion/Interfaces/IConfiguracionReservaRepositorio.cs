using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructura.Modelos;

namespace Aplicacion.Interfaces
{
    public interface IConfiguracionReservaRepositorio
    {
        string crearConfiguracion(ConfiguracionReserva configuracion);
        string actualizarConfiguracion(ConfiguracionReserva reserva);

        ConfiguracionReserva obtenerConfiguracion(DateOnly fecha, string turno);

    }
}
