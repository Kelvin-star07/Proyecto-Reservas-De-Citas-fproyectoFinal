using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Infraestructura.Modelos;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplicacion.Servicios
{
    public class ConfiguracionReservaServicio : IConfiguracionReservaServicio
    {

        private readonly IConfiguracionReservaRepositorio repo;



        public ConfiguracionReservaServicio(IConfiguracionReservaRepositorio repo)
        {

            this.repo = repo;

        }




        public string crearConfiguracion(ConfiguracionDTO configuracion, string admin)
        {
                  
            try
            {
                if (configuracion.Fecha < DateOnly.FromDateTime(DateTime.Now))
                    throw new Exception("La fecha no puede ser menor al dia actual");

                if (configuracion.CantidadEstaciones < 0)
                {
                    LoggerServicio.getInstancia().Error($"Fallo a crear configuracion por el admin:{admin}, no indico la cantidad de estaciones");
                    throw new Exception("La cantidad de estaciones debe ser positiva");
                }

                if (configuracion.DuracionCitas <= 0)
                {
                    LoggerServicio.getInstancia().Error($"Fallo a crear configuracion por el admin:{admin}, no indico la duracion de la cita");
                    throw new Exception("La duracion de las citas debe ser positiva y mayor que 0");
                }


                var horaInicio = TimeOnly.ParseExact(configuracion.HoraInicio, "HH:mm", CultureInfo.InvariantCulture);
                var horaFin = TimeOnly.ParseExact(configuracion.HoraFin, "HH:mm", CultureInfo.InvariantCulture);


                if (horaFin <= horaInicio)
                    throw new Exception("La hora de fin debe ser mayor a la hora inicio");

                var duracionTotal = horaFin.ToTimeSpan() - horaInicio.ToTimeSpan();

                if (duracionTotal.TotalMinutes < configuracion.DuracionCitas)
                    throw new Exception("El rango horario debe tener exactamente la diferencia de la duracion de la cita.");


                if (string.IsNullOrEmpty(configuracion.Turno))
                    throw new Exception("El campo turno es obligatorio");

                if (configuracion.Turno != "Matutino" && configuracion.Turno != "Vespertino")
                {
                    LoggerServicio.getInstancia().Error($"Fallo al crear configuracion el admin:{admin} no indico un valor correcto para el turno");
                    throw new Exception("El turno debe tener un valor correcto");
                }


                var horaMinMatutino = new TimeOnly(6, 0);
                var horaMaxMatutino = new TimeOnly(12, 0);

                var horaMinVespertino = new TimeOnly(13, 0);
                var horaMaxVespertino = new TimeOnly(18, 0);

                if (configuracion.Turno == "Matutino")
                {
                    if (horaInicio < horaMinMatutino || horaFin > horaMaxMatutino)
                    {
                        LoggerServicio.getInstancia().Error($"Fallo al crear configuracion el admin:{admin} no indico valores correcto para el horario matutino");
                        throw new Exception("El horario para el turno Matutino debe estar entre 6:00 y 12:00.");
                    }
                }


                else if (configuracion.Turno == "Vespertino")
                {
                    if (horaInicio < horaMinVespertino || horaFin > horaMaxVespertino)
                    {
                        LoggerServicio.getInstancia().Error($"Fallo al crear configuracion el admin:{admin} no indico valores correcto para el horario vesrpertino");
                        throw new Exception("El horario para el turno Vespertino debe estar entre 13:00 y 18:00.");
                    }
                }


              


                var Newconfiguracion = new ConfiguracionReserva
                {
                    Fecha = configuracion.Fecha,
                    Turno = configuracion.Turno,
                    HoraInicio = horaInicio,
                    HoraFin = horaFin,
                    DuracionCitas = configuracion.DuracionCitas,
                    CantidadEstaciones = configuracion.CantidadEstaciones

                };




                LoggerServicio.getInstancia().Info($"Configuracion creada exitosamente  por el admin:{admin}");
                return repo.crearConfiguracion(Newconfiguracion);
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al crearse la confiuracion " + ex.Message);
            }
        }





        public string actualizarConfiguracion(ConfiguracionDTO config, string nombre)
        {
            try
            {
               
                if (config.Fecha < DateOnly.FromDateTime(DateTime.Now))
                    throw new Exception("La fecha no puede ser menor al día actual");

                if (config.CantidadEstaciones < 0)
                {
                    LoggerServicio.getInstancia().Error($"Fallo al modificar configuración por admin:{nombre}, cantidad de estaciones no válida");
                    throw new Exception("La cantidad de estaciones debe ser positiva");
                }

                if (config.DuracionCitas <= 0)
                {
                    LoggerServicio.getInstancia().Error($"Fallo al modificar configuración por admin:{nombre}, duración de citas no válida");
                    throw new Exception("La duración de las citas debe ser mayor que 0");
                }

              
                var horaInicio = TimeOnly.ParseExact(config.HoraInicio, "HH:mm", CultureInfo.InvariantCulture);
                var horaFin = TimeOnly.ParseExact(config.HoraFin, "HH:mm", CultureInfo.InvariantCulture);

                if (horaFin <= horaInicio)
                    throw new Exception("La hora de fin debe ser mayor a la hora de inicio");

                var duracionTotal = horaFin.ToTimeSpan() - horaInicio.ToTimeSpan();
                if (duracionTotal.TotalMinutes < config.DuracionCitas)
                    throw new Exception("El rango horario debe ser mayor o igual a la duración de la cita.");

                if (string.IsNullOrEmpty(config.Turno))
                    throw new Exception("El campo turno es obligatorio");

                if (config.Turno != "Matutino" && config.Turno != "Vespertino")
                {
                    LoggerServicio.getInstancia().Error($"Fallo al modificar configuración por admin:{nombre}, turno no válido");
                    throw new Exception("El turno debe ser Matutino o Vespertino");
                }

                var horaMinMatutino = new TimeOnly(6, 0);
                var horaMaxMatutino = new TimeOnly(12, 0);
                var horaMinVespertino = new TimeOnly(13, 0);
                var horaMaxVespertino = new TimeOnly(18, 0);

                if ((config.Turno == "Matutino" && (horaInicio < horaMinMatutino || horaFin > horaMaxMatutino)) ||
                    (config.Turno == "Vespertino" && (horaInicio < horaMinVespertino || horaFin > horaMaxVespertino)))
                {
                    LoggerServicio.getInstancia().Error($"Fallo al modificar configuración por admin:{nombre}, horario fuera del rango del turno");
                    throw new Exception($"El horario para el turno {config.Turno} debe estar dentro del rango permitido.");
                }

              
                var configExistente = repo.obtenerConfiguracion(config.Fecha, config.Turno);
                if (configExistente == null)
                    throw new Exception("No se encontró configuración para la fecha y turno seleccionados");

               
                configExistente.HoraInicio = horaInicio;
                configExistente.HoraFin = horaFin;
                configExistente.DuracionCitas = config.DuracionCitas;
                configExistente.CantidadEstaciones = config.CantidadEstaciones;

                LoggerServicio.getInstancia().Modificacion($"Configuración actualizada por admin:{nombre} turno:{config.Turno} hora fin:{config.HoraFin}");

                return repo.actualizarConfiguracion(configExistente);
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error al modificar la configuración: " + ex.Message);
            }
        }






        public ConfiguracionDTO obtenerConfiguracion(DateOnly fecha, string turno, string nombre)
        {

            try
            {

                if (fecha < DateOnly.FromDateTime(DateTime.Now))
                {
                    LoggerServicio.getInstancia().Error($"Fallo al consultar una configuracion el admin:{nombre} no indico una fecha correcta");
                    throw new ArgumentException("La fecha no puede ser menor a la fecha actual");
                }

                if (turno != "Matutino" && turno != "Vespertino")
                {
                    LoggerServicio.getInstancia().Error($"Fallo al consultar una configuracion el admin:{nombre} no indico unt turno correcto");
                    throw new ArgumentException("Turno no valido");
                }


                var config = repo.obtenerConfiguracion(fecha,turno);
                 


                LoggerServicio.getInstancia().Info($"configuracion consultada correctamente por admin:{nombre} turno:{turno} fecha:{fecha}");
                return new ConfiguracionDTO
                {   
                    Fecha = config.Fecha,
                    Turno = config.Turno,
                    HoraInicio = config.HoraInicio.ToString("HH:mm"), 
                    HoraFin = config.HoraFin.ToString("HH:mm"),   
                    DuracionCitas = config.DuracionCitas,
                    CantidadEstaciones = config.CantidadEstaciones
                };

            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al consultar la configuracion " + ex.Message);


            }


















        }
    }































}


