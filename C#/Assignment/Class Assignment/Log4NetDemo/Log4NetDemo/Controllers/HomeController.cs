using System.Diagnostics;
using Log4NetDemo.Models;
using Microsoft.AspNetCore.Mvc;
using log4net.Core;

namespace Log4NetDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Home Index visited");
            _logger.LogError("Demo error log for testing log4net");

            return View();
        }
        public JsonResult GetJsonResult()
        {
            _logger.LogInformation("GetJsonResult called");
            return Json(new { message = "Hello from GetJsonResult!" });
        }   
        public ContentResult GetContentResult()
        {
            _logger.LogInformation("GetContentResult called");
            return Content("Hello from GetContentResult!", "text/plain");
        }

        public IActionResult RedirectToGoogle()
        {
            _logger.LogInformation("Redirecting to Google");
            return Redirect("https://www.google.com");
        }
        public IActionResult RedirectToPrivacy()
        {
            _logger.LogInformation("Redirecting to Privacy page");
            return RedirectToAction("Privacy");
        }
        public IActionResult DownloadTheFile()
        {
            _logger.LogInformation("DownloadTheFile called");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "sample.txt");
            var fileName = "sample.txt";
            return PhysicalFile(filePath, "text/plain", fileName);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
