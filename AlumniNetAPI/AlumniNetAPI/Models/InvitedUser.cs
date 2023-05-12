using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class InvitedUser
{
    public int InvitedUserId { get; set; }

    public int EventId { get; set; }

    public string UserId { get; set; } = null!;

    public int Status { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Status StatusNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
