using Data.Models;
using Data.Models.Helpers;

namespace Data.Repositories.IRepositories
{
    public interface IPostRepo
    {
        public Task<List<Post>> GetAllPosts();
        public Task<Post> AddPostAsync(Post post);
        public Task<Post> UpdatePostAsync(Guid postId, Post newPost);
        public Task<PagedList<Post>> GetCategoryPosts(Guid categoryId, int page, int pageSize);
        public Task<Post> GetPostAsync(Guid postId);
        public Task DeletePost(Guid postId);
    }
}