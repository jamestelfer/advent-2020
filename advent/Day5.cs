using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Day5
{
    public void Execute()
    {
        var passes = File.ReadAllLines("day5.txt")
            .Where(l => l.Length > 0)
            .Select(DecodePass);

        var sortedRoster = passes.OrderBy(p => p.SeatID).ToArray();
        Console.WriteLine(sortedRoster.Last());
        Console.WriteLine();

        var byRow = sortedRoster.GroupBy(p => p.Row).Select(g => new { Row = g.Key, Seats = g.OrderBy(p => p.Column).ToArray() }).OrderBy(p => p.Row).ToArray();

// foreach(var r in byRow) { Console.WriteLine(r);}
        for (int r = 1; r < byRow.Length-1; r++) {
            var row = byRow[r];

            if (row.Seats.Length < 8) {
                // Console.WriteLine(row.Row + "::" + string.Join<Pass>(',', row.Seats));

                Console.WriteLine("My seat: " + (row.Seats.Where((p, i) => p.Column != i).FirstOrDefault() ?? new Pass(row.Row, 7)) );
            }
        }

    }

    private Pass DecodePass(string s)
    {
        int row = Partition(s[..^3], 0, 127, 'F', 'B');
        int seat = Partition(s[^3..], 0, 7, 'L', 'R');

        return new Pass(row, seat);
    }

    private int Partition(string commands, int lower, int upper, char left = 'L', char right = 'R')
    {
        if (commands.Length < 1)
        {
            return -1;
        }
        char command = commands[0];
        // Console.WriteLine($"{command} {lower}-{upper} ({left}/{right})");

        if (commands.Length == 1)
        {
            return command switch
            {
                char c when c == left => lower,
                char c when c == right => upper,
                _ => throw new ArgumentException("invalid directive " + command, nameof(commands))
            };
        }
        else
        {
            int halfPartition = (upper - lower) / 2;

            return command switch
            {
                char c when c == left => Partition(commands.Substring(1), lower, lower + halfPartition, left, right),
                char c when c == right => Partition(commands.Substring(1), upper - halfPartition, upper, left, right),
                _ => throw new ArgumentException("invalid directive " + command, nameof(commands))
            };
        }
    }
}

record Pass(int Row, int Column)
{
    public int SeatID
    {
        get => (Row * 8) + Column;
    }
}