using Microsoft.AspNetCore.Mvc;

namespace TMinggu2.Controllers
{
    public class StartBootstrapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
