using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Data
{
    public class TodoContext:IdentityDbContext
    {
        public TodoContext(DbContextOptions options) : base(options) { }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
