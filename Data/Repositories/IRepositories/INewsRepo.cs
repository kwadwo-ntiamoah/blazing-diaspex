using Data.Models;
using Data.Models.Helpers;

namespace Data.Repositories.IRepositories
{
    public interface INewsRepo
    {
        public Task<News> AddNewsAsync(News news);
        public Task<PagedList<News>> GetNewsAsync(int page, int pageSize);
        public Task<News> GetSingleNewsAsync(Guid newsId);
    }
}