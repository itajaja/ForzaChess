namespace ForzaChess.Core.Model
{
  /// <summary>
  /// Represents the base class for any piece on the chessboard
  /// </summary>
  public class Piece : ModelBase
  {
    /// <summary>
    /// Creates a new piece passing the initial position and the type
    /// </summary>
    /// <param name="type">The type of piece</param>
    /// <param name="color">The color of the piece</param>
    public Piece(PieceType type, ChessColor color)
    {
      PieceType = type;
      Color = color;
    }

    /// <summary>
    /// The type of piece
    /// </summary>
    public PieceType PieceType { get; set; }

    /// <summary>
    /// The color of the piece
    /// </summary>
    public ChessColor Color { get; set; }
  }

  public enum ChessColor
  {
    White,
    Black
  }

  public enum PieceType
  {
    Pawn,
    King,
    Queen,
    Bishop,
    Knight,
    Rook
  }
}
