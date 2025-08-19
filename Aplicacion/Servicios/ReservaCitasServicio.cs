using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Infraestructura.Modelos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplicacion.Servicios
{
    public class ReservaCitasServicio : IReservaCitasServicio
    {

        private readonly IReservaCitaRepositorio _config;
        private readonly IConfiguracionReservaRepositorio repo;
        private readonly GeneracionSlotServicio servicio;
        private readonly EnvioGmailServicio envioGmail;


        public ReservaCitasServicio(IReservaCitaRepositorio config, IConfiguracionReservaRepositorio repo, GeneracionSlotServicio servicio, EnvioGmailServicio envioGmail)
        {
            this._config = config;
            this.repo = repo;
            this.servicio = servicio;
            this.envioGmail = envioGmail;

        }



        public async Task<string> ReservarCita(string correo, string nombre, int idUsuario, ReservaCitasDTO reservaModel)
        {
            try
            {
                if (string.IsNullOrEmpty(reservaModel.Turno))
                    throw new ArgumentException("El campo turno no puede estar vacío");

                if (reservaModel.Turno != "Matutino" && reservaModel.Turno != "Vespertino")
                    throw new ArgumentException("El turno debe ser válido");

                if (reservaModel.Fecha < DateOnly.FromDateTime(DateTime.Now))
                    throw new ArgumentException("La fecha no puede ser menor a la fecha actual");

                if (_config.CitaActiva(idUsuario, reservaModel.Fecha))
                    throw new ArgumentException("Ya tiene una reserva activa");

                var config = _config.obtenerConfiguracionPorTurno(reservaModel.Fecha, reservaModel.Turno);
                if (config == null)
                    throw new ArgumentException("Configuración no encontrada");

                var slotsDisponibles = servicio.GenerarSlots(reservaModel.Fecha, reservaModel.Turno);

                var horaInput = TimeOnly.ParseExact(reservaModel.Hora, "HH:mm", CultureInfo.InvariantCulture);
                var slotSeleccionado = slotsDisponibles
                    .FirstOrDefault(s => s.HoraInicio == horaInput);

                if (slotSeleccionado == null)
                    throw new ArgumentException("El horario seleccionado no es válido.");

                if (slotSeleccionado.CupoDisponible <= 0)
                    throw new ArgumentException("No hay cupos para ese horario.");

                var fechaHoraActual = DateTime.Now;
                var fechaHoraSlot = reservaModel.Fecha.ToDateTime(slotSeleccionado.HoraInicio);

                if (fechaHoraActual > fechaHoraSlot) 
                    throw new ArgumentException("La hora ya está pasada, elige un horario a futuro");

                var reservasSlot = _config.contarReservasPorSlot(reservaModel.Fecha, horaInput, reservaModel.Turno);
                if (reservasSlot >= config.CantidadEstaciones)
                {
                    LoggerServicio.getInstancia().Error($"Fallo  reserva por usuario {nombre} no habia cupos a las {DateOnly.FromDateTime(DateTime.Now)}");
                    throw new ArgumentException("No hay cupos para ese horario");
                }

                var estacionesOcupadas = _config.obtenerEstacionesOcupadas(reservaModel.Fecha, horaInput, reservaModel.Turno);
                var todasLasEstaciones = _config.obtenerTodasLasEstaciones();
                var estacionLibre = todasLasEstaciones.FirstOrDefault(e => !estacionesOcupadas.Contains(e.Id));
                if (estacionLibre == null)
                {
                    LoggerServicio.getInstancia().Error($"Fallo  reserva por usuario {nombre} no habia estaciones libres {DateOnly.FromDateTime(DateTime.Now)}");
                    throw new ArgumentException("No hay estaciones libres");
                }

                var nuevaReserva = new ReservaCita
                {
                    IdEstacion = estacionLibre.Id,
                    IdUsuario = idUsuario,
                    Turno = reservaModel.Turno,
                    Hora = horaInput,
                    Fecha = reservaModel.Fecha
                };

                _config.ReservarCita(nuevaReserva);
                await envioGmail.EnviarGmail(nombre, correo, reservaModel);

                LoggerServicio.getInstancia().Info($"Usuario {nombre} reservó una cita para {nuevaReserva.Fecha} (turno:{nuevaReserva.Turno})");
                return "Reserva registrada correctamente";
            }
            catch (Exception ex)
            {
                LoggerServicio.getInstancia().Error($"Hubo un error al registrar una reserva por {nombre} para {reservaModel.Fecha}");
                return "Hubo un error al registrar la reserva: " + ex.Message;
            }
        }





        public ReservaCitasDTO GetReservaCita(string nombre, int idUsuario)
        {
            try
            {
                if (idUsuario < 0)
                    throw new ArgumentException("El id no puede ser negativo");

                var cita = _config.GetReservaCita(idUsuario);

                if (cita == null)
                {
                    LoggerServicio.getInstancia().Error($"Fallo al consultar reserva por usuario {nombre} no tenía cita a las {DateOnly.FromDateTime(DateTime.Now)}");
                    throw new ArgumentException("No tiene cita reservada");
                }

                LoggerServicio.getInstancia().Info($"El usuario {nombre} consultó su cita en fecha: {DateTime.Now}");

                var hora = cita.Hora.HasValue ? cita.Hora.Value.ToString("HH:mm") : null;

                if( hora == null )
                {
                    throw new Exception("Hubo un error la registrar la hora, no puede ser null");
                }

                return new ReservaCitasDTO
                {
                    Fecha = cita.Fecha,
                    Turno = cita.Turno,
                    Hora = hora,
                    Estado = "Pendiente"
                };
            }
            catch (Exception ex)
            {
                LoggerServicio.getInstancia().Error($"Error al obtener la cita de {nombre}: {ex.Message}");
                throw;
            }
        }














    }
}

