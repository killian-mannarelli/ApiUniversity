namespace ApiUniversity.Models;

// Data Transfer Object class, used to bypass navigation properties validation during API calls
public class DetailedCourseDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Credits { get; set; }
    public DepartmentDTO Department { get; set; } = null!;

    public DetailedCourseDTO() { }

    public DetailedCourseDTO(Course course)
    {
        Id = course.Id;
        Title = course.Title;
        Credits = course.Credits;
        Department = new DepartmentDTO(course.Department);
    }
}
