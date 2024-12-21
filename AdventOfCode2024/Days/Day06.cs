namespace AdventOfCode2024.Days;

public class Day06
{
    public static int GetAnswer1()
    {
        var map = new Map(GetInput());

        return map.TrackGuard();
    }

    public static int GetAnswer2()
    {
        return 1;
    }

    private static List<string> GetInput()
    {
        return Inputs.ForDay(6)
            .ReadLines()
            .ToList();
    }

    private class Map
    {
        private readonly List<string> _data;
        private readonly int _width;
        private readonly int _height;
        private readonly HashSet<Position> _visitedPositions = [];
        private Position _currentPosition;
        private Direction _currentDirection = Direction.Up;


        public Map(List<string> data)
        {
            _data = data;
            _width = _data[0].Length;
            _height = _data.Count;
            _currentPosition = GetGuardStartingPosition();
            _visitedPositions.Add(_currentPosition);
        }

        public int TrackGuard()
        {
            while (IsInBounds(_currentPosition))
            {
                Move();
                if (IsInBounds(_currentPosition)) _visitedPositions.Add(_currentPosition);
            }

            return _visitedPositions.Count;
        }

        private void Move()
        {
            while (HasObstructionAt(_currentPosition.Move(_currentDirection)))
            {
                _currentDirection = _currentDirection switch
                {
                    Direction.Up => Direction.Right,
                    Direction.Down => Direction.Left,
                    Direction.Left => Direction.Up,
                    Direction.Right => Direction.Down,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            _currentPosition = _currentPosition.Move(_currentDirection);
        }

        private bool IsInBounds(Position position)
        {
            return position.X >= 0 && position.X < _width && position.Y >= 0 && position.Y < _height;
        }

        private bool HasObstructionAt(Position position)
        {
            if (!IsInBounds(position)) return false;

            return _data[position.Y][position.X] == '#';
        }

        private Position GetGuardStartingPosition()
        {
            return new Position(
                _data.Single(x => x.Contains('^')).IndexOf('^'),
                _data.IndexOf(_data.Single(x => x.Contains('^')))
            );
        }
    }

    private record Position(int X, int Y)
    {
        public Position Move(Direction direction)
        {
            return direction switch
            {
                Direction.Up => this with { X = X, Y = Y - 1 },
                Direction.Down => this with { X = X, Y = Y + 1 },
                Direction.Left => this with { X = X - 1, Y = Y },
                Direction.Right => this with { X = X + 1, Y = Y },
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    private enum Direction { Up, Down, Left, Right }
}
