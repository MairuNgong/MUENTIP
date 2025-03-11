namespace MUENTIP.ViewModels
{
    public class ViewActivityViewModel
    {
        public string UserName { get; set; }
        public string OwnerImg { get; set; }
        public string OwnerId { get; set; }
        public bool? IsApplyOn { get; set; } 
        public bool? IsSelected { get; set; } 
        public string ParticipationStatus { get; set; }
        public Boolean OutOfDate { get; set; }
        public ActivityCardViewModel Card { get; set; }
        public List<AnnouncementViewModel> Announcements { get; set; }
    }
}