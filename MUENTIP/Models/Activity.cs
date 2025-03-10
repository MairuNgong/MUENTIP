using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MUENTIP.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        [Required]
        public DateTime DeadlineDateTime { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public DateTime PostDateTime { get; set; }

        [Required]
        [Range(0,200)]
        public int ApplyMax { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<ApplyOn> Applications { get; set; }

        public virtual ICollection<ParticipateIn>? Participations { get; set; }

        public virtual ICollection<ActivityType> ActivityTags { get; set; }

        public virtual ICollection<Annoucement> Annoucements { get; set; }

    }
}
