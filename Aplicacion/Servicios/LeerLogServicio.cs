using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Interfaces;

namespace Aplicacion.Servicios
{
    public class LeerLogServicio : ILeerLogServicio
    {



        private static string ruta = "C:\\Users\\LENOVO\\OneDrive\\Documentos\\code\\excepsiones.cs\\asp.net wed api\\capa-presentacion-Reservas\\LoggTXT.txt";



        public  string LeerLogg(string admin)
        {

            try
            {

                string contenido = File.ReadAllText(ruta);

                if (contenido == null)
                {
                    LoggerServicio.getInstancia().Error($"Fallo al intentar leer el log de actividades  por el admin:{admin} en fecha:{DateTime.Now}");
                    throw new Exception("Hubo un error al intentar leer el log");


                }


                LoggerServicio.getInstancia().Info($"El admin '{admin}' consulto el log a las {TimeOnly.FromDateTime(DateTime.Now)} el dia {DateOnly.FromDateTime(DateTime.Today)}");
                return contenido;

            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un error el leer el log de las actividades " + ex.Message);
            }

        }
    }
}
