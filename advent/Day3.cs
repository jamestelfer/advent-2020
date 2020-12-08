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

        var counts = new[]{
            new { x = 1, y = 1 },
            new { x = 1, y = 3 },
            new { x = 1, y = 5 },
            new { x = 1, y = 7 },
            new { x = 2, y = 1 }
        };

        var calculated = counts.Select(c => TreeCount(map, c.y, c.x));

        foreach (var res in calculated)
        {
            Console.WriteLine($"Trees: {res}");
        }

        long total = calculated.Aggregate(1L, (total, next) => total * next);

        Console.WriteLine(total);
    }

    private static int TreeCount(char[][] map, int yStep, int xStep)
    {
        int y = 0;
        int max = map[0].Length;
        int treeCount = 0;

        for (int x = 0; x < map.Length; x += xStep)
        {
            char[] step = map[x];

            if (step[y] == '#')
            {
                treeCount++;
            }

            y = (y + yStep) % max;
        }

        return treeCount;
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