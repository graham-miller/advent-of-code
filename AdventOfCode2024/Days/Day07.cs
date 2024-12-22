namespace AdventOfCode2024.Days;

public class Day07
{
    public static int GetAnswer1()
    {
        var x = GetInput();

        return 1;
    }

    public static int GetAnswer2()
    {
        var x = GetInput();

        return 1;
    }

    private static List<string> GetInput()
    {
        return Inputs.ForDay(7)
            .ReadLines()
            .ToList();
    }

    public class MissingOperationsResolver
    {
        public bool IsResolvable(int testValue, params int[] numbers)
        {
            if (testValue == 0 && numbers.Length == 0) return true;

            if (numbers.Length == 1) return testValue == numbers[0];

            if (testValue == numbers.Sum()) return true;

            if (testValue == numbers.m)

        }
    }
}
