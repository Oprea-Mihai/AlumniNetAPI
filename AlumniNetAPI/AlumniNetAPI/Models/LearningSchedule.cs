using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class LearningSchedule
{
    public int LearningScheduleId { get; set; }

    public string ScheduleName { get; set; } = null!;

    public virtual ICollection<FinishedStudy> FinishedStudies { get; } = new List<FinishedStudy>();
}
