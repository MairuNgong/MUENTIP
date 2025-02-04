using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MUENTIP.Models;

namespace MUENTIP.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplyOn>()
                .HasKey(a => new { a.ActivityId, a.UserId }); // Composite key
            modelBuilder.Entity<ActivityType>()
                .HasKey(a => new { a.ActivityId, a.TagName });
            modelBuilder.Entity<Annoucement>()
                .HasKey(a => new { a.AnnoucementId, a.ActivityId });
            modelBuilder.Entity<InterestIn>()
                .HasKey(a => new { a.UserId, a.TagName });
            modelBuilder.Entity<ParticipateIn>()
                .HasKey(a => new {a.ActivityId, a.UserId});
        }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Annoucement> Annoucements { get; set;}
        public DbSet<ApplyOn> ApplyOn { get; set; }
        public DbSet<InterestIn> InterestIn { get; set; }
        public DbSet<ParticipateIn> ParticipateIn { get; set;}


    }
}
