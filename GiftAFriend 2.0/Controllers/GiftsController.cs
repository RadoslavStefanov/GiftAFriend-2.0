using GAF.Core.Models;
using GAF.Core.Services;
using GAF.Core.Statics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GiftAFriend_2._0.Controllers
{
    public class GiftsController : BaseController
    {
        private UsersService usersService;
        private UserManager<IdentityUser> userManager;
        public GiftsController(UserManager<IdentityUser> _userManager, UsersService _usersService, Guard _guard) : base(_userManager, _usersService, _guard)
        {
            userManager = _userManager;
            usersService = _usersService;
        }

        public IActionResult Send(string toastrCommands)
        {
            ViewBag.ToastrCommands = toastrCommands;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(GiftSendPostModel model)
        {
            //return View(await usersService.sendGift(userManager.GetUserName(User),model));
            return Redirect($"/Gifts/Send?toastrCommands={await usersService.sendGift(userManager.GetUserName(User), model)}");
        }
    }
}
