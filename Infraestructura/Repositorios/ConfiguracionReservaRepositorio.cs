using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Interfaces;
using Infraestructura.Contexto;
using Infraestructura.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios
{
    public class ConfiguracionReservaRepositorio : IConfiguracionReservaRepositorio
    {


        private readonly ReservaCitasDbContext context;
       


        public ConfiguracionReservaRepositorio(ReservaCitasDbContext context)
        {


            this.context = context;


        }




        public string crearConfiguracion(ConfiguracionReserva configuracion)
        {
            try
            {
                context.ConfiguracionReservas.Add(configuracion);
                context.SaveChanges();
                return "Cofiguracion creada exitosamente";
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al crear la configuacion " + ex.Message);

            }
        }



        public string actualizarConfiguracion(ConfiguracionReserva reserva)
        {
            try
            {
                context.ConfiguracionReservas.Entry(reserva).State = EntityState.Modified;
                context.SaveChanges();

                return "Cofiguracion actualizada correctamente";
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al modificar la configuracion " + ex.Message);

            }





        }



        public ConfiguracionReserva obtenerConfiguracion(DateOnly fecha, string turno)
        {

            try
            {


                var configuracion = context.ConfiguracionReservas.FirstOrDefault(x => x.Fecha == fecha && x.Turno == turno);

                if(configuracion == null)
                {
                    throw new Exception("Hubo un error al obtener la configuracion");
                }

                return configuracion;
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error al obtener la configuracion " + ex.Message);

            }





    }
    }
}
