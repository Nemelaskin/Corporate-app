using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Corporate_app.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult MainAdminMenu()
        {
            return View();
        }
        public IActionResult ToCRUDUsers()
        {
            return RedirectToAction("Index", "Users");
        }
    }
}
