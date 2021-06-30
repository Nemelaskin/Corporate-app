using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Corporate_app.Models;
using Corporate_app.Models.Context;
using Corporate_app.Repositories;


namespace Corporate_app.Controllers
{
    public class UsersController : Controller
    {
        private readonly ModelsContext _context;
        private readonly UsersRepository repository;
        public UsersController(ModelsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var modelsContext = repository.GetUsers();
            return View(await modelsContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await repository.GetUser(id);
            if (user == null) {
               
                return RedirectToAction("Index","Users");
            }
            return View(user);
        }

        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionId");
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserId,Name,SurName,Email,Password,PositionId,RoleId,Phone,ConfirmationCode")] User user)
        {
            if (ModelState.IsValid) {
                repository.SaveUser(user);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionId", user.PositionId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", user.RoleId);
            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) {
                ModelState.AddModelError("", "Error! Not found this id!");
                return View();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null) {
                ModelState.AddModelError("", "Error! Not found this id!");
                return View();
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionId", user.PositionId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", user.RoleId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("UserId,Name,SurName,Email,Password,PositionId,RoleId,Phone,ConfirmationCode")] User user)
        {
            if (id != user.UserId) {
                ModelState.AddModelError("", "Error! Not found this id!");
                return View();
            }

            if (ModelState.IsValid) {
                try {
                    repository.SaveUser(user);
                }
                catch (DbUpdateConcurrencyException) {
                    if (!UserExists(user.UserId)) {
                        ModelState.AddModelError("", "Error! Not found this id!");
                        return View();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionId", user.PositionId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", user.RoleId);
            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await repository.GetUser(id);
            if (user == null) {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await repository.GetUser(id);
            repository.DeleteUser(user);
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
