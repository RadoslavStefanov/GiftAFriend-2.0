using GAF.Core.Services;
namespace GAF.Core.Statics
{
    public class Guard
    {
        private readonly UsersService usersService;

        public Guard(UsersService _usersService)
        {
            usersService = _usersService;
        }

        public async Task<bool> AgainstNullUserInfo(string userName)
        {
            if (usersService.hasInfo(userName) == false)
            { await usersService.generateInfo(userName); return true; }
            return false;
        }

    }
}
