using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Turtle.Challenge
{
  public class SequenceFile
  {
    public IList<Sequence> Sequences { get; set; }

    public SequenceFile()
    {
      Sequences = new List<Sequence>();
    }
    public static SequenceFile FromFile(string filePath)
    {
      var sequenceFile = new SequenceFile();
      using (StreamReader r = new StreamReader(filePath + ".json"))
      {
        string json = r.ReadToEnd();
        JObject config = JObject.Parse(json);
        var sequenceList = config.SelectToken("moves").Children().ToList();
        foreach (var sequence in sequenceList)
        {
          var moves = sequence.Select(m => Enum.Parse<Movement>((string)m)).ToList();
          sequenceFile.Sequences.Add(new Sequence(moves));
        }
      }
      return sequenceFile;
    }
  }
}
