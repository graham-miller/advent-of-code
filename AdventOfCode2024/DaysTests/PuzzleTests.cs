namespace AdventOfCode2024.DaysTests;

public class PuzzleTests
{
    [Test]
    public void Day01()
    {
        Assert.That(Days.Day01.GetAnswer1(), Is.EqualTo(2742123));
        Assert.That(Days.Day01.GetAnswer2(), Is.EqualTo(21328497));
    }

    [Test]
    public void Day02()
    {
        Assert.That(Days.Day02.GetAnswer1(), Is.EqualTo(559));
        Assert.That(Days.Day02.GetAnswer2(), Is.EqualTo(601));
    }

    [Test]
    public void Day03()
    {
        Assert.That(Days.Day03.GetAnswer1(), Is.EqualTo(191183308));
        Assert.That(Days.Day03.GetAnswer2(), Is.EqualTo(92082041));
    }

    [Test]
    public void Day04()
    {
        Assert.That(Days.Day04.GetAnswer1(), Is.EqualTo(2434));
        Assert.That(Days.Day04.GetAnswer2(), Is.EqualTo(1835));
    }

    [Test]
    public void Day05()
    {
        Assert.That(Days.Day05.GetAnswer1(), Is.EqualTo(5762));
        Assert.That(Days.Day05.GetAnswer2(), Is.EqualTo(4130));
    }

    [Test]
    public void Day06()
    {
        Assert.That(Days.Day06.GetAnswer1(), Is.EqualTo(4515));
        Assert.That(Days.Day06.GetAnswer2(), Is.EqualTo(1309));
    }

    [Test]
    public void Day07()
    {
        Assert.That(Days.Day07.GetAnswer1(), Is.EqualTo(2314935962622));
        Assert.That(Days.Day07.GetAnswer2(), Is.EqualTo(401477450831495));
    }

    [Test]
    public void Day08()
    {
        Assert.That(Days.Day08.GetAnswer1(), Is.EqualTo(295));
        Assert.That(Days.Day08.GetAnswer2(), Is.EqualTo(0));
    }
}