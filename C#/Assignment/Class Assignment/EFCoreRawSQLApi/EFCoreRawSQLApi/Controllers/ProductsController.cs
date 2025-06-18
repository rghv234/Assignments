using Microsoft.AspNetCore.Mvc;

namespace EFCoreRawSQLApi.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
