using Data.Models;

namespace Data.Repositories
{
    public interface ICategoryRepo
    {
        public Task<IEnumerable<Category>> GetCategoriesAsync();
        public Task<Category> GetCategoryAsync(Guid categoryId);
        public Task<Category> AddCategoryAsync(Category category);
        public Task<Category> EditCategoryAsync(Guid categoryId, Category category);
    }
}