using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infraestructura.Modelos;

public partial class ReservaCita
{
    [Key]
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdEstacion { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly? Hora { get; set; }

    public string Turno { get; set; } = null!;

    [DefaultValue("Pendiente")]
    public string Estado { get; set; } = "Pendiente";

    public virtual Estacione IdEstacionNavigation { get; set; } = null!;

    public virtual RegistroUsuario IdUsuarioNavigation { get; set; } = null!;
}
