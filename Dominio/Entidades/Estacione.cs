using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infraestructura.Modelos;

public partial class Estacione
{
    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ReservaCita> ReservaCita { get; set; } = new List<ReservaCita>();
}
