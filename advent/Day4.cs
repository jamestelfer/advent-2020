using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Day4
{
    public async Task Execute()
    {
        //var linePattern = new Regex(@"^(\d+)-(\d+)\s+([a-zA-Z]):\s+([a-zA-Z]+)$", RegexOptions.Compiled);

        await foreach (string p in GetPassports()) {
            Console.WriteLine(p);
        }
    }

    public async IAsyncEnumerable<string> GetPassports() {

        using var r = new StreamReader("day4.txt");

        string currentPassport = "";

        string? l;
        while ((l = await r.ReadLineAsync()) != null) {
            if (l == "") {
                yield return currentPassport;

                currentPassport = "";
            } else {
                currentPassport += " " + l;
            }
        }

        if (currentPassport != "") {
            yield return currentPassport;
        }
    }
}