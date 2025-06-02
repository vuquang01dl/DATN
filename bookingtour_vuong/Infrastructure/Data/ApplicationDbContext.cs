using Microsoft.EntityFrameworkCore;
using System;
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
        public DbSet<TourStatusLog> TourStatusLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Khóa chính cho bảng nhiều-nhiều
            modelBuilder.Entity<TourHotel>()
                .HasKey(th => new { th.TourId, th.HotelId });

            modelBuilder.Entity<TourEmployee>()
                .HasKey(te => new { te.TourId, te.EmployeeId });

            // Cấu hình kiểu tiền tệ
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Tour>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18,2)");

            // ⚠️ Tránh multiple cascade path
            modelBuilder.Entity<Payment>()
                .HasOne<Customer>() // Không cần navigation ngược
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.CustomerID)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Tránh cascade

            // Quan hệ Booking → Payment
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Payment)
                .WithMany() // Không có navigation ngược
                .HasForeignKey(b => b.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ Booking → Customer
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
