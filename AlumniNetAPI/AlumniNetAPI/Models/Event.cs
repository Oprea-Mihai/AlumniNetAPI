using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class Event
{
    public int EventId { get; set; }

    public string Initiator { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public string Description { get; set; } = null!;

    public string? Image { get; set; }

    public virtual ICollection<InvitedUser> InvitedUsers { get; } = new List<InvitedUser>();
}
