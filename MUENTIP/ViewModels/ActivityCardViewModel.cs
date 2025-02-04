using System;
using System.Collections.Generic;

public class ActivityCardViewModel
{
    public int ActivityId { get; set; }
    public string Title { get; set; }
    public string Owner { get; set; } // User.Username
    public string Location { get; set; }
    public string ActivityDateTime { get; set; } // Formatted DateTime
    public string DeadlineDateTime { get; set; }
    public int ApplyMax { get; set; }
    public int ApplyCount { get; set; } // Number of Applications
    public List<string> TagsList { get; set; } // Activity Tags
}