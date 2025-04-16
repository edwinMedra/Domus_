using System.Net.Mail;

namespace Domus.Pages.Class
{
	public class ProcesosComunes
	{





		public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
		{
			try
			{
				string correoOrigen = "estudiante20230260@cdb.edu.sv";
				string ContraseñaOrigen = "pjpf cagr mhxj ytbk";

				// Configuración del servidor SMTP
				var smtpClient = new SmtpClient("smtp.gmail.com")
				{
					Port = 587, // Cambia al puerto que tu proveedor use, como 465 o 25
					Credentials = new System.Net.NetworkCredential(correoOrigen ,ContraseñaOrigen),
					EnableSsl = true // Cambia a 'false' si SSL no es necesario
				};

				// Crear el mensaje
				var mensaje = new MailMessage
				{
					From = new MailAddress(correoOrigen),
					Subject = asunto,
					Body = cuerpo,
					IsBodyHtml = true // Cambiar a 'false' si no deseas enviar HTML
				};

				mensaje.To.Add(destinatario);

				// Enviar el correo
				smtpClient.Send(mensaje);
				Console.WriteLine("Correo enviado exitosamente.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al enviar el correo: {ex.Message}");
			}
		}









	}
}
