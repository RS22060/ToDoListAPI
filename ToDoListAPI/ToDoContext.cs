using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Models;

namespace ToDoListAPI
{
    public class ToDoContext : DbContext
    {

        public ToDoContext(DbContextOptions<ToDoContext> options) 
            : base(options)
        {
        }

        public DbSet<Note> ToDos { get; set; }
    }
}
