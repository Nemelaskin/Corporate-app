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
    public class ChatListsController : Controller
    {
        private readonly ModelsContext _context;
        private readonly ChatListRepository repository;

        public ChatListsController(ModelsContext context, ChatListRepository repository)
        {
            _context = context;
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var modelsContext = repository.GetChatLists();
            return View(await modelsContext);
        }

        public async Task<IActionResult> Details(int id)
        {
            var chats = await repository.GetChatList(id);
            if (chats == null) {
                return RedirectToAction("Index", "ChatLists");
            }
            return View(chats);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChatListId,Name")] ChatList chatList)
        {
            if (ModelState.IsValid) {
                repository.SaveChatList(chatList);
                return RedirectToAction(nameof(Index));
            }
            return View(chatList);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var chats = await repository.GetChatList(id);
            if (chats == null) {
                ModelState.AddModelError("", "Error! Not found this id!");
                return View();
            }
            return View(chats);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChatListId,Name")] ChatList chatList)
        {
            if (id != chatList.ChatListId) {
                ModelState.AddModelError("", "Error! Not found this id!");
                return View();
            }

            if (ModelState.IsValid) {
                try {
                    repository.SaveChatList(chatList);
                }
                catch (DbUpdateConcurrencyException) {
                    if (!ChatListExists(chatList.ChatListId)) {
                        ModelState.AddModelError("", "Error! Not found this id!");
                        return View();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(chatList);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var chats = await repository.GetChatList(id);
            if (chats == null) {
                ModelState.AddModelError("", "Error! Not found this id!");
                return View();
            }

            return View(chats);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chats = await repository.GetChatList(id);
            repository.DeleteChatList(chats);
            return RedirectToAction(nameof(Index));
        }

        private bool ChatListExists(int id)
        {
            return _context.ChatLists.Any(e => e.ChatListId == id);
        }
    }
}
