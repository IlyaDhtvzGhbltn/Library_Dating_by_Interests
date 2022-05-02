using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Entities
{
    public class LibraryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryDB;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerYoutubeChanell>()
                .HasKey(customerChannel => new { 
                    customerChannel.CustomerId, 
                    customerChannel.YoutubeChanellId });

            modelBuilder.Entity<CustomerYoutubeChanell>()
                .HasOne(customerChannel => customerChannel.Customer)
                .WithMany(customer => customer.Subscriptions)
                .HasForeignKey(customerChannel => customerChannel.CustomerId);

            modelBuilder.Entity<CustomerYoutubeChanell>()
                .HasOne(customerChanell => customerChanell.YoutubeChanell)
                .WithMany(chanell => chanell.Customers)
                .HasForeignKey(customerChannel => customerChannel.YoutubeChanellId);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<YoutubeChanell> YoutubeChanells { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }

        public DbSet<CustomerYoutubeChanell> CustomerYoutubeChanells { get; set; }
    }
}
