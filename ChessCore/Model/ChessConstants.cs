namespace ForzaChess.Core.Model
{
  /// <summary>
  /// Defines some constants of the game of chess
  /// </summary>
  public static class ChessConstants
  {
    public const int FirstTurn = 1;

    public const int ChessboardHeight = 8;

    public const int ChessboardWidth = 8;

    public const int MaxHalfMovesBeforeStaleMate = 50;

    public static readonly string WhiteKingPosition = "e1";
    public static readonly string WhiteKingRookPosition = "a1";
    public static readonly string WhiteQueenRookPosition = "h1";
    public static readonly string BlackKingPosition = "e8";
    public static readonly string BlackKingRookPosition = "a8";
    public static readonly string BlackQueenRookPosition = "h8";
  }
}