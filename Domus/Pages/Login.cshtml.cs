using Domus.Pages.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Text.Json;

namespace Domus.Pages
{
    public class LoginModel : PageModel
    {
		Class_MTO_BD class_MTO_BD = new Class_MTO_BD();

	

		


		//Campos del formulario deben tener el mismo nombre en el nodo asp-for
		[BindProperty]
		public string imputEmail { get; set; }

		[BindProperty]
		public string imputContrasena { get; set; }






		public void OnGet()
        {

        }



		public async Task<IActionResult> OnpostObtenerUsuarios()
		{

			

				var Json = new
				{
					condicion = "Iniciar_Sesion",
					Correo = imputEmail,
					Contrasena = imputContrasena


				};

				string cadenaJson = JsonSerializer.Serialize(Json); //convertir a formato json
			    string Existe_Usuario = (await class_MTO_BD.Obtener_Registros("Administrar_Usuarios", cadenaJson)).Tables[0].Rows[0][0].ToString();
			   

			if (Existe_Usuario == "0") 
			{
				// En el método POST
				TempData["Alerta"] = "Correo o contraseña Incorrecto.";
				return RedirectToPage();
			}
			else
			{
				string Usuario = (await class_MTO_BD.Obtener_Registros("Administrar_Usuarios", cadenaJson)).Tables[1].Rows[0][1].ToString();
				string Rol_estudiante = (await class_MTO_BD.Obtener_Registros("Administrar_Usuarios", cadenaJson)).Tables[1].Rows[0][9].ToString();
				string Rol_SuperAdmid = (await class_MTO_BD.Obtener_Registros("Administrar_Usuarios", cadenaJson)).Tables[1].Rows[0][10].ToString();
				string Rol_Chalet = (await class_MTO_BD.Obtener_Registros("Administrar_Usuarios", cadenaJson)).Tables[1].Rows[0][11].ToString();
				// Establecer una variable de sesión
				HttpContext.Session.SetString("usuario",Usuario);
				HttpContext.Session.SetString("ROL_Estudiante", Rol_estudiante);
				HttpContext.Session.SetString("ROL_SuperAdmind", Rol_SuperAdmid);
				HttpContext.Session.SetString("ROL_Chalet", Rol_Chalet);
				return RedirectToPage("Index");
			}

			

		}



	}
}
