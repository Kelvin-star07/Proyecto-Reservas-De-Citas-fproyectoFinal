using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Interfaces;
using Infraestructura.Contexto;
using Infraestructura.Modelos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios
{
    public class ReservaCitaRepositorio : IReservaCitaRepositorio
    {

        private readonly ReservaCitasDbContext context;
        private readonly ConfiguracionReservaRepositorio config;



        public ReservaCitaRepositorio(ReservaCitasDbContext Context, ConfiguracionReservaRepositorio config)
        {
            this.context = Context;
            this.config = config;

        }


        //Metodos de accion de usuario


        public ReservaCita GetReservaCita(int IdUsuario)
        {
            try
            {
                var cita = context.ReservaCitas
                    .Where(x => x.IdUsuario == IdUsuario)
                    .OrderByDescending(x => x.Fecha) 
                    .Select(x => new ReservaCita
                    {
                        Fecha = x.Fecha,
                        Turno = x.Turno,
                        Hora = x.Hora,
                        Estado = x.Estado,
                    })
                    .FirstOrDefault(); 

                return cita;
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error al consultar la reserva: " + ex.Message);
            }
        }



        public string ReservarCita(ReservaCita reserva)
        {
            using var transaction = context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable);
            try
            {
                context.ReservaCitas.Add(reserva);
                context.SaveChanges();

                transaction.Commit();
                return "Reserva creada con éxito.";
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();

                if (ex.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    {
                        if (sqlEx.Message.Contains("UQ_Usuario_Fecha"))
                            return "Ya tienes una reserva activa para ese día.";

                        if (sqlEx.Message.Contains("idx_noRepetirEstacion"))
                            return "La estación ya está ocupada en ese horario.";
                    }

                    if (sqlEx.Message.Contains("No hay cupos disponibles"))
                        return "No hay cupos disponibles para ese horario.";
                }
                throw;
            }
        }







        //Metodos de validacion

        public bool CitaActiva(int idUsuario, DateOnly fecha)
        {

            var existe = context.ReservaCitas.FirstOrDefault(x => x.IdUsuario == idUsuario && x.Fecha == fecha);

            if (existe != null)
            {

                return true;

            }
            else
            {
                return false;
            }

        }



        public ConfiguracionReserva obtenerConfiguracionPorTurno(DateOnly fecha, string turno)
        {

            return config.obtenerConfiguracion(fecha, turno);

        }



        public int contarReservasPorSlot(DateOnly fecha, TimeOnly hora, string turno)
        {

            var reservasPorSlot = context.ReservaCitas.Count(x => x.Fecha == fecha && x.Hora == hora && x.Turno == turno);

            if (reservasPorSlot > 0)
            {

                return reservasPorSlot;

            }
            else
            {

                return 0;

            }
        }


        public List<Estacione> obtenerTodasLasEstaciones()
        {

            return context.Estaciones.ToList();

        }


        public List<int> obtenerEstacionesOcupadas(DateOnly fecha, TimeOnly hora, string turno)
        {

            return context.ReservaCitas
                      .Where(x => x.Fecha == fecha && x.Hora == hora && x.Turno == turno)
                      .Select(x => x.IdEstacion)
                      .ToList();

        }



    }
}
