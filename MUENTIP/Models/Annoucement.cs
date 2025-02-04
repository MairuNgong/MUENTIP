using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MUENTIP.Models
{
    public class Annoucement
    {
        [Required]
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }

        public virtual Activity Activity { get; set; }

        [Required]
        public int AnnoucementId { get; set; }

        [Required]
        public DateTime AnnouceDate { get; set; }

        [Required]
        public string Content { get; set;}
    }
}
