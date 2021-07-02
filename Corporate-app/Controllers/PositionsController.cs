using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Corporate_app.Models;
using Corporate_app.Repositories;
using Corporate_app.Models.Context;

namespace Corporate_app.Controllers
{
    public class PositionsController : Controller
    {
        private readonly ModelsContext _context;
        private readonly PositionRepository repository;

        public PositionsController(ModelsContext context, PositionRepository repository)
        {
            _context = context;
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var modelsContext = repository.GetPositions();
            return View(await modelsContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var position = await repository.GetPosition(id);
            if (position == null) {

                return RedirectToAction("Index", "Positions");
            }
            return View(position);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PositionId,PositionName")] Position position)
        {
            if (ModelState.IsValid) {
                repository.SavePosition(position);
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var position = await repository.GetPosition(id);
            if (position == null) {
                ModelState.AddModelError("", "Error! Not found this id!");
                return View();
            }
            return View(position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PositionId,PositionName")] Position position)
        {
            if (id != position.PositionId) {
                ModelState.AddModelError("", "Error! Not found this id!");
                return View();
            }

            if (ModelState.IsValid) {
                try {
                    repository.SavePosition(position);
                }
                catch (DbUpdateConcurrencyException) {
                    if (!PositionExists(position.PositionId)) {
                        ModelState.AddModelError("", "Error! Not found this id!");
                        return View();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var position = await repository.GetPosition(id);
            if (position == null) {
                ModelState.AddModelError("", "Error! Not found this id!");
                return View();
            }

            return View(position);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var position = await repository.GetPosition(id);
            repository.DeletePosition(position);
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(int id)
        {
            return _context.Positions.Any(e => e.PositionId == id);
        }
    }
}
