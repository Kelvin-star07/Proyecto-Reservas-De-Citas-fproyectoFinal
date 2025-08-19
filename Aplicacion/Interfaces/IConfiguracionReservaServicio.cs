using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Infraestructura.Modelos;

namespace Aplicacion.Interfaces
{
    public interface IConfiguracionReservaServicio
    {


        string crearConfiguracion(ConfiguracionDTO configuracion,string admin);
        string actualizarConfiguracion(ConfiguracionDTO reserva,string nombre);

        ConfiguracionDTO obtenerConfiguracion(DateOnly fecha, string turno,string nombre);


    }
}
