using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiUniversity.Models;

namespace ApiTodo.Controllers;

[ApiController]
[Route("api/enrollment")]
public class EnrollmentController : ControllerBase
{
    private readonly UniversityContext _context;

    public EnrollmentController(UniversityContext context)
    {
        _context = context;
    }

    // GET: api/enrollment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DetailedEnrollmentDTO>> GetEnrollment(int id)
    {
        var enrollment = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .SingleOrDefaultAsync(e => e.Id == id);

        if (enrollment == null)
        {
            return NotFound();
        }

        return new DetailedEnrollmentDTO(enrollment);
    }

    [HttpPost]
    public async Task<ActionResult<DetailedEnrollmentDTO>> CreateEnrollment(
        EnrollmentDTO enrollmentDTO
    )
    {
        Enrollment enrollment = new Enrollment();
        var student = await _context.Students.SingleOrDefaultAsync(
            t => t.Id == enrollmentDTO.StudentId
        );
        var course = await _context.Courses.SingleOrDefaultAsync(
            t => t.Id == enrollmentDTO.CourseId
        );
        enrollment.Student = student!;
        enrollment.Course = course!;
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(Enrollment),
            new { id = enrollment.Id },
            new DetailedEnrollmentDTO(enrollment)
        );
    }
}
