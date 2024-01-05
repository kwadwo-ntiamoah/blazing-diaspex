using Data.Models;
using Data.Models.Helpers;
using Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.RepositoryImpl
{
    public class PostRepo : IPostRepo
    {
        private readonly AppDbContext _context;

        public PostRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Post> AddPostAsync(Post post)
        {
            var temp = await _context.Posts!.AddAsync(post);

            await _context.SaveChangesAsync();
            return temp.Entity;
        }

        public async Task DeletePost(Guid postId)
        {
            await _context.Posts!.Where(x => x.Id.Equals(postId)).ExecuteDeleteAsync();
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _context.Posts!.ToListAsync();
        }

        public async Task<PagedList<Post>> GetCategoryPosts(Guid categoryId, int page, int pageSize)
        {
            IQueryable<Post> postQuery = _context.Posts!;

            // filter
            postQuery = postQuery.Where(p => p.CategoryId.Equals(categoryId));

            // order by latest
            postQuery = postQuery.OrderByDescending(p => p.ModifiedDate);

            // return pagedlist
            var posts = await PagedList<Post>.CreateAsync(postQuery, page, pageSize);
            return posts;
        }

        public async Task<Post> GetPostAsync(Guid postId)
        {
            var post = await _context.Posts!.FindAsync(postId);
            return post!;
        }

        public async Task<Post> UpdatePostAsync(Guid postId, Post newPost)
        {
            var temp = await _context.Posts!.Where(p => p.Id.Equals(postId))
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Title, newPost.Title)
                    .SetProperty(x => x.Content, newPost.Content)
                );
            
            return newPost;
        }
    }
}