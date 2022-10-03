using Microsoft.EntityFrameworkCore;

namespace trackingapi.Models
{
    public class ExamDbContext : DbContext
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options)
        {

        }

        

        public DbSet<Cal> Cals { get; set; }
    }
}
