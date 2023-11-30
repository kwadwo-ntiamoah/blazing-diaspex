using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Data.Constants;
using Data.DTOs;
using Data.Models;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using Services.IServices;

namespace Services.ServicesImpl
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly ICategoryRepo _categoryRepo;

        public CategoryService(ILogger<CategoryService> logger, ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
            _logger = logger;
        }

        public async Task<ApiResponse<Category>> AddCategoryAsync(AddCategoryDto dto)
        {
            var response = new ApiResponse<Category>();

            try
            {
                Category category = new() {
                    Description = dto.Description,
                    Title = dto.Title,
                    Type = dto.CategoryType
                };

                var tempCategory = await _categoryRepo.AddCategoryAsync(category);

                if (tempCategory != null) {
                    response.Status = HttpStatusCode.OK;
                    response.Data = tempCategory;
                    response.Message = "Category added successfully";

                    return response;
                }

                response.Status = HttpStatusCode.BadRequest;
                response.Message = "An error occurred adding Category";

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at CategoryService::AddCategoryAsync => message::{message}", ex.Message);

                response.Status = HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }

        public async Task<ApiResponse<Category>> EditCategoryAsync(Guid categoryId, EditCategoryDto dto)
        {
            var response = new ApiResponse<Category>();

            try
            {
                var category = await _categoryRepo.GetCategoryAsync(categoryId);

                if (category == null) {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "Category not found";

                    return response;
                }

                var newCategory = new Category() {
                    Description = dto.Description,
                    Title = dto.Title,
                };

                var updatedCategory = await _categoryRepo.EditCategoryAsync(categoryId, newCategory);

                if (updatedCategory == null) {
                    response.Message = "An error occurred updating Category";
                    response.Status = HttpStatusCode.BadRequest;

                    return response;
                }

                response.Message = "Category updated successfully";
                response.Status = HttpStatusCode.OK;
                response.Data = updatedCategory;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at CategoryService::EditCategoryAsync => message::{message}", ex.Message);

                response.Status = HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }

        public async Task<ApiResponse<IEnumerable<Category>>> GetCategoriesAsync()
        {
            var response = new ApiResponse<IEnumerable<Category>>();

            try
            {
                var categories = await _categoryRepo.GetCategoriesAsync();
                
                response.Message = "Categories retrieved successfully";
                response.Status = HttpStatusCode.OK;
                response.Data = categories;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at CategoryService::GetCategoriesAsync => message::{message}", ex.Message);

                response.Status = HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }

        public async Task<ApiResponse<Category>> GetCategoryAsync(Guid categoryId)
        {
            var response = new ApiResponse<Category>();

            try
            {
                var category = await _categoryRepo.GetCategoryAsync(categoryId);
                
                response.Message = "Category retrieved successfully";
                response.Status = HttpStatusCode.OK;
                response.Data = category;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at CategoryService::GetCategoriesAsync => message::{message}", ex.Message);

                response.Status = HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }
    }
}