namespace MUENTIP.ViewModels
{
    public class MyActivityViewModel
    {
        public int userId { get; set; }

        public List<ActivityCardViewModel> activity { get; set; }
        public List<ActivityCardViewModel> createdActivity { get; set; }
        public List<ActivityCardViewModel> approvedActivity { get; set; }
        public List<ActivityCardViewModel> nonApproveActivity { get; set; }

        public List<TagFilterViewModel> filterTags { get; set; }
    }
}
