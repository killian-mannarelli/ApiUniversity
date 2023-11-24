namespace ApiUniversity.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Credits { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new();
    public List<Instructor> Instructors { get; set; } = new();

    public Department Department { get; set; } = new();

    public Course() { }

    public Course(CourseDTO courseDTO)
    {
        Id = courseDTO.Id;
        Title = courseDTO.Title;
        Credits = courseDTO.Credits;
    }
}
