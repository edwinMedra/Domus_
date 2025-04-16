using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Domus.Pages
{
    public class CerrarSesionModel : PageModel
    {
        public IActionResult  OnGet()
        {


			HttpContext.Session.Clear(); // Mata todas las sesiones activas

			return RedirectToPage("Login");
		}
     }
}
