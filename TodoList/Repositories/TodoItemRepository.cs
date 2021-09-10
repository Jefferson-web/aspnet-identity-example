using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Data;
using TodoList.Interfaces;
using TodoList.Models;

namespace TodoList.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext context;

        public TodoItemRepository(TodoContext context) {
            this.context = context;
        }

        public async Task<bool> AddItemAsync(TodoItem newItem)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTime.Now.AddDays(3);
            context.Add(newItem);
            var result = await context.SaveChangesAsync();
            return result == 1;
        }

        public async Task<ICollection<TodoItem>> GetIncompleteItemsAsync()
        {
            return await context.TodoItems
                .Where(t => t.IsDone == false)
                .ToListAsync();
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
           var item =  await context.TodoItems
                            .Where(item => item.Id == id)
                            .SingleOrDefaultAsync();
            if (item is null) return false;
            item.IsDone = true;
            var result = await context.SaveChangesAsync();
            return result == 1;
        }
    }
}
