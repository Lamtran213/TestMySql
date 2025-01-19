using System;
using System.Collections.Generic;

namespace TestMySql.Entities;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string? Description { get; set; }

    public int Credits { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
