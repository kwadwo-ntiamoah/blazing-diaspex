using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Constants;
using Data.DTOs;
using Data.Models;
using Data.Models.Helpers;
using Data.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using Services.IServices;

namespace Services.ServicesImpl
{
    public class NewsService : INewsService
    {
        private readonly INewsRepo _newsRepo;
        private readonly ILogger<NewsService> _logger;

        public NewsService(INewsRepo newsRepo, ILogger<NewsService> logger)
        {
            _newsRepo = newsRepo;
            _logger = logger;
        }

        public async Task<ApiResponse<News>> AddNewsAsync(AddNewsDto news)
        {
            var response = new ApiResponse<News>();

            try
            {
                var payload = new News() {
                    Title = news.Title,
                    Content = news.Content,
                    Image = news.Image,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                var addedNews = await _newsRepo.AddNewsAsync(payload);

                if (addedNews != null) {
                    response.Status = System.Net.HttpStatusCode.OK;
                    response.Message = "News added successfully";
                    response.Data = addedNews;

                    return response;
                }

                response.Status = System.Net.HttpStatusCode.BadRequest;
                response.Message = "An error occurred adding News. Try again";

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at NewsService::AddNewsAsync => message::{message}", ex.Message);

                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }

        public async Task<ApiResponse<PagedList<News>>> GetNewsAsync(int page, int pageSize)
        {
            var response = new ApiResponse<PagedList<News>>();

            try
            {
                var allNews = await _newsRepo.GetNewsAsync(page, pageSize);

                response.Status = System.Net.HttpStatusCode.OK;
                response.Message = "News retreived successfully";
                response.Data = allNews;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at NewsService::GetNewsAsync => message::{message}", ex.Message);

                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }

        public async Task<ApiResponse<News>> GetSingleNewsAsync(Guid newsId)
        {
            var response = new ApiResponse<News>();

            try
            {
                var news = await _newsRepo.GetSingleNewsAsync(newsId);

                if (news != null) {
                    response.Status = System.Net.HttpStatusCode.OK;
                    response.Message = "News retreived successfully";
                    response.Data = news;

                    return response;
                }

                response.Message = "News with Id not found";
                response.Status = System.Net.HttpStatusCode.NotFound;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at NewsService::GetSingleNews => message::{message}", ex.Message);

                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }
    }
};