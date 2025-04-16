using Domus.Pages.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domus.Pages.Administrador
{
    public class Administrar_UsuariosModel : PageModel
    {

		Class_MTO_BD class_MTO_BD = new Class_MTO_BD();


		public DataTable datatable_Usuario { get; set; }

		public DataTable datatable_Chalet { get; set; }


		[BindProperty(SupportsGet = true)]
		public int id_Usuario { get; set; }


		[BindProperty]
		public string input_Usuario { get; set; }


		[BindProperty]
		public string input_Nombre1 { get; set; }


		[BindProperty]
		public string input_Nombre2 { get; set; }


		[BindProperty]
		public string input_Apellido1 { get; set; }


		[BindProperty]
		public string input_Apellido2 { get; set; }


		[BindProperty]
		public string input_FechaNacimiento { get; set; }


		[BindProperty]
		public string input_Correo { get; set; }


		


		[BindProperty]
		public string inpu_Estado_Usuario { get; set; }


		[BindProperty]
		public Boolean input_RolEstudiante { get; set; }


		[BindProperty]
		public Boolean input_RolSuperAdmin { get; set; }


		[BindProperty]
		public Boolean input_RolAdminChalet { get; set; }


		[BindProperty]
		public decimal input_Boscoins { get; set; }

		[BindProperty]
		public string inputidChalet { get; set; }




		public async Task OnGet()
        {

			await Obtener_Usuarios();

		}


		public async Task Obtener_Usuarios()
		{

			var Json = new
			{
				condicion = "ConsultarUsuarios"

			};

			string cadenaJson = JsonSerializer.Serialize(Json); //convertir a formato json

			datatable_Usuario = (await class_MTO_BD.Obtener_Registros("Administrar_Usuarios", cadenaJson)).Tables[0];
			datatable_Chalet = (await class_MTO_BD.Obtener_Registros("Administrar_Usuarios", cadenaJson)).Tables[1];

			if (id_Usuario != 0)
			{


				// Filtrar filas por una condición, por ejemplo, id = 1
				var filasFiltradas = datatable_Usuario.AsEnumerable().Where(fila => fila.Field<int>("id_Usuario") == id_Usuario);

				// Convertir a una lista si necesitas trabajar con los resultados
				List<DataRow> listaFilasFiltradas = filasFiltradas.ToList();

				// Mostrar resultados
				foreach (var fila in listaFilasFiltradas)
				{


					input_Usuario = fila["usuario"].ToString();
					input_Nombre1 = fila["nombre1"].ToString();
					input_Nombre2 = fila["nombre2"].ToString();
					input_Apellido1 = fila["apellido1"].ToString();
					input_Apellido2 = fila["apellido2"].ToString();
					input_FechaNacimiento = fila["fecha_nacimiento"].ToString();
					input_Correo = fila["Correo"].ToString();
					inpu_Estado_Usuario = fila["estado"].ToString();
					input_RolEstudiante = Convert.ToBoolean(fila["Rol_estudiante"].ToString());
					input_RolSuperAdmin = Convert.ToBoolean(fila["Rol_Super_admin"].ToString());
					input_RolAdminChalet = Convert.ToBoolean(fila["Rol_adminChalet"].ToString());
					input_Boscoins = Convert.ToDecimal(fila["boscoins"].ToString());
					inputidChalet = fila["id_chalet"].ToString();
				}


			}

		}




		public async Task<IActionResult> OnpostGuardar_Usuarios(int idUsuario)
		{

			var valorSql_RolEstudiante = input_RolEstudiante ? 1 : 0;
			var valorSql_RolSuperAdmin = input_RolSuperAdmin ? 1 : 0;
			var valorSql_RolAdminChalet = input_RolAdminChalet ? 1 : 0;

			if (idUsuario == 0)//insertaremos en la base de dastos
			{


				var Json = new
				{
					condicion = "Insertar_Usuarios",
					usuario = input_Usuario,
					nombre1 = input_Nombre1,
					nombre2 = input_Nombre2,
					apellido1 = input_Apellido1,
					apellido2 = input_Apellido2,
					fecha_nacimiento = input_FechaNacimiento,
					Correo = input_Correo,
					estado = inpu_Estado_Usuario,
					Rol_estudiante = valorSql_RolEstudiante,
					Rol_Super_admin = valorSql_RolSuperAdmin,
					Rol_adminChalet = valorSql_RolAdminChalet,
					boscoins = input_Boscoins,
					id_Chalet = inputidChalet


				};

				string cadenaJson = JsonSerializer.Serialize(Json); //convertir a formato json
				await class_MTO_BD.Enviar_Registros("Administrar_Usuarios", cadenaJson);

			}
			else
			{ //modificaremos el registro



				var Json = new
				{
					condicion = "Modificar_Usuario",
					id_Usuario = idUsuario,
					usuario = input_Usuario,
					nombre1 = input_Nombre1,
					nombre2 = input_Nombre2,
					apellido1 = input_Apellido1,
					apellido2 = input_Apellido2,
					fecha_nacimiento = input_FechaNacimiento,
					Correo = input_Correo,
					estado = inpu_Estado_Usuario,
					Rol_estudiante = valorSql_RolEstudiante,
					Rol_Super_admin = valorSql_RolSuperAdmin,
					Rol_adminChalet = valorSql_RolAdminChalet,
					boscoins = input_Boscoins,
					id_Chalet = inputidChalet



				};

				string cadenaJson = JsonSerializer.Serialize(Json); //convertir a formato json
				await class_MTO_BD.Enviar_Registros("Administrar_Usuarios", cadenaJson);



			}

			return RedirectToPage(new { id_Usuario = idUsuario });

		}








	}
}
