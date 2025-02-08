using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MUENTIP.Models
{
    public class ActivityType
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public virtual required Activity Activity { get; set; }

        [Required]
        [ForeignKey("Tag")]
        public string? TagName { get; set; }
        public virtual Tag? Tag { get; set; }
    }
}
