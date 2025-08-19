using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Configuraciones;
using Aplicacion.DTOs;

namespace Aplicacion.Servicios
{
    public class EnvioGmailServicio 
    {


        private readonly SmtpSettings _smtp;



        public EnvioGmailServicio()
        {


            _smtp = new SmtpSettings();



        }


        public async Task EnviarGmail(string nombre, string correo, ReservaCitasDTO reservaCitaModel)
        {


            if (string.IsNullOrEmpty(correo))
            {

                throw new ArgumentException("No se encontro el correo");
            }


            if (reservaCitaModel == null)
            {
                LoggerServicio.getInstancia().Error($"Fallo al enviar correo a {correo} la reserva no llego a registrarse");
                throw new Exception("La reserva aun no se registrado");
            }

            _smtp.Server = "smtp.gmail.com";
            _smtp.Port = 587;
            _smtp.UserName = "kodakjoefabricacitas@gmail.com";
            _smtp.SenderEmail = "kodakjoefabricacitas@gmail.com";
            _smtp.SenderName = "FabricaCitas";
            _smtp.Password = "aiwj padb mtrz atbd";






            string asunto = "Confirmación de cita";

            string cuerpo = $""""
                            
                            Estimado/a {nombre},

                            Le agradecemos por realizar su solicitud de reserva de cita a través de nuestro sistema. Su solicitud ha sido recibida correctamente
                            y está en proceso de revisión.

                            Le informamos que en breve recibirá una confirmación oficial con los detalles finales de su cita. Mientras tanto, le pedimos mantener
                            disponible el día y la hora solicitados para asegurar la mejor atención.

                            Si tiene alguna consulta o necesita realizar algún cambio, no dude en contactarnos.

                            Agradecemos su confianza y quedamos atentos a cualquier comunicación.

                            Datos de la Cita
                            Fecha:{reservaCitaModel.Fecha}
                            Hora: {reservaCitaModel.Hora}
                            Estado:{reservaCitaModel.Estado}

                            Atentamente,
                            Fabrica Citas
                            {_smtp.SenderEmail}
                            """";




            using (var cliente = new SmtpClient(_smtp.Server, _smtp.Port))
            {
                cliente.Credentials = new NetworkCredential(_smtp.UserName, _smtp.Password);
                cliente.EnableSsl = true;

                var mensaje = new MailMessage(_smtp.SenderEmail, correo, asunto, cuerpo);

                LoggerServicio.getInstancia().Info($"Correo enviado sastifactoriamente a '{correo}' despues de reservar su cita");
                await cliente.SendMailAsync(mensaje);
            }
        }













    }
}
