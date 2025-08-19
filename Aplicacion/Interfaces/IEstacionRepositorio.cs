using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructura.Modelos;

namespace Aplicacion.Interfaces
{
    public interface IEstacionRepositorio
    {
        string agregarEstacion(Estacione estacion);


        string ModificarEstacion(Estacione estacion);


        List<Estacione> ObtenerEstaciones();


    }
}
