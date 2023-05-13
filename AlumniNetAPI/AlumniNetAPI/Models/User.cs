using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool IsValid { get; set; }

    public string? Email { get; set; }

    public int ProfileId { get; set; }

    public virtual ICollection<Post> Posts { get; } = new List<Post>();

    public virtual Profile Profile { get; set; } = null!;
}
