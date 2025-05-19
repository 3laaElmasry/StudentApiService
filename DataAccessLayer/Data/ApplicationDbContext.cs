using Microsoft.EntityFrameworkCore;
using StudentApi.Models;

namespace DataAccessLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasData(new List<Student>()
            {
                 new Student { Id = 1, Name = "Ali Ahmed", Age = 20,Grade=88 },
                 new Student { Id = 2, Name = "Fadi Khail", Age = 22,Grade=77 },
                 new Student { Id = 3, Name = "Ola Jaber", Age = 21 , Grade = 66},
                 new Student { Id = 4, Name = "Alia Maher", Age = 19,Grade=44 }
            });
        }
    }
}
