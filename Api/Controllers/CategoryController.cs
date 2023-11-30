using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;

        public CategoryController(ICategoryService categoryService, IPostService postService)
        {
            _categoryService = categoryService;
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Category>>>> GetCategoriesAsync() {
            return await _categoryService.GetCategoriesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Category>>> GetCategoryAsync(Guid id) {
            return await _categoryService.GetCategoryAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Category>>> AddCategoryAsync(AddCategoryDto dto) {
            return await _categoryService.AddCategoryAsync(dto);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<ApiResponse<Category>>> UpdateCategoryAsync(Guid id, EditCategoryDto dto) {
            return await _categoryService.EditCategoryAsync(id, dto);
        }
    }
}