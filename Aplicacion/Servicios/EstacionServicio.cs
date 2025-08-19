using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Infraestructura.Modelos;

namespace Aplicacion.Servicios
{
    public class EstacionServicio : IEstacionServicio
    {

        private readonly IEstacionRepositorio repo;



        public EstacionServicio(IEstacionRepositorio repo)
        {
            this.repo = repo;
        }




        public string agregarEstacion(EstacionDTO estacion, string admin)
        {

            try
            {
                if (estacion == null)
                {
                    LoggerServicio.getInstancia().Error($"Fallo al agregar nueva estacion por admin:{admin} no indico el nombre de la estacion");
                    throw new ArgumentNullException("No se indico una estacion para agregar");

                }



                var nuevaEstacion = new Estacione
                {

                    Nombre = estacion.Nombre,

                };

                LoggerServicio.getInstancia().Info($"Se agrego una nueva estacion por el admin:{admin}, llamada {nuevaEstacion}");
                return repo.agregarEstacion(nuevaEstacion);
            }
            catch (Exception ex)
            {

                return "Hubo un error al agregar la estacion " + ex.Message;


            }

        }

        public string ModificarEstacion (EstacionDTO estacion,string admin)
        {

            try
            {
                if (estacion == null)
                {
                    LoggerServicio.getInstancia().Error($"Error al modificar estacion el admin:{admin} no indico cual estacion modificar");
                    throw new ArgumentNullException("No se indico una estacion para modificar");
                }

                if (estacion.Id < 0)
                {
                    LoggerServicio.getInstancia().Error($"Error al modificar estacion por el admin:{admin} no se encontro la estacion a modificar");
                    throw new ArgumentException("El id debe ser positivo");
                }

                var estacionEditada = new Estacione
                {
                    Id = estacion.Id,   
                    Nombre = estacion.Nombre,

                };

                LoggerServicio.getInstancia().Modificacion($"Estacion modificada exitosamente por admin:{admin}");
                return repo.ModificarEstacion(estacionEditada);
                

            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al modificar la estacion" + ex.Message);

            }

        }





        public List<EstacionDTO> ObtenerEstaciones(string admin)
        {
            try
            {

                var estaciones = repo.ObtenerEstaciones();

                if (estaciones == null)
                {

                    LoggerServicio.getInstancia().Error($"Fallo al consultar las estaciones el admin:{admin} no encontro estaciones registrada");
                    throw new Exception("No hay estaciones para registrar");

                }


                LoggerServicio.getInstancia().Info($"El admin '{admin}' consulto sastifactoriamente la estaciones existente");

                return repo.ObtenerEstaciones().Select(o => new EstacionDTO {

                     Id = o.Id,
                     Nombre = o.Nombre

                }).ToList();
                    

               
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al consultas la estaciones" + ex.Message);

            }









        }
    }
}
