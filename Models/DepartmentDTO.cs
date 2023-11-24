namespace ApiUniversity.Models;

public class DepartmentDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int AdministratorId { get; set; }

    public DepartmentDTO() { }

    public DepartmentDTO(Department department)
    {
        Id = department.Id;
        Name = department.Name;
        AdministratorId = department.Administrator.Id;
    }
}
