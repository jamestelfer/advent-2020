using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Day4
{
    public void Execute()
    {
        //var linePattern = new Regex(@"^(\d+)-(\d+)\s+([a-zA-Z]):\s+([a-zA-Z]+)$", RegexOptions.Compiled);
        Regex linePattern = new(@"([a-z]{3}):([^ ]+)");

        var passports = GetRawPassports()
            .Select(l => linePattern.Matches(l))
            .Select(m => m.ToDictionary(i => i.Groups[1].Value, i => i.Groups[2].Value));

        var requiredFields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        var validators = Validators();

        int count = passports.Count(p => requiredFields.All(f => {
            if (!p.ContainsKey(f)) return false;
            bool v = validators[f](p[f]);
            if (!v) Console.WriteLine($"{f}:{p[f]}");
            return v;
        }));

        Console.WriteLine($"Valid passports: {count}");
    }

    private Dictionary<string, Func<string, bool>> Validators()
    {
        return new Dictionary<string, Func<string, bool>>{
            { "byr", s => int.TryParse(s, out var yr) && yr is >= 1920 and <= 2002 },
            { "iyr", s => int.TryParse(s, out var yr) && yr is >= 2010 and <= 2020 },
            { "eyr", s => int.TryParse(s, out var yr) && yr is >= 2020 and <= 2030 },
            { "hgt", s =>
                Regex.Match(s, @"^(\d{1,3})(cm|in)$") switch {
                    Match { Success: false } => false,
                    Match m when m.Groups[2].Value == "cm" && int.TryParse(m.Groups[1].Value, out var ht) && ht is >= 150 and <= 193 => true,
                    Match m when m.Groups[2].Value == "in" && int.TryParse(m.Groups[1].Value, out var ht) && ht is >= 59 and <= 76 => true,
                    _ => false
                }
            },
            { "hcl", s => Regex.IsMatch(s, @"^#[a-f0-9]{6}$") },
            { "ecl", s => Regex.IsMatch(s, @"^(?:amb|blu|brn|gry|grn|hzl|oth)$") },
            { "pid", s => Regex.IsMatch(s, @"^[0-9]{9}$") }
        };
    }
private T p<T>(T s) {Console.WriteLine(s);return s;}
    public IEnumerable<string> GetRawPassports()
    {

        using var r = new StreamReader("day4.txt");

        string currentPassport = "";

        string? l;
        while ((l = r.ReadLine()) != null)
        {
            if (l == "")
            {
                yield return currentPassport;

                currentPassport = "";
            }
            else
            {
                currentPassport += " " + l;
            }
        }

        if (currentPassport != "")
        {
            yield return currentPassport;
        }
    }
}