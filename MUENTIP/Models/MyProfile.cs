using MUENTIP.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace MUENTIP.Models
{
    public class MyProfile
    {
        public int Id { get; set; }

        public string Picture { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(150)] 
        public string Name { get; set; }


        public string Detail { get; set; }

        [Required]
        public DateTime Birthday { get; set; } 

        [Required]
        public Gender Gender { get; set; } 


        public string Education { get; set; }
        public string Address { get; set; }

        public List<TagFilterViewModel> Interest { get; set; }

    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
