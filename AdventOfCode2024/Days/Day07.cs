namespace AdventOfCode2024.Days;

public class Day07
{
    public static long GetAnswer1()
    {
        var result = GetInput()
            .Select(line => new Equation(line))
            .Where(MissingOperationsResolver.IsResolvable)
            .Sum(x => x.Value);

        return result;
    }

    public static long GetAnswer2()
    {
        var result = GetInput()
            .Select(line => new Equation(line))
            .Where(MissingOperationsResolver.IsResolvable)
            .Sum(x => x.Value);

        return result;
    }

    private static List<string> GetInput()
    {
        return Inputs.ForDay(7)
            .ReadLines()
            .ToList();
    }

    public class Equation
    {
        public Equation(string line)
        {
            var parts = line.Split(": ");

            Value = long.Parse(parts[0]);
            Numbers = parts[1].Split(' ').Select(long.Parse).ToArray();
        }

        public long Value { get; }

        public long[] Numbers { get; }
    }

    public static class MissingOperationsResolver
    {
        public static bool IsResolvable(Equation equation)
        {
            return IsResolvable(equation.Value, equation.Numbers);
        }

        public static bool IsResolvable(long value, long[] numbers)
        {
            var input = numbers[0];
            
            var results = Test(input,  numbers.Skip(1).ToArray());

            return results.Any(r => r == value);
        }

        private static List<long> Test(long input, long[] numbers)
        {
            var results = new List<long>();

            var operand = numbers[0];

            if (numbers.Length == 1)
            {
                results.Add(input + operand);
                results.Add(input * operand);
            }
            else
            {
                results.AddRange(Test(input + operand, numbers.Skip(1).ToArray()));
                results.AddRange(Test(input * operand, numbers.Skip(1).ToArray()));
            }

            return results;
        }
    }
}
