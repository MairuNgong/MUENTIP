using MUENTIP.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MUENTIP.ViewModels
{
    public class EditActivityViewModel
    {
        public int ActivityId { get; set; }

        public string Title { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public DateTime DeadlineDateTime { get; set; }
        public string Location { get; set; }

        public int ApplyMax { get; set; }

        public string? Description { get; set; }

        public List<string>? ActivityTags { get; set; }

        public List<string>? AllTags { get; set; }


    }
}
