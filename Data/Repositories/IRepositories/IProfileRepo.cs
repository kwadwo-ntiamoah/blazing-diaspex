using Data.Models;
using Data.Models.Helpers;

namespace Data.Repositories.IRepositories
{
    public interface IProfileRepo
    {
        public Task<Profile> CreateProfileAsync(Profile profile);
        public Task<Profile> GetProfileByEmailAsync(string email);
        public Task<PagedList<Profile>> GetNewJoiners(string email, int page, int pageSize);
    }
}