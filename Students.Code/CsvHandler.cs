using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

namespace Students.Code;

public static class CsvHandler
{
    private static string _csvPath = @"C:\Users\Schneider David\Desktop\StudentsCSV\SchuelerListe.csv";

    public static async Task<List<T>?> GetData<T>()
    {
        return await Task.Factory.StartNew(() =>
        {
            var config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.ASCII };

            using StreamReader reader = new(_csvPath);
            using CsvReader csvReader = new(reader, config);

            csvReader.Read();
            csvReader.ReadHeader();

            List<T>? data = csvReader.GetRecords<T>().ToList();
            return data;
        });
    }
}