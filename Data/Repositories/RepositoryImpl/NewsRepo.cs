using Data.Models;
using Data.Models.Helpers;
using Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.RepositoryImpl
{
    public class NewsRepo : INewsRepo
    {
        private readonly AppDbContext _context;

        public NewsRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<News> AddNewsAsync(News news)
        {
            var temp = await _context.News!.AddAsync(news);

            await _context.SaveChangesAsync();
            return temp.Entity;
        }

        public async Task<PagedList<News>> GetNewsAsync(int page, int pageSize)
        {
            IQueryable<News> newsQuery = _context.News!;

            // order
            newsQuery = newsQuery.OrderByDescending(x => x.ModifiedDate);

            // create paginated list
            var news = await PagedList<News>.CreateAsync(newsQuery, page, pageSize);
            return news;
        }

        public async Task<News> GetSingleNewsAsync(Guid newsId)
        {
            var temp = await _context.News!.FindAsync(newsId);
            return temp!;
        }
    }
}