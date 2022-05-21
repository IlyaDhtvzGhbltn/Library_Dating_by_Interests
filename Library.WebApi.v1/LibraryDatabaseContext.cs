using Library.WebApi.v1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.v1
{
    public class LibraryDatabaseContext : IdentityDbContext
    {
        public LibraryDatabaseContext(DbContextOptions options) : base(options)
        {}

        public DbSet<ApiUser> ApiUsers { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<YoutubeChanell> YoutubeChanells { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<CustomerYoutubeChanell> CustomerYoutubeChanells { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerYoutubeChanell>()
                .HasKey(customerChannel => new {
                    customerChannel.UserId,
                    customerChannel.YoutubeChanellId
                });

            modelBuilder.Entity<CustomerYoutubeChanell>()
                .HasOne(customerChannel => customerChannel.User)
                .WithMany(customer => customer.Subscriptions)
                .HasForeignKey(customerChannel => customerChannel.UserId);

            modelBuilder.Entity<CustomerYoutubeChanell>()
                .HasOne(customerChanell => customerChanell.YoutubeChanell)
                .WithMany(chanell => chanell.Customers)
                .HasForeignKey(customerChannel => customerChannel.YoutubeChanellId);
        }

    }
}
