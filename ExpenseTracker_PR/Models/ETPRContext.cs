using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker_PR.Models
{
    public class ETPRContext:DbContext
    {
        public ETPRContext(DbContextOptions options):base(options) { }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ExpenseModel> Expenses { get; set; }
    }
}
