using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Infraestructura.Modelos;

namespace Aplicacion.Interfaces
{
    public interface IReservaCitasServicio
    {


        Task<string> ReservarCita(string correo, string nombre, int idUsuario,ReservaCitasDTO reserva);


        ReservaCitasDTO GetReservaCita(string nombre,int idUsuario);


      

    }
}
