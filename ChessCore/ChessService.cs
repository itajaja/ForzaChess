using System;
using System.Collections.Generic;
using ForzaChess.Core.Model;
using ForzaChess.Core.Utils;

namespace ForzaChess.Core
{
  public class ChessService : IChessService
  {
    private readonly Chessboard _chessboard = Chessboard.InitialChessboard;
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
      _chessboard = board;
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
      get { return _chessboard; }
    }

    public MoveResult MovePiece(Position from, Position to)
    {
      //check and move the piece
      //remove with enPassant?
      //check if check, mate, draw, or other
      _enPassant = CalculateEnPassant(from,to,CurrentPlayer);
      NextTurn();
      return MoveResult.Ok;
    }

    public ChessColor CurrentPlayer
    {
      get { return _currentPlayer; }
    }

    public IList<Position> GetAvailablePositions(Piece piece)
    {
      throw new NotImplementedException();
    }

    public int Turn
    {
      get { return _turn; }
    }

    private Position? CalculateEnPassant(Position from, Position to, ChessColor player)
    {
      if (!(_chessboard.PieceAt(to).PieceType == PieceType.Pawn && Position.Distance(from,to)==2))
        return null;
      var enPassant = new Position(to.X, to.Y);
      //depending if black or white, move forward or backward
      if (player == ChessColor.White)
        enPassant.Y--;
      else
        enPassant.Y++;
      return enPassant;
    }

    private void NextTurn()
    {
      if (_currentPlayer == ChessColor.White)
      {
        _currentPlayer = ChessColor.Black;
      }
      else
      {
        _turn++;
        _currentPlayer = ChessColor.Black;        
      }
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
    Promotion
  }
}