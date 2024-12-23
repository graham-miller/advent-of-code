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
        var x = GetInput();

        return 1;
    }

    private static List<string> GetInput()
    {
        //return
        //[
        //    "190: 10 19",
        //    "3267: 81 40 27",
        //    "83: 17 5",
        //    "156: 15 6",
        //    "7290: 6 8 6 15",
        //    "161011: 16 10 13",
        //    "192: 17 8 14",
        //    "21037: 9 7 18 13",
        //    "292: 11 6 16 20"
        //];

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
                //results.Add(input - operand);
                //results.Add(input / operand);
            }
            else
            {
                results.AddRange(Test(input + operand, numbers.Skip(1).ToArray()));
                results.AddRange(Test(input * operand, numbers.Skip(1).ToArray()));
                //results.AddRange(Test(input - operand, numbers.Skip(1).ToArray()));
                //results.AddRange(Test(input / operand, numbers.Skip(1).ToArray()));
            }

            return results;
        }
    }
}
