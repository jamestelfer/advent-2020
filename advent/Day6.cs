using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Day6
{
    public void Execute()
    {
        var sum = ReadGroups()
            .Select(g => string.Join("", g))
            .Select(g => new HashSet<char>(g))
            .Select(g => g.Count)
            .Sum();

        Console.WriteLine($"Part 1 sum: {sum}");

        var sum2 = ReadGroups()
            .Select(g => new
            {
                MemberCount = g.Length,
                AnswerSummary = g.Aggregate(
                    new Dictionary<char, int>(),
                    (d, answers) =>
                    {
                        foreach (char a in answers)
                        {
                            int curr = (d.TryGetValue(a, out var s)) ? s : 0;
                            d[a] = ++s;
                        }
                        return d;
                    })
            })
            .Select(g => g.AnswerSummary.Count(p => p.Value == g.MemberCount))
            .Sum();

        Console.WriteLine($"Part 2 sum: {sum2}");
    }

    public IEnumerable<string[]> ReadGroups()
    {
        using var reader = new StreamReader("day6.txt");

        var currentGroup = new List<string>();

        string? l;
        while ((l = reader.ReadLine()) != null)
        {
            if (l.Length == 0)
            {
                if (currentGroup.Count > 0)
                {
                    yield return currentGroup.ToArray();
                }

                currentGroup.Clear();
            }
            else
            {
                currentGroup.Add(l);
            }
        }

        if (currentGroup.Count > 0)
        {
            yield return currentGroup.ToArray();
        }
    }
}