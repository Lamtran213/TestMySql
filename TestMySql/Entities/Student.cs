using System;
using System.Collections.Generic;

namespace TestMySql.Entities;

public partial class Student
{
    public int StudentId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly? EnrollmentDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
