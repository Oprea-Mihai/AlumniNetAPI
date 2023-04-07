using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class Experience
{
    public int ExperienceId { get; set; }

    public int ProfileId { get; set; }

    public string JobTitle { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Profile Profile { get; set; } = null!;
}
