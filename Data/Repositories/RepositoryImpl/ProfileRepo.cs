using Data.Models;
using Data.Models.Helpers;
using Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.RepositoryImpl
{
    public class ProfileRepo : IProfileRepo
    {
        private readonly AppDbContext _context;

        public ProfileRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Profile> CreateProfileAsync(Profile profile)
        {
            var temp = await _context.AddAsync(profile);

            await _context.SaveChangesAsync();
            return temp.Entity;
        }

        public async Task<PagedList<Profile>> GetNewJoiners(string email, int page, int pageSize)
        {
            IQueryable<Profile> profilesQuery = _context.Profiles!;
            
            // sorting
            profilesQuery = profilesQuery.Where(x => x.User != email);

            // order by recently join
            profilesQuery = profilesQuery.OrderByDescending(x => x.DateJoined);

            var profiles = await PagedList<Profile>.CreateAsync(profilesQuery, page, pageSize);
            return profiles;
        }

        public async Task<Profile> GetProfileByEmailAsync(string email)
        {
            var profile = await _context.Profiles!.Where(p => p.User!.Equals(email)).FirstOrDefaultAsync();
            return profile!;
        }
    }
}