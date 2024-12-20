namespace AdventOfCode2024.Days;

public class Day02
{
    public static int GetAnswer1()
    {
        return GetData().Count(IsSafe);
    }

    public static int GetAnswer2()
    {
        var count = 0;
        var data = GetData();


        foreach (var levels in data)
        {
            if (IsSafe(levels))
            {
                count++;
            }
            else
            {
                for (var i = 0; i < levels.Count; i++)
                {
                    var levelsWithOneRemoved = levels.Where((_, index) => index != i).ToList();

                    if (IsSafe(levelsWithOneRemoved))
                    {
                        count++;
                        break;
                    }
                }
            }
        }

        return count;
    }

    private static bool IsSafe(List<int> values)
    {
        var position = 0;
        var direction = 0;

        while (position < values.Count - 1)
        {
            position++;

            var a = values[position-1];
            var b = values[position];

            var diff = Math.Abs(a - b);

            if (diff is < 1 or > 3) return false;

            var previousDirection = direction;
            direction = a - b < 0 ? -1 : 1;

            if (position == 1) continue;

            if (previousDirection != direction) return false;
        }

        return true;
    }

    private static List<List<int>> GetData()
    {
        return Inputs.ForDay(2)
            .ReadLines()
            .ToList()
            .Select(line => line.Split(' ').Select(int.Parse).ToList())
            .ToList();
    }
}
