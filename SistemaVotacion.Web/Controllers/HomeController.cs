using Microsoft.AspNetCore.Mvc;

namespace SistemaVotacion.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
