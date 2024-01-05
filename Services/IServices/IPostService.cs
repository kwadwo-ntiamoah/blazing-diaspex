using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Models;
using Data.Models.Helpers;

namespace Services.IServices
{
    public interface IPostService
    {
        public Task<ApiResponse<List<Post>>> GetAllPosts();
        public Task<ApiResponse<Post>> AddPostAsync(string owner, AddPostDto post);
        public Task<ApiResponse<Post>> UpdatePostAsync(string owner, Guid postId, EditPostDto newPost);
        public Task<ApiResponse<PagedList<Post>>> GetCategoryPosts(Guid categoryId, int page, int pageSize);
        public Task<ApiResponse<Post>> GetPost(Guid postId);
        public Task<ApiResponse<int>> DeletePost(string owner, Guid postId);
    }
}