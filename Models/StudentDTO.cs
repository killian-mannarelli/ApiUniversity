namespace ApiUniversity.Models;

// Data Transfer Object class, used to bypass navigation properties validation during API calls
public class StudentDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; }

    public StudentDTO() { }

    public StudentDTO(Student student)
    {
        Id = student.Id;
        FirstName = student.FirstName;
        LastName = student.LastName;
        Email = student.Email;
        EnrollmentDate = student.EnrollmentDate;
    }
}
