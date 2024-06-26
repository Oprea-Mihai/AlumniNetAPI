﻿using System;
using System.Collections.Generic;

namespace AlumniNetAPI.Models;

public partial class Faculty
{
    public int FacultyId { get; set; }

    public string FacultyName { get; set; } = null!;

    public virtual ICollection<Specialization> Specializations { get; } = new List<Specialization>();
}
