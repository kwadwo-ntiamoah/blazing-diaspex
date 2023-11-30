using Data.Models;
using Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.RepositoryImpl
{
    public class ReplyRepo : IReplyRepo
    {
        private readonly AppDbContext _context;

        public ReplyRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reply>> GetPostRepliesAsync(Guid postId)
        {
            return await _context.Replies!.Where(r => r.PostId.Equals(postId)).ToListAsync();
        }

        public async Task<Reply> PostReplyAsync(Reply reply)
        {
            var temp = await _context.Replies!.AddAsync(reply);
            await _context.SaveChangesAsync();

            return temp.Entity;
        }
    }
}