namespace ApiUniversity.Models;

public class Instructor
{
    public int Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public DateTime HireDate { get; set; }
    public List<Course> Courses { get; set; } = new();
    public List<Department> AdministeredDepartments { get; set; } = new();

    // Default constructor
    public Instructor() { }

    public Instructor(InstructorDTO instructorDTO)
    {
        Id = instructorDTO.Id;
        LastName = instructorDTO.LastName;
        FirstName = instructorDTO.FirstName;
        Email = instructorDTO.Email;
        HireDate = instructorDTO.HireDate;
    }
}
