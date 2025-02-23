namespace MUENTIP.ViewModels
{
    public class ViewActivityViewModel
    {
        public string UserName { get; set; }
        public ActivityCardViewModel Card { get; set; }
        public List<AnnouncementViewModel> Announcements { get; set; }
    }
}