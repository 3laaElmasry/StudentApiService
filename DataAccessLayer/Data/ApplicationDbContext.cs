using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using StudentApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DataAccessLayer.Identity;

namespace DataAccessLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<City> Cities { get; set; }
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

            modelBuilder.Entity<City>().HasData(new List<City>()
            {
                new City()
                {
                    CityId = Guid.Parse("c1bc9a62-8492-4d69-89da-75eae46a658b"),
                    CityName = "New York",
                },
                 new City()
                {
                    CityId = Guid.Parse("0386b09a-7536-4fc9-b079-f9a637096abf"),
                    CityName = "London",
                }
            });

        }
    }
}
