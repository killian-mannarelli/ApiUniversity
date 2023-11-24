namespace ApiUniversity.Models;

// Data Transfer Object class, used to bypass navigation properties validation during API calls
public class EnrollmentDTO
{
    public int Id { get; set; }
    public Grade? Grade { get; set; } = null!;
    public int StudentId { get; set; }
    public int CourseId { get; set; }

    public EnrollmentDTO() { }

    public EnrollmentDTO(Enrollment enrollment)
    {
        Id = enrollment.Id;
        Grade = enrollment.Grade;
        StudentId = enrollment.Student.Id;
        CourseId = enrollment.Course.Id;
    }
}
