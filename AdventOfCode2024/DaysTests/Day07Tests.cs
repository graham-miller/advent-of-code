namespace AdventOfCode2024.DaysTests;

[TestFixture]
internal class Day07Tests
{
    [Test]
    public void DegenerateCaseReturnsTrue()
    {
        AssertMissingOperationsResolverIsResolvable(true, 0);
    }

    [Test]
    public void OneValueReturnsTrue()
    {
        AssertMissingOperationsResolverIsResolvable(true, 1, 1);
    }

    [Test]
    public void OneValueReturnsFalse()
    {
        AssertMissingOperationsResolverIsResolvable(false, 1, 2);
    }

    [Test]
    public void TwoValuesAddedReturnsTrue()
    {
        AssertMissingOperationsResolverIsResolvable(true, 2, 1, 1);
    }

    [Test]
    public void TwoValuesMultipliedReturnsTrue()
    {
        AssertMissingOperationsResolverIsResolvable(true, 6, 2, 3);
    }

    private void AssertMissingOperationsResolverIsResolvable(bool expected, int testValue, params int[] numbers)
    {
        var sut = new Day07.MissingOperationsResolver();

        var actual = sut.IsResolvable(testValue, numbers);

        Assert.That(actual, Is.EqualTo(expected));
    }
}