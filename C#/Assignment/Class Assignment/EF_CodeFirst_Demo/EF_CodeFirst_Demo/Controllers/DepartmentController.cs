using EF_CodeFirst_Demo.DBContext;
using Microsoft.AspNetCore.Mvc;

namespace EF_CodeFirst_Demo.Controllers
{
    public class DepartmentController : Controller
    {
        public readonly ApplicationDBContext _context;
        public DepartmentController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Models.Department department)
        {
            if (ModelState.IsValid)
            {
                // Here you would typically save the department to the database
                // using your DbContext, e.g.:
                _context.Departments.Add(department);
                _context.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(department);
        }
    }
}
