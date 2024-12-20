using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days;

public class Day03
{
    public static int GetAnswer1()
    {
        var input =  GetInput();
        var pattern = @"(mul\(\d+,\d+\))";
        var total = 0;

        foreach (var line in input)
        {
            var matches = Regex.Matches(line, pattern);
      
            foreach (var match in matches)
            {
                var values = match
                    .ToString()!
                    .Replace("mul(", "")
                    .Replace(")", "")
                    .Split(',');

                total += int.Parse(values[0]) * int.Parse(values[1]);
            }
        }

        return total;
    }

    public static int GetAnswer2()
    {
        var input =  GetInput();
        var pattern = @"(mul\(\d+,\d+\))|(don't\(\))|(do\(\))";
        var enabled = true;
        var total = 0;

        foreach (var line in input)
        {
            var matches = Regex.Matches(line, pattern);
      
            foreach (var match in matches)
            {
                if (match.ToString() == "do()")
                {
                    enabled = true;
                    continue;
                }

                if (match.ToString() == "don't()")
                {
                    enabled = false;
                    continue;
                }

                var values = match
                    .ToString()!
                    .Replace("mul(", "")
                    .Replace(")", "")
                    .Split(',');

                if (enabled)
                    total += int.Parse(values[0]) * int.Parse(values[1]);
            }
        }

        return total;
    }

    private static List<string> GetInput()
    {
        return Inputs.ForDay(3)
            .ReadLines()
            .ToList();
    }
}
