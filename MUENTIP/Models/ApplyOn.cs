using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MUENTIP.Models
{
    public class ApplyOn
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Activity")]
        public int ActivityId { get; set; } // Foreign key to Activity
        public virtual Activity Activity { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; } // Foreign key to User
        public virtual User? User { get; set; }
        public DateTime AppliedDate { get; set; }

        // Navigation properties
        
        
        
    }
}
