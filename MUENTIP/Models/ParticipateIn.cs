using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MUENTIP.Models
{
    public class ParticipateIn
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Activity")]
        public int? ActivityId { get; set; }
        public virtual Activity? Activity { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}
