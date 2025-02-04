using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MUENTIP.Models
{
    public class InterestIn
    {
        [Required]
        [ForeignKey("Tag")]
        public int TagName { get; set; }
        public virtual Tag Tag { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }


    }
}
