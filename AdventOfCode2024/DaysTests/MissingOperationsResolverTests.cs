namespace AdventOfCode2024.DaysTests;

[TestFixture]
internal class MissingOperationsResolverTests
{
    [TestCase(190, new long[] { 10, 19 }, true)]
    [TestCase(3267, new long[] { 81, 40, 27 }, true)]
    [TestCase(83, new long[] { 17, 5 }, false)]
    [TestCase(156, new long[] { 15, 6 }, false)]
    [TestCase(7290, new long[] { 6, 8, 6, 15 }, false)]
    [TestCase(161011, new long[] { 16, 10, 13 }, false)]
    [TestCase(192, new long[] { 17, 8, 14 }, false)]
    [TestCase(21037, new long[] { 9, 7, 18, 13 }, false)]
    [TestCase(292, new long[] { 11, 6, 16, 20 }, true)]
    public void ExamplesFromPuzzleReturnAsExpected(long value, long[] numbers, bool expected)
    {
        var actual = Day07.MissingOperationsResolver.IsResolvable(value, numbers, false);

        Assert.That(actual, Is.EqualTo(expected));
    }
}