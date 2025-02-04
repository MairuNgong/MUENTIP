using System.ComponentModel.DataAnnotations;

namespace MUENTIP.Models
{
    public class Tag
    {
        [Key]
        public string TagName { get; set; }

        public virtual ICollection<InterestIn> InterestedTags { get; set;}

        public virtual ICollection<ActivityType> ActivityTags { get; set; }
    }
}
