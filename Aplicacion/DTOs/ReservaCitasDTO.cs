using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class ReservaCitasDTO
    {   


        public DateOnly Fecha { get; set; }

        public string Hora { get; set; } 

        public string Turno { get; set; } = null!;


        public string Estado = "Pendiente";

    }
}
