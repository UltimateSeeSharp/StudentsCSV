using Spectre.Console;
using Students.Code.StudentLayer;

namespace Students.UI;

public class Program
{
    public static async Task Main(string[] args)
    {
        List<StudentDto>? students = await StudentAccessLayer.GetStudentsAsync();

        if (students is null)
            throw new Exception("CSV konnte nicht geladen werden");

        Table table = new();

        table.AddColumn("Klasse");
        table.AddColumn("Vorname");
        table.AddColumn("Nachname");

        foreach (StudentDto student in students)
        {
            table.AddRow(student.Klasse, student.Vorname, student.Nachname);
        }

        AnsiConsole.Write(table);
        Console.WriteLine();

        Dictionary<string, int> studentsInClass = await StudentAccessLayer.GetStudentsInClass();
        foreach (var classStudentPair in studentsInClass)
        {
            AnsiConsole.WriteLine($"Klasse: {classStudentPair.Key} Schüleranzahl: {classStudentPair.Value}");
        }
        Console.WriteLine();

        int classCount = await StudentAccessLayer.GetClassCountAsync();
        AnsiConsole.WriteLine($"Klassen Gesamt: {classCount}\n");

        int avgStudentsInClass = await StudentAccessLayer.GetAvgStudentsInClassAsync();
        AnsiConsole.WriteLine($"Avg. Schüler pro Klasse: {avgStudentsInClass}");
    }
}