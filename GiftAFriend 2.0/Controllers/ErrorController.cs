using Microsoft.AspNetCore.Mvc;

namespace GiftAFriend_2._0.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error404()
        {return View();}
    }
}
