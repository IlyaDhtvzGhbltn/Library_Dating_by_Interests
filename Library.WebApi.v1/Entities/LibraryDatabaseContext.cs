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

        public DbSet<User> LibraryUsers { get; set; }
        public DbSet<YoutubeChanell> YoutubeChanells { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
