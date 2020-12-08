using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Day2
{
    public async Task Execute()
    {
        var lines = await File.ReadAllLinesAsync("day2.txt");

        var linePattern = new Regex(@"^(\d+)-(\d+)\s+([a-zA-Z]):\s+([a-zA-Z]+)$", RegexOptions.Compiled);

        var grep = (
                from l in lines
                where l is not null
                select linePattern.Match(l)
        );
        var rules = (
            from m in grep
            where m.Success
            let g = m.Groups
            select new Rule(int.Parse(g[1].Value), int.Parse(g[2].Value), g[3].Value[0], g[4].Value)
        //select new { Lower = g[1].Value , Upper = g[2].Value, Ch = g[3].Value[0], Pw = g[4].Value }
        );

        int count = rules.Count(r =>
        {
            bool a = r.Password[r.Lower - 1] == r.Ch;
            bool b = r.Password[r.Upper - 1] == r.Ch;

            return (a && !b) || (!a && b);
        });

        Console.WriteLine(count);
    }
}
record Rule(int Upper, int Lower, char Ch, string Password = "")
{
}