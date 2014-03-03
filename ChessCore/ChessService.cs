using System;
using System.Collections.Generic;
using System.Linq;
using ForzaChess.Core.Model;

namespace ForzaChess.Core
{
  public class ChessService : IChessService
  {
    private Chessboard _board = Chessboard.InitialChessboard;
    private readonly Player _whitePlayer = new Player();
    private readonly Player _blackPlayer = new Player();
    private int _turn = ChessConstants.FirstTurn;
    private int _halfMoves;
    private Position? _enPassant;
    private ChessColor _currentPlayer = ChessColor.White;
    private Position? _waitingPromotion;

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

    public MoveResult Promote(PieceType type)
    {
      if (!(type == PieceType.Rook || type == PieceType.Queen
        || type == PieceType.Knight || type == PieceType.Bishop))
        return MoveResult.NotPossible;
      if (_waitingPromotion == null)
        return MoveResult.NotPossible;
      _board.PieceAt(_waitingPromotion.Value).PieceType = type;
      var pos = _waitingPromotion.Value;
      _waitingPromotion = null;
      return CalculateSituation(pos, pos, _board.PieceAt(pos));
    }

    private MoveResult CalculateSituation(Position from, Position to, Piece piece)
    {
      //check if check, mate, draw, or other
      if (piece.PieceType == PieceType.Pawn
        && (to.Y == 0 && piece.Color == ChessColor.Black || to.Y == ChessConstants.ChessboardHeight - 1 && piece.Color == ChessColor.White))
      {
        _waitingPromotion = to;
        return MoveResult.Promotion;
      }
      _enPassant = CalculateEnPassant(from, to, CurrentPlayer);
      NextTurn();
      return MoveResult.Ok;
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
      if (!IsTurnToMove(piece))
        return MoveResult.NotInTurn;
      if (!GetAvailablePositions(from).Contains(to))
        return MoveResult.NotPossible;
      _board.MovePiece(from, to);
      //remove the enPassant
      if (to.Equals(_enPassant) && piece.PieceType == PieceType.Pawn)
        _board.RemovePiece(new Position(to.X, to.Y - Dir(piece.Color)));
      //check for promotion
      return CalculateSituation(from, to, piece);
    }

    private bool IsTurnToMove(Piece piece)
    {
      return piece.Color == _currentPlayer && _waitingPromotion == null;
    }

    public ChessColor CurrentPlayer
    {
      get { return _currentPlayer; }
    }

    public IList<Position> GetAvailablePositions(Position position)
    {
      var piece = _board.PieceAt(position);
      IEnumerable<Position> moves = GetControlledPositions(position);
      if (piece.PieceType == PieceType.Pawn)
        moves = GetPawnPositions(position);
      var illegalMoves = new List<Position>();
      foreach (var move in moves)
      {
        var tempBoard = new Chessboard(_board);
        _board.MovePiece(position, move);
        if(IsCheck(piece.Color))
          illegalMoves.Add(move);
        _board = tempBoard;
      }
      moves = moves.Except(illegalMoves);
      return moves.ToList();
    }

