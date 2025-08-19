using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Infraestructura.Modelos;

namespace Aplicacion.Interfaces
{
    public interface IEstacionServicio
    {
        string agregarEstacion(EstacionDTO estacion,string admin);
        string ModificarEstacion(EstacionDTO estacion,string admin);
        List<EstacionDTO> ObtenerEstaciones(string admin);

    }

}
