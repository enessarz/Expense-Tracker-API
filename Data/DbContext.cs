using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        public DbSet<User> User {get; set;}
        public DbSet<ExpenseModel> Expense {get; set;}
    }
}