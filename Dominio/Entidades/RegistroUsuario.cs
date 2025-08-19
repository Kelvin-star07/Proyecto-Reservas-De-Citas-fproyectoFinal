using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infraestructura.Modelos;

public partial class RegistroUsuario
{
    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Edad { get; set; }

    public string Cedula { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public int Dia { get; set; }

    public int Mes { get; set; }

    public int Año { get; set; }

    public bool Rol { get; set; } = false;

    public virtual ICollection<ReservaCita> ReservaCita { get; set; } = new List<ReservaCita>();
}
