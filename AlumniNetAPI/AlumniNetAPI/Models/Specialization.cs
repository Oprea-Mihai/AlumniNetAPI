using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class Specialization
{
    public int SpecializationId { get; set; }

    public int FacultyId { get; set; }

    public string? SpecializationName { get; set; }

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual ICollection<FinishedStudy> FinishedStudies { get; } = new List<FinishedStudy>();
}
