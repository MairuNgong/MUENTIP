using Microsoft.AspNetCore.Mvc;

namespace MUENTIP.Controllers
{
    public class ViewActivityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
