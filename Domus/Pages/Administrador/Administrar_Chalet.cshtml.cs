using Domus.Pages.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domus.Pages.Administrador
{
    public class Administrar_ChaletModel : PageModel
    {
        Class_MTO_BD class_MTO_BD = new Class_MTO_BD();

		[BindProperty (SupportsGet = true)]
		public int id_Chalet { get; set; }

		public DataTable datatable_chalet { get; set; }


		[BindProperty]
		public string input_Nombre_Chalet { get; set; }


		[BindProperty]
		public string inpu_Estado_Chalet { get; set; }


		public async Task OnGet()
        {

          await   Obtener_Chalets();


           

        }

        public async Task Obtener_Chalets() {

            var Json = new
            {
                condicion = "ConsultarChalets"

			};

            string cadenaJson = JsonSerializer.Serialize(Json); //convertir a formato json

            datatable_chalet = (await class_MTO_BD.Obtener_Registros("Administrar_Chalet", cadenaJson)).Tables[0];
			
			if (id_Chalet != 0)
            {



				// Filtrar filas por una condición, por ejemplo, id = 1
				var filasFiltradas = datatable_chalet.AsEnumerable().Where(fila => fila.Field<int>("id_Chalet") == id_Chalet);

				// Convertir a una lista si necesitas trabajar con los resultados
				List<DataRow> listaFilasFiltradas = filasFiltradas.ToList();

				// Mostrar resultados
				foreach (var fila in listaFilasFiltradas)
				{
					input_Nombre_Chalet = fila["nombre_chalet"].ToString();
					inpu_Estado_Chalet = fila["Estado"].ToString();
				}


			}

		}

        public async Task <IActionResult> OnpostGuardar_Chalet(int id_Chalet) 
        {

            if (id_Chalet == 0)//insertaremos en la base de dastos
            {

				var Json = new
				{
					condicion = "Insertar_Chalet",
                      nombrechalet = input_Nombre_Chalet, 
                      estado = inpu_Estado_Chalet


				};

				string cadenaJson = JsonSerializer.Serialize(Json); //convertir a formato json
                await class_MTO_BD.Enviar_Registros("Administrar_Chalet",cadenaJson);

			}
            else { //modificaremos el registro


				var Json = new
				{
					condicion = "Modificar_Chalet",
                    id_chalet = id_Chalet,
					nombrechalet = input_Nombre_Chalet,
					estado = inpu_Estado_Chalet
					

				};

				string cadenaJson = JsonSerializer.Serialize(Json); //convertir a formato json
				await class_MTO_BD.Enviar_Registros("Administrar_Chalet", cadenaJson);

			}

			return RedirectToPage(new { id_Chalet = id_Chalet });
        
        }

    }
}
