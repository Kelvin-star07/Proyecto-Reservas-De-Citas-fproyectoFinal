using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class LoggerServicio
    {
        private readonly static string ruta = "C:\\Users\\LENOVO\\OneDrive\\Documentos" +
                                              "\\code\\excepsiones.cs\\asp.net wed api\\" +
                                               "capa-presentacion-Reservas\\LoggTXT.txt";
        private readonly static object _lockControl = new object();

        private static LoggerServicio? Instacia = null;


        private LoggerServicio() { }


        public static LoggerServicio getInstancia()
        {

            if (Instacia == null)
            {

                lock (_lockControl)
                {

                    if (Instacia == null)
                    {

                        Instacia = new LoggerServicio();




                    }

                }

            }

            return Instacia;
        }




        public void Error(string mensaje)
        {

            EscribirLogg("Error", mensaje);

        }



        public void Info(string mensaje)
        {

            EscribirLogg("Info", mensaje);

        }



        public void Modificacion(string mensaje)
        {

            EscribirLogg("Modificacion", mensaje);

        }


        public void Warning(string mensaje)
        {
            EscribirLogg("Warning", mensaje);

        }



        private void EscribirLogg(string tipo, string mensaje)
        {

            lock (_lockControl)
            {

                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string actividad = $"{date} ---> {tipo}:{mensaje}";
                File.AppendAllText(ruta, actividad + Environment.NewLine);
            }

        }




    }
}
