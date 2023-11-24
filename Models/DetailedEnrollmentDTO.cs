namespace ApiUniversity.Models;

// Data Transfer Object class, used to bypass navigation properties validation during API calls
public class DetailedEnrollmentDTO
{
    public int Id { get; set; }
    public Grade? Grade { get; set; } = null!;
    public StudentDTO Student { get; set; } = null!;
    public CourseDTO Course { get; set; } = null!;

    public DetailedEnrollmentDTO() { }

    public DetailedEnrollmentDTO(Enrollment enrollment)
    {
        Id = enrollment.Id;
        Grade = enrollment.Grade;
        Student = new StudentDTO(enrollment.Student);
        Course = new CourseDTO(enrollment.Course);
    }
}
