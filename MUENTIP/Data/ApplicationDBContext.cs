using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MUENTIP.Models;

namespace MUENTIP.Data
{
    public class ApplicationDBContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Annoucement> Annoucements { get; set;}
        public DbSet<ApplyOn> ApplyOn { get; set; }
        public DbSet<InterestIn> InterestIn { get; set; }
        public DbSet<ParticipateIn> ParticipateIn { get; set;}

    }
}
