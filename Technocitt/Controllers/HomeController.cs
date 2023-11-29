using Microsoft.AspNetCore.Mvc;

namespace Technocitt.Controllers
{ 
    public class HomeController : Controller
    { 

        [HttpGet]
        public IActionResult Index()
        { 
            return RedirectToAction("Index", "Dashboard");
        }
  
    }
}
