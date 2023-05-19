using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public int ProfileId { get; set; }

    public string Language { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public bool IsValid { get; set; }

    public string? DeviceNotificationToken { get; set; }

    public virtual ICollection<InvitedUser> InvitedUsers { get; } = new List<InvitedUser>();

    public virtual ICollection<Post> Posts { get; } = new List<Post>();

    public virtual Profile Profile { get; set; } = null!;
}
