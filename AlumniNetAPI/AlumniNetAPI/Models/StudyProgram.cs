using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class StudyProgram
{
    public int StudyProgramId { get; set; }

    public string ProgramName { get; set; } = null!;

    public virtual ICollection<FinishedStudy> FinishedStudies { get; } = new List<FinishedStudy>();
}
