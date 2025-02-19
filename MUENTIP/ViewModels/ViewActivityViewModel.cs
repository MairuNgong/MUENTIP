namespace MUENTIP.ViewModels
{
    public class ViewActivityViewModel
    {
        public string UserId { get; set; }
        public ActivityCardViewModel Card { get; set; }
        public List<AnnouncementViewModel> Announcements { get; set; }
    }
}