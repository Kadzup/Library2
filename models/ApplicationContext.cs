using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library2
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookOrder> BookOrders { get; set; }

        const string Host = "localhost";
        const string Db = "library";
        const string User = "root";
        const string Password = "";

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql($"Database={Db};Datasource={Host};User={User};Password={Password}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookOrder>()
                .HasOne(order => order.User)
                .WithMany(o => o.Orders)
                .HasForeignKey(order => order.UserTicket);

            modelBuilder.Entity<BookOrder>()
                .HasOne(order => order.Book)
                .WithMany(o => o.Orders)
                .HasForeignKey(order => order.BookId);
        }
    }
}