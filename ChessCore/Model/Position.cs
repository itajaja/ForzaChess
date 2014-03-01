using System;
using System.Globalization;

namespace ForzaChess.Core.Model
{
  /// <summary>
  /// Defines a position on the chessboard
  /// </summary>
  public struct Position
  {
    private int _x;
    private int _y;

    public Position(int x, int y)
      : this()
    {
      X = x;
      Y = y;
    }

    /// <summary>
    /// Create a position from a string representing it (e.g.: a4, B2, c8...)
    /// </summary>
    /// <param name="pos">the string representing the rank and the file, case ignored</param>
    public Position(string pos)
      : this()
    {
      if (pos.Length != 2)
        throw new ChessException("string must be 2 charachters long to be a valid position");
      var lPos = pos.ToLower();
      Y = lPos[0] - 'a';
      X = lPos[1] - '1';
    }

    /// <summary>
    /// Gets the file of the position, a vertical column of the chessboard
    /// </summary>
    public string File { get { return (Y + 1).ToString(CultureInfo.InvariantCulture); } }

    /// <summary>
    /// Gets the rank of the position, an horizontal line of the chessboard
    /// </summary>
    public string Rank { get { return char.ConvertFromUtf32(X); } }

    /// <summary>
    /// Gets or sets the horizontal coordinate
    /// </summary>
    public int X
    {
      get
      {
        return _x;
      }
      set
      {
        if (value >= 0 && value < ChessConstants.ChessboardWidth)
          _x = value;
        else
          throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// Gets or sets the vertical coordinate
    /// </summary>
    public int Y
    {
      get
      {
        return _y;
      }
      set
      {
        if (value >= 0 && value < ChessConstants.ChessboardHeight)
          _y = value;
        else
          throw new ArgumentOutOfRangeException("value");
      }
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      return obj is Position && Equals((Position)obj);
    }

    public bool Equals(Position other)
    {
      return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (X * 397) ^ Y;
      }
    }

    /// <summary>
    /// Calculate the distance between two positions, including the diagonal paths
    /// </summary>
    /// <param name="p1">first position</param>
    /// <param name="p2">second position</param>
    /// <returns>the distance of the two positions</returns>
    public static int Distance(Position p1, Position p2)
    {
      return Math.Max(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
    }
  }
}