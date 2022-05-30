using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Library.Entities
{
    public class LibraryDatabaseContext : IdentityDbContext /*DbContext*/
    {
        public LibraryDatabaseContext(DbContextOptions options) : base(options)
        {}

        public DbSet<ApiUser> ApiUsers { get; set; }
        public DbSet<DatingCriteriaEntry> DatingCriterias { get; set; }
        public DbSet<YoutubeChanell> YoutubeChanells { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UsersRelation> UsersRelations { get; set; }
        public DbSet<ApiUser_YoutubeChannel> ApiUserYoutubeChannel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApiUser_YoutubeChannel>()
                .HasKey(ac => new { ac.ApiUserId, ac.YoutubeChannelId });


            builder.Entity<ApiUser_YoutubeChannel>()
                .HasOne(ac => ac.ApiUser)
                .WithMany(a => a.ApiUsers_YoutubeChannels)
                .HasForeignKey(ac => ac.ApiUserId);            
            
            builder.Entity<ApiUser_YoutubeChannel>()
                .HasOne(ac => ac.YoutubeChanell)
                .WithMany(a => a.ApiUsers_YoutubeChannels)
                .HasForeignKey(ac => ac.YoutubeChannelId);
        }
    }
}
