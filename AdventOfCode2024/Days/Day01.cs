namespace AdventOfCode2024.Days;

public class Day01
{
    public static int GetAnswer1()
    {
        var (list1, list2) = GetLists();

        var answer = list1.Order()
            .Zip(list2.Order(), (a, b) => Math.Abs(a - b))
            .Sum();

        return answer;
    }

    public static int GetAnswer2()
    {
        var (list1, list2) = GetLists();

        var answer = list2
            .Where(new HashSet<int>(list1).Contains)
            .GroupBy(x => x)
            .Sum(g => g.Key * g.Count());

        return answer;
    }

    private static (List<int>, List<int>) GetLists()
    {
        var list1 = new List<int>();
        var list2 = new List<int>();

        Inputs.ForDay(1)
            .ReadLines()
            .ToList()
            .ForEach(line =>
            {
                var split = line.Split(' ');

                list1.Add(int.Parse(split.First()));
                list2.Add(int.Parse(split.Last()));
            });

        return (list1, list2);
    }
}