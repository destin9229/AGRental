using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AGRental.Models;


namespace AGRental.Data
{
    public class AGRentalDBContext : DbContext
    {

        public AGRentalDBContext(DbContextOptions<AGRentalDBContext> options)
                : base(options)
        { }


        public DbSet<User> Users { get; set; }
        public DbSet<Properties> Properties { get; set; }
        public DbSet<UserProperties> UserProperties { get; set; }
        public DbSet<UserType> UserType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<UserProperties>()
           .HasKey(c => new { c.UserID, c.PropertyID });

            modelBuilder.Entity<UserType>().HasData(
                new UserType { UserTypeID = 1, UserTypeName = "admin" },
                new UserType { UserTypeID = 2, UserTypeName = "user" });

             modelBuilder.Entity<User>().HasData(
                 new User { User_ID = 1, Username = "admin", FirstName = "Destin", LastName = "Thomas", Email = "", Password = "Qwer1234", UserTypeID = 1 });
        }

    }
}