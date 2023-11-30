using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Models;
using Data.Models.Helpers;

namespace Services.IServices
{
    public interface INewsService
    {
        public Task<ApiResponse<News>> AddNewsAsync(AddNewsDto news);
        public Task<ApiResponse<PagedList<News>>> GetNewsAsync(int page, int pageSize);
        public Task<ApiResponse<News>> GetSingleNewsAsync(Guid newsId);
    }
}