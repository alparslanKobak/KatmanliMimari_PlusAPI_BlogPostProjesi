using Microsoft.EntityFrameworkCore;
using P013KatmanliBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace P013KatmanliBlog.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database= P013KatmanliBlog; Trusted_Connection = True ");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Email = "admin@KatmanliBlog.com",
                IsActive = true,
                IsAdmin = true,
                Name = "Admin",
                Password = "123",
                UserName = "Admin",
                UserGuid = Guid.NewGuid()
            }
                );


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }


    }
}
