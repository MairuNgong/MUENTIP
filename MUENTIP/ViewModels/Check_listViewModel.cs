﻿using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace MUENTIP.ViewModels
{
    public class Check_listViewModel
    {
        public string? ownerId { get; set; }
        public string? ownerImgLink { get; set; }
        public int? applyMax { get; set; }
        public string? email { get; set; }

        public int activity_id { get; set; }

        public List<UserCardViewModel>? Appliers { get; set; }


    }
}
