using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class ConfiguracionDTO
    { 

      
        public DateOnly Fecha { get; set; }

        public string Turno { get; set; } = null!;

        public string HoraInicio { get; set; }  = string.Empty; 

        public string HoraFin { get; set; } = string.Empty;

        public int DuracionCitas { get; set; }

        public int CantidadEstaciones { get; set; }

    }
}
