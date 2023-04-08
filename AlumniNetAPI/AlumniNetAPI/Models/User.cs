using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool IsValid { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? FirebaseAuthToken { get; set; }

    public int ProfileId { get; set; }

    public virtual ICollection<Post> Posts { get; } = new List<Post>();

    public virtual Profile Profile { get; set; } = null!;
}
