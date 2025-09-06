using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;

public static class PeopleFilter
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        if (string.IsNullOrWhiteSpace(xmlData))
            return "{\"Names\":[],\"TotalSalary\":0,\"AverageSalary\":0,\"MaxSalary\":0,\"Count\":0}";

        var doc = XDocument.Parse(xmlData);

        var people = doc.Descendants("Person")
            .Select(p =>
            {
                var name = p.Element("Name")?.Value ?? string.Empty;
                var age = (int?)p.Element("Age") ?? 0;
                var dep = p.Element("Department")?.Value ?? string.Empty;
                var salary = (decimal?)p.Element("Salary") ?? 0m;

                DateTime hireDate;
                var hireStr = p.Element("HireDate")?.Value;
                if (!DateTime.TryParse(hireStr, out hireDate))
                    hireDate = DateTime.MinValue;

                return new { Name = name, Age = age, Department = dep, Salary = salary, HireDate = hireDate };
            })
            .Where(p => p.Age > 30
                        && p.Department == "IT"
                        && p.Salary > 5000
                        && p.HireDate < new DateTime(2019, 1, 1))
            .OrderBy(p => p.Name)
            .ToList();

        var result = new
        {
            Names = people.Select(p => p.Name).ToList(),
            TotalSalary = people.Sum(p => p.Salary),
            AverageSalary = people.Count > 0 ? people.Average(p => p.Salary) : 0,
            MaxSalary = people.Count > 0 ? people.Max(p => p.Salary) : 0,
            Count = people.Count
        };

        return JsonSerializer.Serialize(result);
    }
}
