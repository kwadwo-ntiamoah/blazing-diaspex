using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Models;

namespace Services.IServices
{
    public interface ICategoryService
    {
        public Task<ApiResponse<IEnumerable<Category>>> GetCategoriesAsync();
        public Task<ApiResponse<Category>> GetCategoryAsync(Guid categoryId);
        public Task<ApiResponse<Category>> AddCategoryAsync(AddCategoryDto category);
        public Task<ApiResponse<Category>> EditCategoryAsync(Guid categoryId, EditCategoryDto category);
    }
}