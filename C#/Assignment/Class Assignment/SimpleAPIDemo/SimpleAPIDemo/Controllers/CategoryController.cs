using Microsoft.AspNetCore.Mvc;

namespace SimpleAPIDemo.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Services.ICategoryService _categoryService;
        public CategoryController(Services.ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Models.Category>> GetCategories()
        {
            return Ok(_categoryService.GetCategories());
        }
        [HttpGet("{id}")]
        public ActionResult<Models.Category> GetCategory(int id)
        {
            var category = _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public ActionResult<Models.Category> CreateCategory(Models.Category category)
        {
            var createdCategory = _categoryService.CreateCategory(category);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }
    }
}
