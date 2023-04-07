using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class FinishedStudy
{
    public int FinishedStudyId { get; set; }

    public int SpecializationId { get; set; }

    public int LearningScheduleId { get; set; }

    public int StudyProgramId { get; set; }

    public int Year { get; set; }

    public int ProfileId { get; set; }

    public virtual LearningSchedule LearningSchedule { get; set; } = null!;

    public virtual Profile Profile { get; set; } = null!;

    public virtual Specialization Specialization { get; set; } = null!;

    public virtual StudyProgram StudyProgram { get; set; } = null!;
}
