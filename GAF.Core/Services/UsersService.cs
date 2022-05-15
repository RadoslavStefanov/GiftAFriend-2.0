using GAF.Infrastructure.Data;
using GAF.Infrastructure.Data.Models;

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

        public async Task<decimal> getUserTokens(string userName)
        {
            var user = repo.Users.FirstOrDefault(x => x.UserName == userName);
            if (user != null)
            { return repo.UserInfos.FirstOrDefault(x => x.UserId == user.Id).GiftTokens; }
            return 0;
        }

        public async Task<string>? getUserNotifications(string userName)
        {
            var user = repo.Users.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
            { return null; }

            var result = "";
            if (!await hasCompleteInfo(user))
            { result+= "-accountSettings"; }

            if (await hasFriendRequests(user))
            { result+="-Friends>FriendRequest"; }

            return result;
        }

        private async Task<bool> hasCompleteInfo(Microsoft.AspNetCore.Identity.IdentityUser user)
        {
            var info = repo.UserInfos.FirstOrDefault(ui => ui.UserId == user.Id);
            if (info==null || info.MobileNumber == null  || info.MobileNumber.Length < 5)
            { return false; }

            return true;
        }

        private async Task<bool> hasFriendRequests(Microsoft.AspNetCore.Identity.IdentityUser user)
        {return repo.FriendRequests.Any(x => x.RecieverId == user.Id);}
    }
}

