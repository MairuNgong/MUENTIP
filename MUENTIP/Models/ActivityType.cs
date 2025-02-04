using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MUENTIP.Models
{
    public class ActivityType
    {
        [Required]
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        [Required]
        [ForeignKey("Tag")]
        public int TagName { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
