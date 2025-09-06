using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class EmployeeFilter
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        if (employees == null)
            return "{\"Names\":[],\"TotalSalary\":0,\"AverageSalary\":0,\"MinSalary\":0,\"MaxSalary\":0,\"Count\":0}";

        var filtered = employees
            .Where(e => e.Age >= 25 && e.Age <= 40)
            .Where(e => e.Department == "IT" || e.Department == "Finance")
            .Where(e => e.Salary >= 5000 && e.Salary <= 9000)
            .Where(e => e.HireDate > new DateTime(2017, 12, 31))
            .ToList();

        var names = filtered
            .OrderByDescending(e => e.Name.Length)
            .ThenBy(e => e.Name)
            .Select(e => e.Name)
            .ToList();

        var totalSalary = filtered.Sum(e => e.Salary);
        var avgSalary = filtered.Count > 0 ? filtered.Average(e => e.Salary) : 0;
        var minSalary = filtered.Count > 0 ? filtered.Min(e => e.Salary) : 0;
        var maxSalary = filtered.Count > 0 ? filtered.Max(e => e.Salary) : 0;

        var result = new
        {
            Names = names,
            TotalSalary = totalSalary,
            AverageSalary = Math.Round(avgSalary, 2),
            MinSalary = minSalary,
            MaxSalary = maxSalary,
            Count = filtered.Count
        };

        return JsonSerializer.Serialize(result);
    }
}
