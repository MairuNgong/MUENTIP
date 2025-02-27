using System.ComponentModel.DataAnnotations;

using MUENTIP.Models;

namespace MUENTIP.ViewModels
{
    public class MyProfileViewModel
    {
        
        [Required]
        public string UserName { get; set; }
        public string? ProfileImageLink { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Info { get; set; }

        public string? Gender { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Education { get; set; }

        public string? Address { get; set; }

        public List<string>  InterestedTags { get; set; }
        public List<ActivityCardViewModel> createdActivity { get; set; }
        public List<ActivityCardViewModel> approvedActivity { get; set; }

        public List<TagFilterViewModel> availableTags { get; set; }

    }
}
