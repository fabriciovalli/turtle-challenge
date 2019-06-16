using System;

namespace Turtle.Challenge
{
  public class Turtle
  {
    private Tuple<short, short> _position;
    private Direction _direction;

    internal Turtle(Tuple<short, short> position, Direction direction)
    {
      _position = position;
      _direction = direction;
    }

    public bool Exited { get; private set; } = false;
    public bool Exploded { get; private set; } = false;

    public bool Move(Board board)
    {
      var previousPosition = _position;
      switch (_direction)
      {
        case Direction.North:
          _position = Tuple.Create(_position.Item1, (short)((_position.Item2 - 1) % board.Dimension.Item2));
          break;
        case Direction.South:
          _position = Tuple.Create(_position.Item1, (short)((_position.Item2 + 1) % board.Dimension.Item2));
          break;
        case Direction.East:
          _position = Tuple.Create((short)((_position.Item1 + 1) % board.Dimension.Item1), _position.Item2);
          break;
        case Direction.West:
          _position = Tuple.Create((short)((_position.Item1 - 1) % board.Dimension.Item1), _position.Item2);
          break;
        default:
          return false;
      }

      if (board.isOutOfBounds(_position))
      {
        _position = previousPosition;
      }
      if (board.HasBomb(_position))
      {
        this.Exploded = true;
      }
      if (board.HasExit(_position))
      {
        this.Exited = true;
      }

      return !(this.Exploded || this.Exited);
    }
    public void Rotate()
    {
      _direction = (Direction)((((byte)_direction) + 1) % 4);
    }
  }
}
