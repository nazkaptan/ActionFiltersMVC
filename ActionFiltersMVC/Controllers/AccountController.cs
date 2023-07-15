using ActionFiltersMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionFiltersMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string link)
        {
            ViewBag.Link = link;
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user, string link)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("userName", user.UserName);
                HttpContext.Session.SetString("userRole", user.UserRole.ToString());

                if (string.IsNullOrEmpty(link)) return RedirectToAction("Index", "Home");

                return Redirect(link);
            }

            ViewBag.Link = link;
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("userRole");
            return RedirectToAction("Login");
        }
    }
}
