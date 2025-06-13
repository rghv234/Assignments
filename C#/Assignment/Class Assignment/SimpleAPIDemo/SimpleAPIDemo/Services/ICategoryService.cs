namespace SimpleAPIDemo.Services
{
    public interface ICategoryService
    {
        IEnumerable<Models.Category> GetCategories();
        Models.Category? GetCategory(int id);
        Models.Category CreateCategory(Models.Category category);
        void UpdateCategory(int id, Models.Category updatedCategory);
        void DeleteCategory(int id);
        List<Models.Category> GetCategoriesByName(string name);
        IEnumerable<Models.Product>? GetProductsByCategoryId(int categoryId);
    }
}
