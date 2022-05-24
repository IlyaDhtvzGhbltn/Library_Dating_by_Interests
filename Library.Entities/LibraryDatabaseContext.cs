using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class LibraryDatabaseContext : IdentityDbContext /*DbContext*/
    {
        public LibraryDatabaseContext(DbContextOptions options) : base(options)
        {}

        public DbSet<ApiUser> LibraryUsers { get; set; }
        public DbSet<DatingCriteria> DatingCriterias { get; set; }
        public DbSet<YoutubeChanell> YoutubeChanells { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
