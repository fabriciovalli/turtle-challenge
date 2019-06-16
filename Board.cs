using System;
using System.Collections.Generic;

namespace Turtle.Challenge
{
  public class Board
  {
    private static bool[,] NewHasBombMatrix(Tuple<short, short> dimension, IList<Tuple<short, short>> bombs)
    {
      bool[,] hasBomb = new bool[dimension.Item1, dimension.Item2];
      foreach (var bomb in bombs)
      {
        hasBomb[bomb.Item1, bomb.Item2] = true;
      }
      return hasBomb;
    }

    private bool[,] _hasBomb;
    private Tuple<short, short> _exit;

    internal Board(Tuple<short, short> dimension, IList<Tuple<short, short>> bombs, Tuple<short, short> exit)
    {
      this.Dimension = dimension;
      _exit = exit;
      _hasBomb = NewHasBombMatrix(dimension, bombs);
    }

    public Tuple<short, short> Dimension { get; private set; }

    public bool HasBomb(Tuple<short, short> position) => _hasBomb[position.Item1, position.Item2];
    public bool HasExit(Tuple<short, short> position) => position.Equals(_exit);
    public bool isOutOfBounds(Tuple<short, short> position) => position.Item1 > Dimension.Item1 || position.Item1 < 0 || position.Item2 > Dimension.Item2 || position.Item2 < 0;
  }
}