namespace ApiUniversity.Models;

// Data Transfer Object class, used to bypass navigation properties validation during API calls
public class DetailedInstructorDTO
{
    public int Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public DateTime HireDate { get; set; }
    public List<CourseDTO> Courses { get; set; } = new();
    public List<DepartmentDTO> AdministeredDepartments { get; set; } = new();

    public DetailedInstructorDTO() { }

    public DetailedInstructorDTO(Instructor instructor)
    {
        Id = instructor.Id;
        LastName = instructor.LastName;
        FirstName = instructor.FirstName;
        Email = instructor.Email;
        HireDate = instructor.HireDate;
        Courses = instructor.Courses.Select(c => new CourseDTO(c)).ToList();
        AdministeredDepartments = instructor.AdministeredDepartments
            .Select(d => new DepartmentDTO(d))
            .ToList();
    }
}
