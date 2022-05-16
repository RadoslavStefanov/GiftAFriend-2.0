using GAF.Core.Services;
using GAF.Core.Statics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GiftAFriend_2._0.Controllers
{
    public class UsersController : BaseController
    {
        private readonly UsersService usersService;
        private UserManager<IdentityUser> userManager;
        public UsersController(UserManager<IdentityUser> _userManager, UsersService _usersService, Guard _guard) 
            : base(_userManager, _usersService, _guard)
        {
            usersService = _usersService;
            userManager = _userManager;
        }

        public async Task<IActionResult> MyWallet()
        {return View(await usersService.getUserTransactions(userManager.GetUserName(User)));}
    }
}
