using Students.Data.Model;

namespace Students.Code.StudentLayer;

public class StudentAccessLayer
{
    public static async Task<List<StudentDto>?> GetStudentsAsync()
    {
        List<Student>? students = await GetStudents();

        return students.Select(student => new StudentDto()
        {
            Klasse = student.Klasse,
            Vorname = student.Vorname,
            Nachname = student.Nachname,
            Email = $"{student.Vorname}.{student.Nachname}@tsn.at",
        }).ToList(); ;
    }

    public static async Task<int> GetClassCountAsync()
    {
        List<Student>? students = await GetStudents();

        int count = students.Select(x => x.Klasse).Distinct().Count();
        return count;
    }

    public static async Task<int> GetAvgStudentsInClassAsync()
    {
        List<Student>? students = await GetStudents();

        int avgStudentsInClass = students.Count / await GetClassCountAsync();
        return avgStudentsInClass;
    }

    public static async Task<Dictionary<string, int>> GetStudentsInClass()
    {
        List<Student>? students = await GetStudents();

        Dictionary<string, int> studentsInClass = new();
        List<string> classNames = students.Select(x => x.Klasse).Distinct().ToList();

        foreach (string className in classNames)
        {
            int studentCount = students.Where(x => x.Klasse == className).Count();
            studentsInClass[className] = studentCount;
        }

        return studentsInClass;
    }

    public static async Task<List<Student>> GetStudents()
    {
        List<Student>? students = await CsvHandler.GetData<Student>();
        if (students is null) return new();

        return students;
    }
}