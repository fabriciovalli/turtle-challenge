using System;

namespace Turtle.Challenge
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine();
      Console.WriteLine($" ---- Turtle Challenge ---- ");
      Console.WriteLine();

      var boardConfig = "board";
      var moves = "sequence";

      if (args.Length > 0)
      {
        boardConfig = args[0];

        if (args.Length > 1)
        {
          moves = args[1];
        }
      }

      var boardFile = BoardFile.FromFile(boardConfig);
      var sequenceFile = SequenceFile.FromFile(moves);
      var board = boardFile?.NewBoard();
      // var turtle = boardFile?.NewTurtle();
      int runNumber = 0;
      foreach (var sequence in sequenceFile.Sequences)
      {
        Console.Write($"Sequence {++runNumber}: ");
        sequence.Run(board, boardFile?.NewTurtle(), out bool exploded, out bool finished);
        if (exploded) Console.WriteLine($" boom! ");
        else if (!finished) Console.WriteLine($" turtle still in the maze! ");
        else Console.WriteLine($" turtle got out of the maze! ");
      }
    }
  }
}
