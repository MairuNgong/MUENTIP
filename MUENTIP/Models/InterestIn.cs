using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MUENTIP.Models
{
    public class InterestIn
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Tag")]
        public string? TagName { get; set; }
        public virtual Tag? Tag { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }


    }
}
