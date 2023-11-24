using ApiUniversity.Data;
using ApiUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiUniversity.Controllers;

[ApiController]
[Route("api/course")]
public class CourseController : ControllerBase
{
    private readonly UniversityContext _context;

    public CourseController(UniversityContext context)
    {
        _context = context;
    }

    // GET: api/course
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetailedCourseDTO>>> GetCourses()
    {
        // Get courses and related lists
        var courses = _context.Courses
            .Include(e => e.Department)
            .ThenInclude(d => d.Administrator)
            .Select(x => new DetailedCourseDTO(x));
        return await courses.ToListAsync();
    }

    // GET: api/course/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DetailedCourseDTO>> GetCourse(int id)
    {
        // Find course and related list
        // SingleAsync() throws an exception if no course is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
        var course = await _context.Courses
            .Include(e => e.Department)
            .ThenInclude(d => d.Administrator)
            .SingleOrDefaultAsync(t => t.Id == id);

        if (course == null)
        {
            return NotFound();
        }

        return new DetailedCourseDTO(course);
    }

    // POST: api/course
    [HttpPost]
    public async Task<ActionResult<Course>> PostCourse(CourseDTO courseDTO)
    {
        Course course = new(courseDTO);

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, new CourseDTO(course));
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
