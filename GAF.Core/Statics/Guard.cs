using GAF.Core.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
