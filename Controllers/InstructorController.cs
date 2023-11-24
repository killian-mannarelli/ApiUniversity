using ApiUniversity.Data;
using ApiUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiUniversity.Controllers;

[ApiController]
[Route("api/instructor")]
public class InstructorController : ControllerBase
{
    private readonly UniversityContext _context;

    public InstructorController(UniversityContext context)
    {
        _context = context;
    }

    // GET: api/course
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetailedInstructorDTO>>> GetInstructors()
    {
        // Get courses and related lists
        var instructors = _context.Instructors
            .Include(e => e.AdministeredDepartments)
            .Include(e => e.Courses)
            .ThenInclude(d => d.Enrollments)
            .Select(x => new DetailedInstructorDTO(x));
        return await instructors.ToListAsync();
    }

    // GET: api/course/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DetailedInstructorDTO>> GetInstructor(int id)
    {
        // Find course and related list
        // SingleAsync() throws an exception if no course is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
        var instructor = await _context.Instructors
            .Include(e => e.AdministeredDepartments)
            .Include(e => e.Courses)
            .ThenInclude(d => d.Enrollments)
            .SingleOrDefaultAsync(t => t.Id == id);

        if (instructor == null)
        {
            return NotFound();
        }

        return new DetailedInstructorDTO(instructor);
    }

    // POST: api/course
    [HttpPost]
    public async Task<ActionResult<Course>> PostInstructor(InstructorDTO instructorDTO)
    {
        Instructor instructor = new(instructorDTO);

        _context.Instructors.Add(instructor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetInstructor),
            new { id = instructor.Id },
            new DetailedInstructorDTO(instructor)
        );
    }

    // POST: api/course
    [HttpPost("{instructorId}/course")]
    public async Task<ActionResult<Course>> PostInstructorCourses(
        int instructorId,
        TaughtCourseDTO taughtCourseDTO
    )
    {
        var instructor = await _context.Instructors
            .Include(e => e.AdministeredDepartments)
            .Include(e => e.Courses)
            .ThenInclude(d => d.Enrollments)
            .SingleOrDefaultAsync(t => t.Id == instructorId);

        if (instructor == null)
        {
            return NotFound();
        }
        foreach (int i in taughtCourseDTO.CourseIds)
        {
            var course = await _context.Courses
                .Include(e => e.Department)
                .ThenInclude(d => d.Administrator)
                .SingleOrDefaultAsync(t => t.Id == i);

            if (course == null)
            {
                return NotFound();
            }
            instructor.Courses.Add(course);
        }
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetInstructor),
            new { id = instructor.Id },
            new DetailedInstructorDTO(instructor)
        );
    }

    // PUT: api/course/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCourse(int id, CourseDTO courseDTO)
    {
        if (id != courseDTO.Id)
        {
            return BadRequest();
        }

        Course course = new(courseDTO);

        _context.Entry(course).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Courses.Any(m => m.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/course/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound();
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
