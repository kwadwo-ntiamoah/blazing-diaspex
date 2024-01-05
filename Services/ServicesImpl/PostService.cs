using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Data.Constants;
using Data.DTOs;
using Data.Models;
using Data.Models.Helpers;
using Data.Repositories;
using Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.IServices;

namespace Services.ServicesImpl
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _postRepo;
        private readonly IProfileRepo _profileRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ILogger<PostService> _logger;

        public PostService(IPostRepo postRepo, IProfileRepo profileRepo, ICategoryRepo categoryRepo, ILogger<PostService> logger)
        {
            _postRepo = postRepo;
            _categoryRepo = categoryRepo;
            _profileRepo = profileRepo;
            _logger = logger;
        }

        public async Task<ApiResponse<Post>> AddPostAsync(string owner, AddPostDto model)
        {
            var response = new ApiResponse<Post>();

            try
            {
                Post post = new()
                {
                    CategoryId = model.CategoryId,
                    Content = model.Content,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Owner = owner,
                    Title = model.Title,
                    Type = model.Type
                };

                var addedPost = await _postRepo.AddPostAsync(post);

                if (addedPost != null)
                {
                    response.Message = "Post added successfully";
                    response.Status = System.Net.HttpStatusCode.Created;
                    response.Data = addedPost;

                    return response;
                }

                response.Message = "Post could not be added. Try again later";
                response.Status = System.Net.HttpStatusCode.BadRequest;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at PostService::AddPostAsync => message::{message}", ex.Message);

                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }

        public async Task<ApiResponse<int>> DeletePost(string owner, Guid postId)
        {
            var response = new ApiResponse<int>();
            try
            {
                var post = await _postRepo.GetPostAsync(postId);

                if (!post.Owner!.Equals(owner)) {
                    response.Status = HttpStatusCode.Unauthorized;
                    response.Message = "You're not authorized to delete this post";

                    return response;
                }

                await _postRepo.DeletePost(postId);

                response.Status = HttpStatusCode.OK;
                response.Message = "Post deleted"; 

                return response;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred at PostService::DeletePost => message::{message}", ex.Message);

                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }

        public async Task<ApiResponse<List<Post>>> GetAllPosts()
        {
            var response = new ApiResponse<List<Post>>();

            try
            {
                var posts = await _postRepo.GetAllPosts();

                response.Message = "Posts retrieved successfully";
                response.Status = System.Net.HttpStatusCode.OK;
                response.Data = posts;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at PostService::GetCategoryPosts => message::{message}", ex.Message);

                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }

        public async Task<ApiResponse<PagedList<Post>>> GetCategoryPosts(Guid categoryId, int page, int pageSize)
        {
            var response = new ApiResponse<PagedList<Post>>();

            try
            {
                var posts = await _postRepo.GetCategoryPosts(categoryId, page, pageSize);

                response.Message = "Posts retrieved successfully";
                response.Status = System.Net.HttpStatusCode.OK;
                response.Data = posts;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at PostService::GetCategoryPosts => message::{message}", ex.Message);

                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }

        public async Task<ApiResponse<Post>> GetPost(Guid postId)
        {
            var response = new ApiResponse<Post>();

            try
            {
                var post = await _postRepo.GetPostAsync(postId);

                if (post != null) {
                    response.Message = "Post retrieved successfully";
                    response.Status = System.Net.HttpStatusCode.OK;
                    response.Data = post;
                    
                    return response;
                }

                response.Status = System.Net.HttpStatusCode.NotFound;
                response.Message = "Post not found";
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at PostService::GetPost => message::{message}", ex.Message);

                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }

        public async Task<ApiResponse<Post>> UpdatePostAsync(string owner, Guid postId, EditPostDto newPost)
        {
            var response = new ApiResponse<Post>();

            try
            {
                var post = await _postRepo.GetPostAsync(postId); 

                if (post == null) {
                    response.Message = "Post to update not found";
                    response.Status = System.Net.HttpStatusCode.NotFound;

                    return response;
                }

                if (post.Owner != owner) {
                    response.Status = System.Net.HttpStatusCode.Unauthorized;
                    response.Message = "You're not allowed to update this post";

                    return response;
                }

                var payload = new Post() {
                    Content = newPost.Content,
                    Title = newPost.Title,
                };

                var updatedPost = await _postRepo.UpdatePostAsync(postId, payload);

                if (updatedPost != null) {
                    response.Message = "Post updated successfully";
                    response.Data = updatedPost;
                    response.Status = System.Net.HttpStatusCode.OK;

                    return response;
                }

                response.Message = "An error occurred updating post";
                response.Status = HttpStatusCode.BadRequest;

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at PostService::UpdatePostAsync => message::{message}", ex.Message);

                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }
    }
}