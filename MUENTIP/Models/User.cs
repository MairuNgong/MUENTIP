using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MUENTIP.Models
{
    public class User: IdentityUser
    {
        
        public string? ProfileImageLink { get; set; }
        public string? Info { get; set; }

        public string? Gender { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Education { get; set; }

        public string? Address { get; set; }

        public virtual ICollection<Activity> CreatedActivities { get; set; } // Navigation property

        public virtual ICollection<ApplyOn> Applications { get; set; } // Many-to-Many relation

        public virtual ICollection<ParticipateIn> Participations { get; set; }

        public virtual ICollection<InterestIn> InterestedTags { get; set; }
    }
}
