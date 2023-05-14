using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<InvitedUser> InvitedUsers { get; } = new List<InvitedUser>();
}
