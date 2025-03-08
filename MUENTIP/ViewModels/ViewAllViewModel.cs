namespace MUENTIP.ViewModels
{
    public class ViewAllViewModel
    {
        public string TagName { get; set; }
        public List<ActivityCardViewModel> Cards { get; set; }
        public List<TagFilterViewModel> FilterTags { get; set; }
    }
}