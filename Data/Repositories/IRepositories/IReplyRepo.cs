using Data.Models;

namespace Data.Repositories.IRepositories
{
    public interface IReplyRepo
    {
        public Task<Reply> PostReplyAsync(Reply reply);
        public Task<IEnumerable<Reply>> GetPostRepliesAsync(Guid postId);
    }
}