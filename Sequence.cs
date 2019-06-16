using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Turtle.Challenge
{
  public class Sequence
  {
    private IEnumerable<Movement> _moves;

    public Sequence(IEnumerable<Movement> moves)
    {
      _moves = moves;
    }

    public static Sequence FromFile(string filePath)
    {
      var moves = new List<Movement>();
      using (StreamReader r = new StreamReader(filePath + ".json"))
      {
        string json = r.ReadToEnd();
        JObject config = JObject.Parse(json);
        moves = config.SelectToken("moves").Select(m => Enum.Parse<Movement>((string)m)).ToList();
      }
      return new Sequence(moves);
    }

    public void Run(Board board, Turtle turtle, out bool exploded, out bool exited)
    {
      exploded = false;
      exited = false;
      foreach (var move in _moves)
      {
        switch (move)
        {
          case Movement.Move:
            if (!turtle.Move(board))
            {
              exploded = turtle.Exploded;
              exited = turtle.Exited;
              return;
            }
            break;
          case Movement.Rotate:
            turtle.Rotate();
            break;
        }
      }
    }
  }
}