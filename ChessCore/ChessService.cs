using System;
using System.Collections.Generic;
using ForzaChess.Core.Model;
using ForzaChess.Core.Utils;

namespace ForzaChess.Core
{
  public class ChessService : IChessService
  {
    private readonly Chessboard _board = Chessboard.InitialChessboard;
    private readonly Player _whitePlayer = new Player();
    private readonly Player _blackPlayer = new Player();
    private int _turn = ChessConstants.FirstTurn;
    private int _halfMoves = 0;
    private Position? _enPassant;
    private ChessColor _currentPlayer = ChessColor.White;

    public ChessService() { }

    public ChessService(Chessboard board, int turn, ChessColor currentPlayer, Player white, Player black, Position? enPassant, int halfMoves)
    {
      board.ValidateBoard();
      if (Turn < ChessConstants.FirstTurn)
        throw new ChessException("Turn turn number must be at least " + ChessConstants.FirstTurn);
      _board = board;
      _whitePlayer = white;
      _blackPlayer = black;
      _turn = turn;
      _enPassant = enPassant;
      _halfMoves = halfMoves;
      _currentPlayer = currentPlayer;
    }

    public int HalfMovesWithoutAdvance
    {
      get { return _halfMoves; }
    }

    public Player WhitePlayer
    {
      get { return _whitePlayer; }
    }

    public Player BlackPlayer
    {
      get { return _blackPlayer; }
    }

    public Player GetPlayer(ChessColor color)
    {
      switch (color)
      {
        case ChessColor.White:
          return WhitePlayer;
        case ChessColor.Black:
          return BlackPlayer;
        default:
          throw new ArgumentOutOfRangeException("color");
      }
    }

    public void Promote(PieceType type)
    {
      throw new NotImplementedException();
    }

    public Chessboard Chessboard
    {
      get { return _board; }
    }

    public MoveResult MovePiece(Position from, Position to)
    {
      //check and move the piece
      var piece = _board.PieceAt(from);
      if (piece == null)
        return MoveResult.NotPossible;
      if (piece.Color != _currentPlayer)
        return MoveResult.NotInTurn;
      if (!GetAvailablePositions(from).Contains(to))
        return MoveResult.NotPossible;
      _board.MovePiece(from, to);
      //remove the enPassant
      if (to.Equals(_enPassant) && piece.PieceType == PieceType.Pawn)
        _board.RemovePiece(new Position(to.X, to.Y - Dir(piece.Color)));
      //check if check, mate, draw, or other
      _enPassant = CalculateEnPassant(from,to,CurrentPlayer);
      NextTurn();
      return MoveResult.Ok;
    }

    public ChessColor CurrentPlayer
    {
      get { return _currentPlayer; }
    }

    public IList<Position> GetAvailablePositions(Position position)
    {
      var piece = Chessboard.PieceAt(position);
      switch (piece.PieceType)
      {
        case PieceType.Pawn:
          return GetPawnPositions(position);
        case PieceType.King:
          break;
        case PieceType.Queen:
          break;
        case PieceType.Bishop:
          break;
        case PieceType.Knight:
          break;
        case PieceType.Rook:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      //check if move discovers king
      throw new NotImplementedException();
    }

    private IList<Position> GetPawnPositions(Position position)
    {
      var piece = Chessboard.PieceAt(position);
      IList<Position> positions = new List<Position>();
      var advance = Dir(piece.Color);
      var pos = new Position(position.X, position.Y + advance);
      if (_board.PieceAt(pos) == null)
      {
        positions.Add(pos);
        pos.Y += advance;
        if (_board.PieceAt(pos) == null &&
          ((piece.Color == ChessColor.White && position.Y == 1) || (piece.Color == ChessColor.Black && position.Y == 6)))
          positions.Add(pos);
      }
      if (Position.IsValid(position.X + 1, position.Y + advance))
      {
        var side1 = new Position(position.X + 1, position.Y + advance);
        if (side1.Equals(_enPassant) || _board.PieceAt(side1) != null && _board.PieceAt(side1).Color != piece.Color)
          positions.Add(side1);
      }
      if (Position.IsValid(position.X - 1, position.Y + advance))
      {
        var side2 = new Position(position.X - 1, position.Y + advance);
        if (side2.Equals(_enPassant) || _board.PieceAt(side2) != null && _board.PieceAt(side2).Color != piece.Color)
          positions.Add(side2);
      }
      return positions;
    }

    public int Turn
    {
      get { return _turn; }
    }

    private Position? CalculateEnPassant(Position from, Position to, ChessColor player)
    {
      if (!(_board.PieceAt(to).PieceType == PieceType.Pawn && Position.Distance(from,to)==2))
        return null;
      var enPassant = new Position(to.X, to.Y);
      //depending if black or white, move forward or backward
      enPassant.Y -= Dir(player);
      return enPassant;
    }

    private void NextTurn()
    {
      if (_currentPlayer == ChessColor.White)
        _currentPlayer = ChessColor.Black;
      else
      {
        _turn++;
        _currentPlayer = ChessColor.White;        
      }
    }

    /// <summary>
    /// returns the direction in whcih the pawns move
    /// </summary>
    /// <param name="color">the color to calculate the direction of</param>
    /// <returns>1 for white (forward) and -1 for black (backward)</returns>
    private int Dir(ChessColor color)
    {
      return color == ChessColor.White ? 1 : -1;
    }

    private bool IsDraw()
    {
      throw new NotImplementedException();
    }

    private bool IsMate()
    {
      throw new NotImplementedException();
    }
  }

  public enum MoveResult
  {
    Ok,
    Check,
    CheckMate,
    Draw,
    Promotion,
    NotPossible,
    NotInTurn
  }
}