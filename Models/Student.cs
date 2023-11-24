namespace ApiUniversity.Models;

public class Student
{
    public int Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime EnrollmentDate { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new();

    // Default constructor
    public Student() { }

    public Student(StudentDTO student)
    {
        Id = student.Id;
        FirstName = student.FirstName;
        LastName = student.LastName;
        Email = student.Email;
        EnrollmentDate = student.EnrollmentDate;
    }
}
