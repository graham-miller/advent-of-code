namespace AdventOfCode2024.Days;

public class Day04
{
    public static int GetAnswer1()
    {
        var wordSearch = new WordSearch(GetInput());
        var position = new Position(0, 0);
        var count = 0;

        do
        {
            do
            {
                if (IsXmas1(wordSearch, position, XDirection.None, YDirection.Up)) count++;
                if (IsXmas1(wordSearch, position, XDirection.None, YDirection.Down)) count++;

                if (IsXmas1(wordSearch, position, XDirection.Right, YDirection.Up)) count++;
                if (IsXmas1(wordSearch, position, XDirection.Right, YDirection.None)) count++;
                if (IsXmas1(wordSearch, position, XDirection.Right, YDirection.Down)) count++;

                if (IsXmas1(wordSearch, position, XDirection.Left, YDirection.Up)) count++;
                if (IsXmas1(wordSearch, position, XDirection.Left, YDirection.None)) count++;
                if (IsXmas1(wordSearch, position, XDirection.Left, YDirection.Down)) count++;

                position = position with { X = position.X + 1 };

            } while (position.X < wordSearch.Width);

            position = position with { X = 0, Y = position.Y + 1 };

        } while (position.Y < wordSearch.Height);

        return count;
    }

    public static int GetAnswer2()
    {
        var wordSearch = new WordSearch(GetInput());
        var position = new Position(0, 0);
        var count = 0;

        do
        {
            do
            {
                if (IsXmas2(wordSearch, position)) count++;

                position = position with { X = position.X + 1 };

            } while (position.X < wordSearch.Width);

            position = position with { X = 0, Y = position.Y + 1 };

        } while (position.Y < wordSearch.Height);

        return count;
    }

    private static List<string> GetInput()
    {
        return Inputs.ForDay(4)
            .ReadLines()
            .ToList();
    }

    private static bool IsXmas1(WordSearch wordSearch, Position start, XDirection xDirection, YDirection yDirection)
    {
        var position1 = start;
        var position2 = position1.Next(xDirection, yDirection);
        var position3 = position2.Next(xDirection, yDirection);
        var position4 = position3.Next(xDirection, yDirection);

        if (!wordSearch.IsInBounds(position1) ||
            !wordSearch.IsInBounds(position2) ||
            !wordSearch.IsInBounds(position3) ||
            !wordSearch.IsInBounds(position4)) return false;

        if (wordSearch.GetLetterAt(position1) != 'X') return false;
        if (wordSearch.GetLetterAt(position2) != 'M') return false;
        if (wordSearch.GetLetterAt(position3) != 'A') return false;
        if (wordSearch.GetLetterAt(position4) != 'S') return false;

        return true;
    }

    private static bool IsXmas2(WordSearch wordSearch, Position start)
    {
        if (wordSearch.GetLetterAt(start) != 'A') return false;

        var position1 = start.Next(XDirection.Left, YDirection.Up);
        var position2 = start.Next(XDirection.Right, YDirection.Up);
        var position3 = start.Next(XDirection.Left, YDirection.Down);
        var position4 = start.Next(XDirection.Right, YDirection.Down);

        if (!wordSearch.IsInBounds(position1) ||
            !wordSearch.IsInBounds(position2) ||
            !wordSearch.IsInBounds(position3) ||
            !wordSearch.IsInBounds(position4)) return false;

        var pair1 = new string([wordSearch.GetLetterAt(position1), wordSearch.GetLetterAt(position4)]);
        var pair2 = new string([wordSearch.GetLetterAt(position2), wordSearch.GetLetterAt(position3)]);

        return
            pair1.Contains('M') && pair1.Contains('S') &&
            pair2.Contains('M') && pair2.Contains('S');
    }

    private class WordSearch
    {
        private readonly List<string> _data;

        public readonly int Width;
        public readonly int Height;

        public WordSearch(List<string> data)
        {
            _data = data;
            Width = _data[0].Length;
            Height = _data.Count;
        }

        public bool IsInBounds(Position position)
        {
            return position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;
        }

        public char GetLetterAt(Position position)
        {
            return _data[position.Y][position.X];
        }
    }

    private record Position(int X, int Y)
    {
        public Position Next(XDirection xDirection, YDirection yDirection)
        {
            return this with { X = X + (int)xDirection, Y = Y + (int)yDirection };
        }
    }

    private enum XDirection {
        None = 0,
        Left = -1,
        Right = 1
    }

    private enum YDirection {
        None = 0,
        Up = -1,
        Down = 1
    }
}
