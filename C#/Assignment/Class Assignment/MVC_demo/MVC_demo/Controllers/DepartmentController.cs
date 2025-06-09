using Microsoft.AspNetCore.Mvc;
using MVC_demo.Models;

namespace MVC_demo.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            Department department = new Department
            {
                id = 1,
                name = "IT",
                domain = ".NET Full Stack"
            };

            List<Department> departments = new List<Department>
            {
                department,
                new Department { id = 2, name = "HR", domain = "Recruitment" },
                new Department { id = 3, name = "Finance", domain = "Accounting" }
            };
            ViewData["Name"] = "Raghav";
            ViewData["Domain"] = ".netFullStack";
            ViewBag.CompanyName = "Hexaware";

            return View(departments);
        }


        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                // Here you would typically save the department to a database
                // For this example, we will just redirect to the Index action
                return RedirectToAction("Index");
            }
            return View(department);
        }
    }
}
