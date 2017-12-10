using Jaxx.VideoDbNetStandard.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard.MySql
{
    public class EnhancedVideoDbContext : DbContext
    {
        public EnhancedVideoDbContext(DbContextOptions<EnhancedVideoDbContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<homewebbridge_usermoviesettings>()
                .ToTable("homewebbridge_usermoviesettings");

            modelBuilder.Entity<homewebbridge_userseen>()
               .ToTable("homewebbridge_userseen");          
        }

        public DbSet<homewebbridge_usermoviesettings> UserSettings { get; set; }
    }

    /// <summary>
    /// Factory class for EmployeesContext
    /// </summary>
    public static class EnhancedVideoDbContextFactory
    {
        public static EnhancedVideoDbContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EnhancedVideoDbContext>();
            optionsBuilder.UseMySql(connectionString);

            //Ensure database creation
            var context = new EnhancedVideoDbContext(optionsBuilder.Options);
            //context.Database.EnsureCreated();

            return context;
        }
    }

}
