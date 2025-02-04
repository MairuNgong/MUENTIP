namespace MUENTIP.ViewModels
{
    public class SelectViewModel
    {
        public int ownerId { get; set; }
        public string ownerImgLink { get; set; }
        public int applyMax { get; set; }

        public List<UserCardViewModel> Appliers { get; set; }


    }
}
