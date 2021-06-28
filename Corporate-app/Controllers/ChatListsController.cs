using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Corporate_app.Models;
using Corporate_app.Models.Context;

namespace Corporate_app.Controllers
{
    public class ChatListsController : Controller
    {
        private readonly ModelsContext _context;

        public ChatListsController(ModelsContext context)

        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ChatLists.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatList = await _context.ChatLists
                .FirstOrDefaultAsync(m => m.ChatListId == id);
            if (chatList == null)
            {
                return NotFound();
            }

            return View(chatList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChatListId,Name")] ChatList chatList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chatList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chatList);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatList = await _context.ChatLists.FindAsync(id);
            if (chatList == null)
            {
                return NotFound();
            }
            return View(chatList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChatListId,Name")] ChatList chatList)
        {
            if (id != chatList.ChatListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatListExists(chatList.ChatListId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(chatList);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatList = await _context.ChatLists
                .FirstOrDefaultAsync(m => m.ChatListId == id);
            if (chatList == null)
            {
                return NotFound();
            }

            return View(chatList);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chatList = await _context.ChatLists.FindAsync(id);
            _context.ChatLists.Remove(chatList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatListExists(int id)
        {
            return _context.ChatLists.Any(e => e.ChatListId == id);
        }
    }
}
