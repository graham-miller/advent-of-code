namespace AdventOfCode2024.Days;

public class Day05
{
    public static int GetAnswer1()
    {
        var (pageOrderingRules, pagesToProduceCollection) = GetInput();
        var total = 0;

        foreach (var pagesToProduce in pagesToProduceCollection)
        {
            var conformsToAllRules = pageOrderingRules
                .Where(rule => 
                    pagesToProduce.Contains(rule.Earlier) &&
                    pagesToProduce.Contains(rule.Later))
                .All(pagesToProduce.ConformsTo);

            if (conformsToAllRules)
                total += pagesToProduce.GetMiddlePage();
        }

        return total;
    }

    public static int GetAnswer2()
    {
        var (pageOrderingRules, pagesToProduceCollection) = GetInput();
        var total = 0;

        foreach (var pagesToProduce in pagesToProduceCollection)
        {
            var applicableRules = pageOrderingRules
                .Where(rule =>
                    pagesToProduce.Contains(rule.Earlier) &&
                    pagesToProduce.Contains(rule.Later))
                .ToList();

            var doesNotConformsToAllRules = !applicableRules.All(pagesToProduce.ConformsTo);

            if (doesNotConformsToAllRules)
            {
                pagesToProduce.FixByApplying(applicableRules);
                total += pagesToProduce.GetMiddlePage();
            }
        }

        return total;
    }

    private static (List<PageOrderingRule>, List<PagesToProduce>) GetInput()
    {
        var lines = Inputs.ForDay(5)
            .ReadLines()
            .ToList();

        var pageOrderingRules = lines
            .Where(l => l.Contains('|'))
            .Select(l =>
                {
                    var parts = l.Split('|');
                    return new PageOrderingRule(int.Parse(parts[0]), int.Parse(parts[1]));
                }
            )
            .ToList();

        var pagesToProduceCollection = lines
            .Where(l => l.Contains(','))
            .Select(l => new PagesToProduce(l.Split(",").Select(int.Parse)))
            .ToList();

        return (pageOrderingRules, pagesToProduceCollection);
    }

    public record PageOrderingRule(int Earlier, int Later);

    public class PagesToProduce(IEnumerable<int> values) : List<int>(values)
    {
        public bool ConformsTo(PageOrderingRule pageOrderingRule)
        {
            return IndexOf(pageOrderingRule.Earlier) < IndexOf(pageOrderingRule.Later);
        }

        public int GetMiddlePage()
        {
            return this[Count / 2];
        }

        public void FixByApplying(List<PageOrderingRule> applicableRules)
        {
            while (!applicableRules.All(ConformsTo))
            {
                foreach (var rule in applicableRules)
                {
                    if (!ConformsTo(rule))
                    {
                        Remove(rule.Earlier);
                        Insert(IndexOf(rule.Later), rule.Earlier);
                    }
                }
            }
        }
    }
}
