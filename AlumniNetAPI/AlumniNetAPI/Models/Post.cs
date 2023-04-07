﻿using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class Post
{
    public int PostId { get; set; }

    public int UserId { get; set; }

    public string? Image { get; set; }

    public string? Text { get; set; }

    public string? Title { get; set; }

    public virtual User User { get; set; } = null!;
}
