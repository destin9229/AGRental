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
        //Links the User table amd sets it to Users
        public DbSet<User> Users { get; set; }

        //Links the Properties table amd sets it to Properties
        public DbSet<Properties> Properties { get; set; }

        //Links the UserProperties table amd sets it to UserProperties
        public DbSet<UserProperties> UserProperties { get; set; }

        //Links the UserType table amd sets it to UserType
        public DbSet<UserType> UserType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

<<<<<<< HEAD
           //Sets UserID and PropertyID as a primary key
           modelBuilder.Entity<UserProperties>()
           .HasKey(c => new { c.UserID, c.PropertyID });

           //Sets the UserTypeID between admin and user
           modelBuilder.Entity<UserType>().HasData(
                new UserType { UserTypeID = 1, UserTypeName = "admin" },
                new UserType { UserTypeID = 2, UserTypeName = "user" });

           //Sets the admin default values
           modelBuilder.Entity<User>().HasData(
                 new User { User_ID = 1, Username = "admin", FirstName = "Destin", LastName = "Thomas", Email = "", Password = "Qwer1234", UserTypeID = 1 });
=======
            //Sets UserID and PropertyID as a primary key
            modelBuilder.Entity<UserProperties>()
            .HasKey(c => new { c.UserID, c.PropertyID });

            //Sets the UserTypeID between admin and user
            modelBuilder.Entity<UserType>().HasData(
                 new UserType { UserTypeID = 1, UserTypeName = "admin" },
                 new UserType { UserTypeID = 2, UserTypeName = "user" });

            //Sets the admin default values
            modelBuilder.Entity<User>().HasData(
                  new User { User_ID = 1, Username = "admin", FirstName = "Destin", LastName = "Thomas", Email = "", Password = "Qwer1234", UserTypeID = 1 });
>>>>>>> Project Working with Images
        }

    }
}