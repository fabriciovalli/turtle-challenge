using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Turtle.Challenge
{
  public class BoardFile
  {
    public Tuple<short, short> Dimension { get; set; }
    public IList<Tuple<short, short>> Bombs { get; set; }
    public Tuple<short, short> Exit { get; set; }
    public Tuple<short, short> TurtlePosition { get; set; }
    public Direction TurtleDirection { get; set; }

    public Turtle NewTurtle() => new Turtle(this.TurtlePosition, this.TurtleDirection);
    public Board NewBoard() => new Board(this.Dimension, this.Bombs, this.Exit);
    public static BoardFile FromFile(string filePath)
    {
      BoardFile boardFile = new BoardFile();
      using (StreamReader r = new StreamReader(filePath + ".json"))
      {
        string json = r.ReadToEnd();
        JObject config = JObject.Parse(json);
        boardFile.Dimension = Tuple.Create(config.SelectToken("dimensions.x").Value<short>(), config.SelectToken("dimensions.y").Value<short>());
        boardFile.Exit = Tuple.Create(config.SelectToken("exit.x").Value<short>(), config.SelectToken("exit.y").Value<short>());
        boardFile.TurtlePosition = Tuple.Create(config.SelectToken("turtle.position.x").Value<short>(), config.SelectToken("turtle.position.y").Value<short>());
        Enum.TryParse(config.SelectToken("turtle.direction").Value<string>(), out Direction turtleDirection);
        boardFile.TurtleDirection = turtleDirection;

        boardFile.Bombs = config.SelectToken("bombs").Select(b => Tuple.Create(b.SelectToken("x").Value<short>(), b.SelectToken("y").Value<short>())).ToList();
      }
      return boardFile;
    }
  }
}