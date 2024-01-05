using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<List<Post>>>> GetPosts()
        {
            return await _postService.GetAllPosts();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Post>>> GetPost(Guid id) {
            return await _postService.GetPost(id);
        }

        [HttpGet("category/{category_id}")]
        public async Task<ActionResult<ApiResponse<PagedList<Post>>>> GetPostsAsync(Guid category_id, int page = 1, int pageSize = 10) {
            return await _postService.GetCategoryPosts(category_id, page, pageSize);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Post>>> AddPostAsync(AddPostDto model) {
            var owner = User.Claims.First(x => x.Type.Equals(ClaimTypes.Email)).Value;
            return await _postService.AddPostAsync(owner, model);
        }

        [HttpPost("{id}/update")]
        public async Task<ActionResult<ApiResponse<Post>>> UpdatePostAsync(Guid id, EditPostDto model) {
            var owner = User.Claims.First(x => x.Type.Equals(ClaimTypes.Email)).Value;
            return await _postService.UpdatePostAsync(owner, id, model);
        }

        [HttpPost("{id}/delete")]
        public async Task<ActionResult<ApiResponse<int>>> DeletePostAsync(Guid id) {
            var owner = User.Claims.First(x => x.Type.Equals(ClaimTypes.Email)).Value;
            return await _postService.DeletePost(owner, id);
        }
    }
}