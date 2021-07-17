using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Corporate_app.Repositories;
using Corporate_app.Models;
using Corporate_app.Models.Context;

namespace Corporate_app.Controllers
{
    public class RolesController : Controller
    {
        private readonly ModelsContext context;
        private readonly RoleRepository repository;

        public RolesController(ModelsContext context, RoleRepository repository)
        {
            this.context = context;
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var modelsContext = repository.GetRoles();
            return View(await modelsContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var role = await repository.GetRole(id);
            if (role == null) {

                return RedirectToAction("Index", "Roles");
            }
            return View(role);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("RoleId,RoleName")] Role role)
        {
            if (ModelState.IsValid) {
                repository.SaveRole(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var role = await repository.GetRole(id);
            if (role == null) {
                ModelState.AddModelError("", "Error! Not found this id!");
                return View();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("RoleId,RoleName")] Role role)
        {
            if (id != role.RoleId) {
                ModelState.AddModelError("", "Error! Not found this id!");
                return View();
            }

            if (ModelState.IsValid) {
                try {
                    repository.SaveRole(role);
                }
                catch (DbUpdateConcurrencyException) {
                    if (!RoleExists(role.RoleId)) {
                        ModelState.AddModelError("", "Error! Not found this id!");
                        return View();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var role = await repository.GetRole(id);
            if (role == null) {
                ModelState.AddModelError("", "Error! Not found this id!");
                return View();
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await repository.GetRole(id);
            repository.DeleteRole(role);
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
            return context.Roles.Any(e => e.RoleId == id);
        }
    }
}
