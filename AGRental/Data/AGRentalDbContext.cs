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
        public DbSet<UserType> Types { get; set; }

    }
}