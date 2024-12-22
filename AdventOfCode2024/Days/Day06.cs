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
        var map = new Map(GetInput());
        var count = 0;

        for (var x = 0; x < map.Width; x++)
        for (var y = 0; y < map.Height; y++)
        {
            var obstruction = new Position(x, y);

            if (map.AddObstruction(obstruction) && map.CheckForLoop())
            {
                count++;
            }
        }

        return count;
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
        private Position _currentPosition;
        private Direction _currentDirection;
        private Position? _addedObstruction;

        public readonly int Width;
        public readonly int Height;

        public Map(List<string> data)
        {
            _data = data;
            Width = _data[0].Length;
            Height = _data.Count;
            _currentPosition = GetGuardStartingPosition();
        }

        public int TrackGuard()
        {
            ResetGuard();
            var visitedPositions = new HashSet<Position> { _currentPosition };

            while (IsInBounds(_currentPosition))
            {
                Move();
                if (IsInBounds(_currentPosition)) visitedPositions.Add(_currentPosition);
            }

            return visitedPositions.Count;
        }

        public bool CheckForLoop()
        {
            ResetGuard();
            var previousMovements = new HashSet<Movement> { new (_currentPosition, _currentDirection) };

            while (IsInBounds(_currentPosition))
            {
                Move();

                var currentMovement = new Movement(_currentPosition, _currentDirection);

                if (!previousMovements.Add(currentMovement)) return true;
            }

            return false;
        }

        public bool AddObstruction(Position position)
        {
            _addedObstruction = null;

            if (HasObstructionAt(position) || position == GetGuardStartingPosition())
                return false;

            _addedObstruction = position;
            
            return true;
        }

        private void ResetGuard()
        {
            _currentPosition = GetGuardStartingPosition();
            _currentDirection = Direction.Up;
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
            return position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;
        }

        private bool HasObstructionAt(Position position)
        {
            if (!IsInBounds(position)) return false;

            if (position == _addedObstruction) return true;

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

    private record Movement(Position Position, Direction Direction);

    private enum Direction { Up, Down, Left, Right }
}
