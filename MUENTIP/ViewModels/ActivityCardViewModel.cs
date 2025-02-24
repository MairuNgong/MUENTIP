using System;
using System.Collections.Generic;

public class ActivityCardViewModel
{
    public int ActivityId { get; set; }
    public string Title { get; set; }
    public string Owner { get; set; }
    public string Location { get; set; }
    public string StartDateTime { get; set; }
    public string EndDateTime { get; set; }
    public string PostDateTime { get; set; }
    public string DeadlineDateTime { get; set; }
    public int ApplyMax { get; set; }
    public int ApplyCount { get; set; }
    public List<string> TagsList { get; set; }
    public string Description { get; set; }
}
