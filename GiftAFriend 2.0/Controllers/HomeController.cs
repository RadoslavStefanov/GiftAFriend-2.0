using GAF.Core.Services;
using GAF.Core.Statics;
using GiftAFriend_2._0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GiftAFriend_2._0.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<IdentityUser> userManager;
        private UsersService usersService;


        public HomeController(UserManager<IdentityUser> _userManager, UsersService _usersService,Guard _guard, ILogger<HomeController> logger) : base(_userManager, _usersService,_guard)
        {
            _logger = logger;
            userManager = _userManager;
            usersService = _usersService;
        }

        public IActionResult Index()
        { return View();}


        [Authorize(Roles = "User")]
        public async Task<IActionResult> Dashboard(string? messages)
        {
            ViewBag.Messages = messages;
            return View();
        }


        [Authorize(Roles = "User")]
        public IActionResult Privacy()
        { return View();}



        [Authorize(Roles = "User")]
        public IActionResult Error()
        {return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });}
    }
}