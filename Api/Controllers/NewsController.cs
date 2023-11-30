using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Models;
using Data.Models.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<News>>> AddNewsAsync(AddNewsDto model) {
            return await _newsService.AddNewsAsync(model);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedList<News>>>> GetAllNewsAsync(int page = 1, int pageSize = 10) {
            return await _newsService.GetNewsAsync(page, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<News>>> GetSingleNewsAsync(Guid id) {
            return await _newsService.GetSingleNewsAsync(id);
        }
    }
}