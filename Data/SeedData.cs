using ApiUniversity.Models;

namespace ApiUniversity.Data;

public static class SeedData
{
    // Test data for part 1 and 2



    // Test data for part 3
    public static void Init()
    {
        using var context = new UniversityContext();
        // Look for existing content
        if (context.Students.Any())
        {
            return; // DB already filled
        }
        // Add students
        Student carson =
            new()
            {
                FirstName = "Alexander",
                LastName = "Carson",
                Email = "alexander.carson@test.com",
                EnrollmentDate = DateTime.Parse("2016-09-01"),
            };
        Student alonso =
            new()
            {
                FirstName = "Meredith",
                LastName = "Alonso",
                Email = "meredith15@glog.com",
                EnrollmentDate = DateTime.Parse("2018-09-01"),
            };
        Student anand =
            new()
            {
                FirstName = "Arturo",
                LastName = "Anand",
                Email = "a.anand@test.com",
                EnrollmentDate = DateTime.Parse("2019-09-01"),
            };
        Student barzdukas =
            new()
            {
                FirstName = "Gytis",
                LastName = "Barzdukas",
                Email = "gytis-barzdukas@it.com",
                EnrollmentDate = DateTime.Parse("2018-09-01"),
            };
        context.Students.AddRange(carson, alonso, anand, barzdukas);
        // Add instructors
        var abercrombie = new Instructor
        {
            FirstName = "Kim",
            LastName = "Abercrombie",
            HireDate = DateTime.Parse("1995-03-11")
        };
        var fakhouri = new Instructor
        {
            FirstName = "Fadi",
            LastName = "Fakhouri",
            HireDate = DateTime.Parse("2002-07-06")
        };
        var harui = new Instructor
        {
            FirstName = "Roger",
            LastName = "Harui",
            HireDate = DateTime.Parse("1998-07-01")
        };
        // Add departments
        var mathematics = new Department { Name = "Mathematics", Administrator = fakhouri };
        var engineering = new Department { Name = "Engineering", Administrator = harui };
        var economics = new Department { Name = "Economics", Administrator = harui };
        // Add courses
        Course chemistry =
            new()
            {
                Id = 1,
                Title = "Chemistry",
                Credits = 3,
                Department = engineering,
                Instructors = new List<Instructor> { abercrombie, harui }
            };
        Course microeconomics =
            new()
            {
                Id = 2,
                Title = "Microeconomics",
                Credits = 3,
                Department = economics,
                Instructors = new List<Instructor> { harui }
            };
        Course macroeconmics =
            new()
            {
                Id = 3,
                Title = "Macroeconomics",
                Credits = 3,
                Department = economics
            };
        Course calculus =
            new()
            {
                Id = 4,
                Title = "Calculus",
                Credits = 4,
                Department = mathematics,
                Instructors = new List<Instructor> { fakhouri }
            };
        context.Courses.AddRange(chemistry, microeconomics, macroeconmics, calculus);
        // Add enrollments
        context.Enrollments.AddRange(
            new Enrollment
            {
                Student = carson,
                Course = chemistry,
                Grade = Grade.A
            },
            new Enrollment
            {
                Student = carson,
                Course = microeconomics,
                Grade = Grade.C
            },
            new Enrollment
            {
                Student = alonso,
                Course = calculus,
                Grade = Grade.B
            },
            new Enrollment { Student = anand, Course = chemistry, },
            new Enrollment
            {
                Student = anand,
                Course = microeconomics,
                Grade = Grade.B
            },
            new Enrollment
            {
                Student = barzdukas,
                Course = chemistry,
                Grade = Grade.C
            }
        );
        // Commit changes into DB
        context.SaveChanges();
    }
}
