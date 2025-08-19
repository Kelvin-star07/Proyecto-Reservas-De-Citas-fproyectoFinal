using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infraestructura.Modelos;

public partial class ConfiguracionReserva
{
    [Key]
    public int Id { get; set; }

    public DateOnly Fecha { get; set; }

    public string Turno { get; set; } = null!;

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public int DuracionCitas { get; set; }

    public int CantidadEstaciones { get; set; }
}
