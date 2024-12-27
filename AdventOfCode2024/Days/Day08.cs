namespace AdventOfCode2024.Days;

public class Day08
{
    public static int GetAnswer1()
    {
        var map = new Map(GetInput());
        var frequencies = map.GetFrequencies();
        var antinodes = new HashSet<Position>();

        foreach (var frequency in frequencies)
        {
            var antennae = map.GetAntennaeWith(frequency);

            foreach (var antenna1 in antennae)
            foreach (var antenna2 in antennae)
            {
                if (antenna1 == antenna2) continue;

                var diffX = -1 * (antenna1.X - antenna2.X);
                var diffY = -1 * (antenna1.Y - antenna2.Y);

                var antinode = new Position(antenna1.X + 2 * diffX, antenna1.Y + 2 * diffY);
                if (map.IsInBounds(antinode)) antinodes.Add(antinode);
            }
        }

        return antinodes.Count;
    }

    public static int GetAnswer2()
    {
        var map = new Map(GetInput());
        var frequencies = map.GetFrequencies();
        var antinodes = new HashSet<Position>();

        foreach (var frequency in frequencies)
        {
            var antennae = map.GetAntennaeWith(frequency);

            foreach (var antenna1 in antennae)
            foreach (var antenna2 in antennae)
            {
                if (antenna1 == antenna2) continue;

                var diffX = -1 * (antenna1.X - antenna2.X);
                var diffY = -1 * (antenna1.Y - antenna2.Y);

                var multiplier = 1;
                while (true)
                {
                    var antinode = new Position(antenna1.X + multiplier * diffX, antenna1.Y + multiplier * diffY);

                    if (!map.IsInBounds(antinode)) break;

                    antinodes.Add(antinode);
                    multiplier++;
                }
            }
        }

        return antinodes.Count;
    }

    private static List<string> GetInput()
    {
        return Inputs.ForDay(8)
            .ReadLines()
            .ToList();
    }

    private class Map
    {
        private readonly Position[][] _data;

        private readonly int _width;
        private readonly int _height;

        public Map(List<string> data)
        {
            _width = data[0].Length;
            _height = data.Count;

            _data = new Position[_width][];

            for (var x = 0; x < _width; x++)
            {
                _data[x] = new Position[_height];

                for (var y = 0; y < _height; y++)
                {
                    var value = data.ElementAt(y).ElementAt(x);

                    if (value == '.')
                        _data[x][y] = new Position(x, y);
                    else
                        _data[x][y] = new Antenna(x, y, value);
                }
            }
        }

        public List<char> GetFrequencies()
        {
            return _data
                .SelectMany(x => x)
                .Where(x => x is Antenna)
                .Cast<Antenna>()
                .Select(x => x.Frequency)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        public List<Antenna> GetAntennaeWith(char frequency)
        {
            return _data
                .SelectMany(x => x)
                .Where(x => x is Antenna)
                .Cast<Antenna>()
                .Where(x => x.Frequency == frequency)
                .ToList();
        }

        public bool IsInBounds(Position position)
        {
            return position.X >= 0 && position.X < _width && position.Y >= 0 && position.Y < _height;
        }
    }

    private record Position(int X, int Y);

    private record Antenna(int X, int Y, char Frequency) : Position(X, Y);
}
