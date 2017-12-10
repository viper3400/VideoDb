using Microsoft.EntityFrameworkCore;
using Jaxx.VideoDbNetStandard.DatabaseModel;


namespace Jaxx.VideoDbNetStandard.MySql
{
    public class VideoDbContext : DbContext
    {
        public VideoDbContext(DbContextOptions<VideoDbContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<videodb_actors>()
                .ToTable("videodb_actors")
                .HasKey(t => t.actorid);

            modelBuilder.Entity<videodb_cache>()
                .ToTable("videodb_cache")
                .HasKey(t => t.tag);

            modelBuilder.Entity<videodb_config>()
                .ToTable("videodb_config")
                .HasKey(t => t.opt);

            modelBuilder.Entity<videodb_videogenre>()
                .ToTable("videodb_videogenre")
                .HasKey(t => new { t.video_id, t.genre_id });
            modelBuilder.Entity<videodb_genres>()
                .ToTable("videodb_genres");

            modelBuilder.Entity<videodb_lent>()
                .ToTable("videodb_lent")
                .HasKey(t => t.diskid);

            modelBuilder.Entity<videodb_mediatypes>()
                .ToTable("videodb_mediatypes");

            modelBuilder.Entity<videodb_permissions>()
                .ToTable("videodb_permissions")
                .HasKey(t => new { t.from_uid, t.to_uid });

            modelBuilder.Entity<videodb_userconfig>()
                .ToTable("videodb_userconfig")
                .HasKey(t => new { t.user_id, t.opt });

            modelBuilder.Entity<videodb_users>()
                .ToTable("videodb_users");

            modelBuilder.Entity<videodb_userseen>()
                .ToTable("videodb_userseen")
                .HasKey(t => new { t.user_id, t.video_id });

            modelBuilder.Entity<videodb_videodata>()
                .ToTable("videodb_videodata");

            modelBuilder.Entity<videodb_videodata>()
                .Property(p => p.plot).HasColumnType("text");

            MapForeignKeys(modelBuilder);
        }

        private void MapForeignKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<videodb_videodata>()
                .HasOne(o => o.VideoOwner)
                .WithMany(p => p.UserVideos)
                .HasForeignKey(f => f.owner_id);

            modelBuilder.Entity<videodb_users>()
                .HasMany(v => v.UserVideos)
                .WithOne(u => u.VideoOwner);
        }

        public DbSet<videodb_actors> Actors { get; set; }
        public DbSet<videodb_cache> Cache { get; set; }
        public DbSet<videodb_config> Config { get; set; }
        public DbSet<videodb_videogenre> Genre { get; set; }
        public DbSet<videodb_genres> Genres { get; set; }
        public DbSet<videodb_lent> Lent { get; set; }
        public DbSet<videodb_mediatypes> MediaTypes { get; set; }
        public DbSet<videodb_permissions> Permissions { get; set; }
        public DbSet<videodb_userconfig> UserConfig { get; set; }
        public DbSet<videodb_users> Users { get; set; }
        public DbSet<videodb_userseen> UserSeen { get; set; }
        public DbSet<videodb_videodata> VideoData { get; set; }     
        
    }

    /// <summary>
    /// Factory class for EmployeesContext
    /// </summary>
    public static class VideoDbContextFactory
    {
        public static VideoDbContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideoDbContext>();
            optionsBuilder.UseMySql(connectionString);

            //Ensure database creation
            var context = new VideoDbContext(optionsBuilder.Options);
            //context.Database.EnsureCreated();

            return context;
        }
    }
}
