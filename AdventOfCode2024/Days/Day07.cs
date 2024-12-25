namespace AdventOfCode2024.Days;

public class Day07
{
    public static long GetAnswer1()
    {
        var result = GetInput()
            .Select(line => new Equation(line))
            .Where(equation => MissingOperationsResolver.IsResolvable(equation, false))
            .Sum(x => x.Value);

        return result;
    }

    public static long GetAnswer2()
    {
        var result = GetInput()
            .Select(line => new Equation(line))
            .Where(equation => MissingOperationsResolver.IsResolvable(equation, true))
            .Sum(x => x.Value);

        return result;
    }

    private static List<string> GetInput()
    {
        //return new List<string>
        //{
        //    "7449867267: 120 8 46 73 882 74"
        //};

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
        public static bool IsResolvable(Equation equation, bool useConcat)
        {
            return IsResolvable(equation.Value, equation.Numbers, useConcat);
        }

        public static bool IsResolvable(long value, long[] numbers, bool useConcat)
        {
            var input = numbers[0];

            var results = Test(input, numbers.Skip(1).ToArray(), useConcat);

            return results.Any(r => r == value);
        }

        private static List<long> Test(long input, long[] numbers, bool useConcat)
        {
            var results = new List<long>();

            var operand = numbers[0];

            if (numbers.Length == 1)
            {
                results.Add(input + operand);
                results.Add(input * operand);
                
                if (useConcat) results.Add(long.Parse(input.ToString() + operand));
            }
            else
            {
                results.AddRange(Test(input + operand, numbers.Skip(1).ToArray(), useConcat));
                results.AddRange(Test(input * operand, numbers.Skip(1).ToArray(), useConcat));

                if (useConcat) results.AddRange(Test(long.Parse(input.ToString() + operand), numbers.Skip(1).ToArray(), useConcat));
            }

            return results;
        }
    }
}
