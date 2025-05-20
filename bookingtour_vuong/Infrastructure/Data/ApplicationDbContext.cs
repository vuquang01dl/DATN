using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;


namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Tour> Tours { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<TourHotel> TourHotels { get; set; }
        public DbSet<TourEmployee> TourEmployees { get; set; }
        public DbSet<TourDestination> TourDestinations { get; set; }
        public DbSet<Account> Accounts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TourHotel>()
                .HasKey(th => new { th.TourID, th.HotelID });
            modelBuilder.Entity<TourEmployee>()
                .HasKey(te => new { te.TourID, te.EmployeeID });
        }





    }
}