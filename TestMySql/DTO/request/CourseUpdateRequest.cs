using System.ComponentModel.DataAnnotations;

namespace TestMySql.DTO.request;

public class CourseUpdateRequest
{
    [Required]
    [StringLength(50)]
    public string CourseName { get; set; } = null!;
    [Required]
    [StringLength(500)]
    public string CourseDescription { get; set; } = null!;
    [Required]
    [Range(1, 10)]
    public int Credits { get; set; }
    public DateOnly CreatedDate { get; set; }
}