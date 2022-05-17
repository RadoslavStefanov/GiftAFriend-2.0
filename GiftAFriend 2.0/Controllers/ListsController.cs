using GAF.Core.Services;
using GAF.Core.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GiftAFriend_2._0.Controllers
{
    public class ListsController : BaseController
    {
        private readonly TransactionsService transactionsService;
        private readonly UsersService usersService;

        public ListsController(UserManager<IdentityUser> _userManager, UsersService _usersService, Guard _guard, TransactionsService _transactionsService)
            : base(_userManager, _usersService, _guard) 
        { 
            transactionsService = _transactionsService;
            usersService = _usersService;
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllTransactionsList()
        {return View(await transactionsService.getAllTransactions());}

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllUsersList()
        { return View(await usersService.getAllUsers()); }
    }
}
