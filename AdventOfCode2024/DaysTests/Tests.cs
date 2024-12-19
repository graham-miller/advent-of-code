namespace AdventOfCode2024.DaysTests;

public class Tests
{
    [Test]
    public void Day01()
    {
        Assert.That(Days.Day01.GetAnswer1(), Is.EqualTo(2742123));
        Assert.That(Days.Day01.GetAnswer2(), Is.EqualTo(21328497));
    }
}