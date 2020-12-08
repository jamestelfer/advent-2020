using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Day3
{
    public async Task Execute()
    {
        char[][] map = await Read();

        int y = 0;
        int max = map[0].Length;
        int treeCount = 0;

        foreach (char[] step in map)
        {
            if (step[y] == '#')
            {
                treeCount++;
            }

            y = (y + 3) % max;
        }

        Console.WriteLine($"Trees: {treeCount}");
    }

    private async Task<char[][]> Read()
    {
        string[] contents = await File.ReadAllLinesAsync("day3.txt");

        return (
            from l in contents
            where l is { Length: > 0 }
            select l.ToCharArray()
        ).ToArray();
    }
}