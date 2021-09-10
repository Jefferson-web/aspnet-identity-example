using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Interfaces;
using TodoList.Models;
using TodoList.Repositories;

namespace TodoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoItemRepository repository;

        public TodoController(ITodoItemRepository repository)
        {
            this.repository = repository;
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addItem(TodoItem newItem) {
            if (!ModelState.IsValid) {
                return RedirectToAction("Index");
            }
            var successful = await repository.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest("Could not add item.");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> MarkDone(Guid id) {
            if (id == Guid.Empty) {
                return RedirectToAction("Index");
            }
            var successful = await repository.MarkDoneAsync(id);
            if (!successful) {
                return BadRequest("Could not mark item as done.");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index()
        {
            var items = await repository.GetIncompleteItemsAsync();
            return View(items);
        }
    }
}
