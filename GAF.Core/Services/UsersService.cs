using GAF.Infrastructure.Data;
using GAF.Infrastructure.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GAF.Core.Services
{
    public class UsersService
    {
        private readonly GAFDbContext repo;
        public UsersService(GAFDbContext _repo)
        { 
            repo = _repo;
        }
        public bool hasInfo(string userName)
        {
            var user = repo.Users.FirstOrDefault(x => x.UserName == userName);
            if (repo.UserInfos.FirstOrDefault(ui => ui.UserId == user.Id) == null)
            { return false; }

            else { return true; }
        }

        public async Task generateInfo(string userName)
        {
            var user = repo.Users.FirstOrDefault(x => x.UserName == userName);
            await repo.UserInfos.AddAsync(new UserInfos
            {
                UserId = user.Id,
                GiftTokens = 0
            });
            await repo.SaveChangesAsync();
        }
    }
}
