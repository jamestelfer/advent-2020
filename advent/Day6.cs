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