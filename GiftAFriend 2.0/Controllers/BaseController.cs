using GAF.Core.Services;
using GAF.Core.Statics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GiftAFriend_2._0.Controllers
{
    public class BaseController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private UsersService usersService;
        private Guard Guard;
        public BaseController(UserManager<IdentityUser> _userManager, UsersService _usersService, Guard _guard)
        {
            userManager = _userManager;
            usersService = _usersService;
            Guard = _guard;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userName = userManager.GetUserName(User);

            if (userName != null && await Guard.AgainstNullUserInfo(userName))
            {
                ViewBag.ShowBonusMessage = "Hello and wellcome to GiftAFriend, to start you off we will award you with 100 GIFT Tokens! Enjoy your stay.";
                await usersService.giveNewUserBonus(userName);
            }

            ViewBag.NotificationNodes = await usersService.getUserNotifications(userName);
            ViewBag.userTokens = await usersService.getUserTokens(userName);
            await next();
        }
    }
}