    public IList<Position> GetControlledPositions(Position position)
    {
      var piece = Chessboard.PieceAt(position);
      switch (piece.PieceType)
      {
        case PieceType.Pawn:
          return GetPawnControls(position);
        case PieceType.King:
          return GetKingPositions(position);
        case PieceType.Queen:
          return GetQueenPositions(position);
        case PieceType.Bishop:
          return GetBishopPositions(position);
        case PieceType.Knight:
          return GetKnightPositions(position);
        case PieceType.Rook:
          return GetRookPositions(position);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private IList<Position> GetPawnControls(Position position)
    {
      var piece = _board.PieceAt(position);
      var advance = Dir(piece.Color);
      IList<Position> positions = new List<Position>();
      if (Position.IsValid(position.X + 1, position.Y + advance))
        positions.Add(new Position(position.X + 1, position.Y + advance));
      if (Position.IsValid(position.X - 1, position.Y + advance))
        positions.Add(new Position(position.X - 1, position.Y + advance));
      return positions;
    }

    private IList<Position> GetRookPositions(Position position)
    {
      IList<Position> positions = new List<Position>();
      Expand(position, 1, 0, positions);
      Expand(position, -1, 0, positions);
      Expand(position, 0, -1, positions);
      Expand(position, 0, 1, positions);
      return positions;
    }

    private IList<Position> GetBishopPositions(Position position)
    {
      IList<Position> positions = new List<Position>();
      Expand(position, 1, 1, positions);
      Expand(position, -1, -1, positions);
      Expand(position, 1, -1, positions);
      Expand(position, -1, 1, positions);
      return positions;
    }

    private IList<Position> GetQueenPositions(Position position)
    {
      return GetRookPositions(position).Union(GetBishopPositions(position)).ToList();
    }

    private IList<Position> GetKingPositions(Position position)
    {
      var piece = _board.PieceAt(position);
      IList<Position> positions = new List<Position>();
      for (var x = position.X - 1; x <= position.X+1; x++)
        for (var y = position.Y - 1; y <= position.Y + 1; y++)
          if (IsAvailable(x, y, piece.Color))
            positions.Add(new Position(x, y));
      return positions;
    }

    public bool IsControlled(Position position, ChessColor color)
    {
      return GetControlledPositions(color).Contains(position);
    }

    public IList<Position> GetControlledPositions(ChessColor color)
    {
      var positions = color == ChessColor.White ? _board.WhitePositions : _board.BlackPositions;
      return positions.SelectMany(GetControlledPositions).Distinct().ToList();
    }

    private void Expand(Position position, int xAdv, int yAdv, ICollection<Position> positions)
    {
      var piece = _board.PieceAt(position);
      var x = position.X + xAdv;
      var y = position.Y + yAdv;
      while (Position.IsValid(x, y))
      {
        var pos = new Position(x, y);
        var p = _board.PieceAt(pos);
        if (p == null || p.Color != piece.Color)
          positions.Add(pos);
        if (p != null)
          break;
        x += xAdv;
        y += yAdv;
      }
    }

    private IList<Position> GetKnightPositions(Position position)
    {
      var piece = _board.PieceAt(position);
      IList<Position> positions = new List<Position>();
      var x = position.X - 1;
      var y = position.Y + 2;
      if (IsAvailable(x, y, piece.Color))
        positions.Add(new Position(x,y));
      x += 2;
      if (IsAvailable(x, y, piece.Color))
        positions.Add(new Position(x, y));
      x += 1;
      y -= 1;
      if (IsAvailable(x, y, piece.Color))
        positions.Add(new Position(x, y));
      y -= 2;
      if (IsAvailable(x, y, piece.Color))
        positions.Add(new Position(x, y));
      x -= 1;
      y -= 1;
      if (IsAvailable(x, y, piece.Color))
        positions.Add(new Position(x, y));
      x -= 2;
      if (IsAvailable(x, y, piece.Color))
        positions.Add(new Position(x, y));
      x -= 1;
      y += 1;
      if (IsAvailable(x, y, piece.Color))
        positions.Add(new Position(x, y));
      y += 2;
      if (IsAvailable(x, y, piece.Color))
        positions.Add(new Position(x, y));
      return positions;
    }

    private bool IsAvailable(int x, int y, ChessColor color)
    {
      return Position.IsValid(x,y) && (_board.PieceAt(x, y) == null || _board.PieceAt(x, y).Color != color);
    }

    private IEnumerable<Position> GetPawnPositions(Position position)
    {
      var piece = _board.PieceAt(position);
      var positions = new List<Position>();
      var advance = Dir(piece.Color);
      var pos = new Position(position.X, position.Y + advance);
      if (_board.PieceAt(pos) == null)
      {
        positions.Add(pos);
        if (Position.IsValid(pos.X, pos.Y + advance))
        {
          pos.Y += advance;
          if (_board.PieceAt(pos) == null &&
              ((piece.Color == ChessColor.White && position.Y == 1) || (piece.Color == ChessColor.Black && position.Y == 6)))
            positions.Add(pos);
        }
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
      //todo calculate halfMoves
      if (_currentPlayer == ChessColor.White)
        _currentPlayer = ChessColor.Black;
      else
      {
        _turn++;
        _currentPlayer = ChessColor.White;        
      }
    }

    private int Dir(ChessColor color)
    {
      return color == ChessColor.White ? 1 : -1;
    }

    private bool IsDraw()
    {
      throw new NotImplementedException();
    }

    private bool IsMate(ChessColor color)
    {
      var kingPos = _board.LocateKing(color);
      return IsCheck(color) && GetAvailablePositions(kingPos).Count == 0;
    }

    private bool IsCheck(ChessColor color)
    {
      var kingPos = _board.LocateKing(color);
      var opponent = color == ChessColor.White ? ChessColor.Black : ChessColor.White;
      return IsControlled(kingPos, opponent);
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