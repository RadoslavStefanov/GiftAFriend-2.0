using GAF.Core.Statics;
using GiftAFriend_2._0.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GiftAFriend_2._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<IdentityUser> userManager;
        private Guard Guard;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> _userManager, Guard _guard)
        {
            _logger = logger;
            userManager = _userManager;
            Guard = _guard;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            //GuardAgainstUnknown
            //GuardAgainstNullUserInfo
            await Guard.AgainstNullUserInfo(userManager.GetUserName(User));
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}