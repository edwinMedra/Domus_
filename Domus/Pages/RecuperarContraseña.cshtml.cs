using Domus.Pages.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace Domus.Pages
{
    public class RecuperarContraseñaModel : PageModel
    {
		Class_MTO_BD class_MTO_BD = new Class_MTO_BD();
		ProcesosComunes procesos_Comunes = new ProcesosComunes();


		//Campos del formulario deben tener el mismo nombre en el nodo asp-for
		[BindProperty]
		public string inputEmail { get; set; }

		[BindProperty]
		public string IngresarCodigo { get; set; }

		[BindProperty]
		public string Comfimar_Contrasena { get; set; }

		[BindProperty]
		public string Contrasena_Nueva { get; set; }




		public void OnGet()
        {
        }


		public async Task<IActionResult> OnpostGenerarClave()
		{



			var Json = new
			{
				condicion = "Generar_Clave",
				Correo = inputEmail


			};

			string cadenaJson = JsonSerializer.Serialize(Json); //convertir a formato json
			string Clave_Generada = (await class_MTO_BD.Obtener_Registros("Administrar_Usuarios", cadenaJson)).Tables[0].Rows[0][0].ToString();

			if (Clave_Generada == "correo electronico no existe")
			{
				// En el método POST
				TempData["Alerta"] = "Asegurate que tu Correo este bien Escrito.";
				return RedirectToPage();


			}
			else {

				string cuerpoCorreo = string.Format(@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Recuperación de Cuenta</title>
</head>
<body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
    <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px;'>
        <h2 style='color: #007BFF;'>Recuperación de tu cuenta</h2>
        <p>Hola <strong>{0}</strong>,</p>
        <p>
            Recibimos tu solicitud para recuperar el acceso a tu cuenta. A continuación, encontrarás la clave que podrás utilizar para completar el proceso de restablecimiento:
        </p>
        <p style='font-size: 1.2em; color: #007BFF; font-weight: bold; text-align: center;'>
            Clave de recuperación: <strong>{1}</strong>
        </p>
        <p>
            Por favor, utiliza esta clave en el formulario de recuperación de tu cuenta. Recuerda que esta clave es personal y no debe ser compartida con nadie para mantener la seguridad de tu información.
        </p>
        <p>
            Si tienes alguna pregunta o necesitas asistencia adicional, no dudes en ponerte en contacto con nuestro equipo de soporte.
        </p>
        <p>¡Gracias por confiar en nosotros!</p>
        <br>
        <p>Atentamente,</p>
        <p><strong>{2}</strong></p>
    </div>
</body>
</html>
", "", Clave_Generada, "Domus");



				procesos_Comunes.EnviarCorreo(inputEmail, "Tu clave Generada Domus", cuerpoCorreo);


			}


			return RedirectToPage();


		}



		public async Task<IActionResult> OnpostCambiarContrasena()
		{

			var Json = new
			{
				condicion = "Restaurar_Clave",
				Correo = inputEmail,
				Contrasena1 = Contrasena_Nueva,
				Contrasena2 = Comfimar_Contrasena,
				clave = IngresarCodigo


			};

			string cadenaJson = JsonSerializer.Serialize(Json); //convertir a formato json
			string Mensaje = (await class_MTO_BD.Obtener_Registros("Administrar_Usuarios", cadenaJson)).Tables[0].Rows[0][0].ToString();

			if (Mensaje == "exito")
			{

				return RedirectToPage("Login");

			}
			else {


			// En el método POST
				TempData["Alerta"] = Mensaje;
				return RedirectToPage();

			}
			

		}




		}
}
