using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnicaRossmon.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Logout()
        {
            // Elimina el JWT de la sesión
            HttpContext.Session.Remove("JWT");

            // Redirige al login del sisteam
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }
}