namespace SimpleAPIDemo.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly List<Models.Category> _categories = new List<Models.Category>
        {
            new Models.Category { Id = 1, Name = "Electronics" },
            new Models.Category { Id = 2, Name = "Books" },
            new Models.Category { Id = 3, Name = "Clothing" }
        };
        public IEnumerable<Models.Category> GetCategories()
        {
            return _categories;
        }
        public Models.Category? GetCategory(int id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }
        public Models.Category CreateCategory(Models.Category category)
        {
            category.Id = _categories.Max(c => c.Id) + 1;
            _categories.Add(category);
            return category;
        }
        public void UpdateCategory(int id, Models.Category updatedCategory)
        {
            var category = GetCategory(id);
            if (category != null)
            {
                category.Name = updatedCategory.Name;
                // Update other properties as needed
            }
        }
        public void DeleteCategory(int id)
        {
            var category = GetCategory(id);
            if (category != null)
            {
                _categories.Remove(category);
            }
        }
        public List<Models.Category> GetCategoriesByName(string name)
        {
            return _categories.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public IEnumerable<Models.Product>? GetProductsByCategoryId(int categoryId)
        {
            return _categories.FirstOrDefault(c => c.Id == categoryId)?.Products;
        }
    }
}
