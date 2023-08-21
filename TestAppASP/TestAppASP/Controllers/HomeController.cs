using Microsoft.AspNetCore.Mvc;


namespace TestAppASP.Controllers
{
    public class HomeController : Controller
    {
       // Вызов html Index при запуске сайта
        public IActionResult Index()
        {
            return View();
        }
    }
}