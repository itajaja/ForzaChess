using System;
using System.Collections.Generic;
using System.Linq;

namespace ForzaChess.Core.Model
{
  /// <summary>
  /// Represents the state of the chessboard in terms of pieces
  /// </summary>
  public class Chessboard : ModelBase
  {
    private readonly IDictionary<Position, Piece> _pieces = new Dictionary<Position, Piece>();

    public IEnumerable<Piece> BlackPieces
    {
      get { return _pieces.Values.Where(p => p.Color == ChessColor.Black); }
    }

    public IEnumerable<Piece> WhitePieces
    {
      get { return _pieces.Values.Where(p => p.Color == ChessColor.White); }
    }

    /// <summary>
    /// Returns the piece at the specified position
    /// </summary>
    /// <param name="position">The specified position</param>
    /// <returns>The current piece at the specified position, if any </returns>
    public Piece PieceAt(Position position)
    {
      Piece piece;
      _pieces.TryGetValue(position, out piece);
      return piece;
    }

    /// <summary>
    /// Returns the piece at the specified position
    /// </summary>
    /// <param name="x">The rank</param>
    /// <param name="y">The file</param>
    /// <returns>The current piece at the specified position, if any </returns>
    public Piece PieceAt(int x, int y)
    {
      return PieceAt(new Position(x, y));
    }

    /// <summary>
    /// insert a piece on the chessboard
    /// </summary>
    /// <param name="position">the position of the new piece</param>
    /// <param name="piece">the piece to insert</param>
    public void InsertPiece(Position position, Piece piece)
    {
      //valid coordinate
      _pieces[position] = piece;
    }

    /// <summary>
    /// insert a piece on the chessboard
    /// </summary>
    /// <param name="x">The rank of the new position</param>
    /// <param name="y">The file of the new position</param>
    /// <param name="piece">The piece to insert</param>
    public void InsertPiece(int x, int y, Piece piece)
    {
      InsertPiece(new Position(x, y), piece);
    }

    /// <summary>
    /// Remove a piece at the specified position
    /// </summary>
    /// <param name="position">The specified position</param>
    public void RemovePiece(Position position)
    {
      _pieces.Remove(position);
    }

    /// <summary>
    /// Remove a piece at the specified position
    /// </summary>
    /// <param name="x">The rank</param>
    /// <param name="y">The file</param>
    public void RemovePiece(int x, int y)
    {
      RemovePiece(new Position(x, y));
    }

    /// <summary>
    /// Returns a chessboard at its initial position
    /// </summary>
    /// <returns></returns>
    public static Chessboard InitialChessboard
    {
      get
      {
        var board = new Chessboard();
        board.InsertPiece(0, 0, new Piece(PieceType.Rook, ChessColor.White));
        board.InsertPiece(1, 0, new Piece(PieceType.Knight, ChessColor.White));
        board.InsertPiece(2, 0, new Piece(PieceType.Bishop, ChessColor.White));
        board.InsertPiece(3, 0, new Piece(PieceType.Queen, ChessColor.White));
        board.InsertPiece(4, 0, new Piece(PieceType.King, ChessColor.White));
        board.InsertPiece(5, 0, new Piece(PieceType.Bishop, ChessColor.White));
        board.InsertPiece(6, 0, new Piece(PieceType.Knight, ChessColor.White));
        board.InsertPiece(7, 0, new Piece(PieceType.Rook, ChessColor.White));
        board.InsertPiece(0, 1, new Piece(PieceType.Pawn, ChessColor.White));
        board.InsertPiece(1, 1, new Piece(PieceType.Pawn, ChessColor.White));
        board.InsertPiece(2, 1, new Piece(PieceType.Pawn, ChessColor.White));
        board.InsertPiece(3, 1, new Piece(PieceType.Pawn, ChessColor.White));
        board.InsertPiece(4, 1, new Piece(PieceType.Pawn, ChessColor.White));
        board.InsertPiece(5, 1, new Piece(PieceType.Pawn, ChessColor.White));
        board.InsertPiece(6, 1, new Piece(PieceType.Pawn, ChessColor.White));
        board.InsertPiece(7, 1, new Piece(PieceType.Pawn, ChessColor.White));
        board.InsertPiece(0, 7, new Piece(PieceType.Rook, ChessColor.Black));
        board.InsertPiece(1, 7, new Piece(PieceType.Knight, ChessColor.Black));
        board.InsertPiece(2, 7, new Piece(PieceType.Bishop, ChessColor.Black));
        board.InsertPiece(3, 7, new Piece(PieceType.Queen, ChessColor.Black));
        board.InsertPiece(4, 7, new Piece(PieceType.King, ChessColor.Black));
        board.InsertPiece(5, 7, new Piece(PieceType.Bishop, ChessColor.Black));
        board.InsertPiece(6, 7, new Piece(PieceType.Knight, ChessColor.Black));
        board.InsertPiece(7, 7, new Piece(PieceType.Rook, ChessColor.Black));
        board.InsertPiece(0, 6, new Piece(PieceType.Pawn, ChessColor.Black));
        board.InsertPiece(1, 6, new Piece(PieceType.Pawn, ChessColor.Black));
        board.InsertPiece(2, 6, new Piece(PieceType.Pawn, ChessColor.Black));
        board.InsertPiece(3, 6, new Piece(PieceType.Pawn, ChessColor.Black));
        board.InsertPiece(4, 6, new Piece(PieceType.Pawn, ChessColor.Black));
        board.InsertPiece(5, 6, new Piece(PieceType.Pawn, ChessColor.Black));
        board.InsertPiece(6, 6, new Piece(PieceType.Pawn, ChessColor.Black));
        board.InsertPiece(7, 6, new Piece(PieceType.Pawn, ChessColor.Black));
        return board;
      }
    }

    /// <summary>
    /// Validate if the pieces on the board represent a valid chessboard
    /// </summary>
    public void ValidateBoard()
    {
      Piece whiteKing, blackKing;
      //one and only one king per player
      try
      {
        whiteKing = _pieces.Values.Single(p => p.Color == ChessColor.White && p.PieceType == PieceType.King);
      }
      catch (Exception e)
      {
        throw new ChessException("The chessboard must have a white king", e);
      }
      try
      {
        blackKing = _pieces.Values.Single(p => p.Color == ChessColor.Black && p.PieceType == PieceType.King);
      }
      catch (Exception e)
      {
        throw new ChessException("The chessboard must have a black king", e);
      }
      //no touching kings
      var blackKingPosition = _pieces.Single(p => p.Value == blackKing).Key;
      var whiteKingPosition = _pieces.Single(p => p.Value == whiteKing).Key;
      if (Position.Distance(blackKingPosition, whiteKingPosition) <= 1)
      {
        throw new ChessException("The two kings can't be close together");
      }
      //no pawns at the end of line
      if (_pieces.Any(p =>
          (p.Key.Y == ChessConstants.ChessboardHeight - 1 && p.Value.PieceType == PieceType.Pawn && p.Value.Color == ChessColor.White) ||
          (p.Key.Y == 0 && p.Value.PieceType == PieceType.Pawn && p.Value.Color == ChessColor.Black)))
        throw new ChessException("No pawn can be at the end of line");
    }
  }
}