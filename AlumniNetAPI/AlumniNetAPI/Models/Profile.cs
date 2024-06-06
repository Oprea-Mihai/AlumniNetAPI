using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class Profile
{
    public int ProfileId { get; set; }

    public string? ProfilePicture { get; set; }

    public string? Description { get; set; }

    public string? Instagram { get; set; }

    public string? Facebook { get; set; }

    public string? LinkedIn { get; set; }

    public virtual ICollection<Experience> Experiences { get; } = new List<Experience>();

    public virtual ICollection<FinishedStudy> FinishedStudies { get; } = new List<FinishedStudy>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
