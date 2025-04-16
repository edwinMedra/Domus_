using System.Data;
using Microsoft.Data.SqlClient;

namespace Domus.Pages.Class
{
	public class Class_MTO_BD
	{

		public Class_MTO_BD() { }

		// Método para obtener la conexión a la base de datos
		private async Task<SqlConnection> GetConexion()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			IConfiguration _configuration = builder.Build();
			var myConnectionString = _configuration["ConnectionStrings:MyDbConection"];

			var SqlConexion = new SqlConnection(myConnectionString);

			await SqlConexion.OpenAsync(); // Abrir la conexión de una vez

			return SqlConexion;
		}


		// Método para actualizar registros
		public async Task Enviar_Registros(string SPName, string JsonQueryString)
		{

			using (var connection = await GetConexion())
			{
				using (var cmd = new SqlCommand(SPName, connection))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@solicitudJson", SqlDbType.NText).Value = JsonQueryString;
					await cmd.ExecuteNonQueryAsync();
				}
			}


		}

		// Método para obtener registros en un DataTable
		public async Task<DataSet> Obtener_Registros(string SPName, string JsonQueryString)
		{
			DataSet dataTableRegistros = new DataSet();


			using (var connection = await GetConexion())
			{
				using (var cmd = new SqlCommand(SPName, connection))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@solicitudJson", SqlDbType.NText).Value = JsonQueryString;

					using (var adapter = new SqlDataAdapter(cmd))
					{
						adapter.Fill(dataTableRegistros); // Llenar el DataTable
					}
				}
			}


			return dataTableRegistros;
		}




	}
}
