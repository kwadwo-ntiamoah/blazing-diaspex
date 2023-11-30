using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.RepositoryImpl
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext _context;

        public CategoryRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            var temp = await _context.Categories!.AddAsync(category);

            await _context.SaveChangesAsync();
            return temp.Entity;
        }

        public async Task<Category> EditCategoryAsync(Guid categoryId, Category category)
        {
            var oldCategory = await GetCategoryAsync(categoryId);

            oldCategory.Description = category.Description;
            oldCategory.Title = category.Title;
            
            await _context.SaveChangesAsync();

            return oldCategory;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories!.ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(Guid categoryId)
        {
            var category = await _context.Categories!.FindAsync(categoryId);
            return category!;
        }
    }
}