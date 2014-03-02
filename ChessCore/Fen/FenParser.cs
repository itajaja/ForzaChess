using System;
using System.Runtime.InteropServices;
using ForzaChess.Core.Model;

namespace ForzaChess.Core.Fen
{
  public class FenParser
  {
    /// <summary>
    /// Generate a ChessService Based on the fen string that is pased
    /// </summary>
    /// <param name="fen"></param>
    /// <returns></returns>
    public static ChessService GenerateMatch(string fen)
    {
      try
      {
        //"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1" example of string
        var fens = fen.Split(' ');
        var board = GenerateBoard(fens[0]);
        var currentPlayer = CurrentPlayer(fens[1]);
        Player white, black;
        GeneratePlayers(fens[2], out white, out black);
        var enPassant = GenerateEnPassant(fens[3]);
        var halfMoves = int.Parse(fens[4]);
        var turns = int.Parse(fens[5]);
        return new ChessService(board, turns, currentPlayer, white, black, enPassant, halfMoves);
      }
      catch (Exception e)
      {
        throw new Exception("Error while parsing the FEN string, inspect the inner exception for details", e);
      }
    }

    private static Chessboard GenerateBoard(string boardString)
    {
      var b = new Chessboard();
      var ranks = boardString.Split('/');
      if (ranks.Length != ChessConstants.ChessboardHeight)
        throw new ChessException("there must be " + ChessConstants.ChessboardHeight + " ranks in the chessboard representation");
      for (var l = 0; l < ranks.Length; l++)
      {
        var f = 0;
        var r = ChessConstants.ChessboardHeight - 1 - l;
        foreach (var c in ranks[l])
        {
          int n;
          if (int.TryParse(c + string.Empty, out n))
            f += n;
          else
          {
            switch (c)
            {
              case 'P':
                b.InsertPiece(f, r, new Piece(PieceType.Pawn, ChessColor.White));
                break;
              case 'N':
                b.InsertPiece(f, r, new Piece(PieceType.Knight, ChessColor.White));
                break;
              case 'B':
                b.InsertPiece(f, r, new Piece(PieceType.Bishop, ChessColor.White));
                break;
              case 'R':
                b.InsertPiece(f, r, new Piece(PieceType.Rook, ChessColor.White));
                break;
              case 'Q':
                b.InsertPiece(f, r, new Piece(PieceType.Queen, ChessColor.White));
                break;
              case 'K':
                b.InsertPiece(f, r, new Piece(PieceType.King, ChessColor.White));
                break;
              case 'p':
                b.InsertPiece(f, r, new Piece(PieceType.Pawn, ChessColor.Black));
                break;
              case 'n':
                b.InsertPiece(f, r, new Piece(PieceType.Knight, ChessColor.Black));
                break;
              case 'b':
                b.InsertPiece(f, r, new Piece(PieceType.Bishop, ChessColor.Black));
                break;
              case 'r':
                b.InsertPiece(f, r, new Piece(PieceType.Rook, ChessColor.Black));
                break;
              case 'q':
                b.InsertPiece(f, r, new Piece(PieceType.Queen, ChessColor.Black));
                break;
              case 'k':
                b.InsertPiece(f, r, new Piece(PieceType.King, ChessColor.Black));
                break;
              default:
                throw new ChessException("the character " + c + " doesn't represent any piece");
            }
            f++;
          }
        }
        if (f != ChessConstants.ChessboardWidth)
          throw new ChessException("there must be " + ChessConstants.ChessboardWidth + " files in the chessboard representation");
      }
      return b;
    }

    private static ChessColor CurrentPlayer(string playerString)
    {
      switch (playerString)
      {
        case "W":
        case "w":
          return ChessColor.White;
        case "B":
        case "b":
          return ChessColor.Black;
        default:
          throw new ChessException(playerString + " doesn't represent the current player");
      }
    }

    private static void GeneratePlayers(string castleString, out Player white, out Player black)
    {
      white = new Player { CanCastleKingSide = false, CanCastleQueenSide = false };
      black = new Player { CanCastleKingSide = false, CanCastleQueenSide = false };
      if (castleString == "-")
        return;
      foreach (var c in castleString)
      {
        switch (c)
        {
          case 'K':
            white.CanCastleKingSide = true;
            break;
          case 'Q':
            white.CanCastleQueenSide = true;
            break;
          case 'k':
            black.CanCastleKingSide = true;
            break;
          case 'q':
            black.CanCastleQueenSide = true;
            break;
          default:
            throw new ChessException("The character '" + c + "' doesn't represent a castling option (k,K,q,Q)");
        }
      }
    }

    private static Position? GenerateEnPassant(string enPassantString)
    {
      if (enPassantString == "-")
        return null;
      return new Position(enPassantString);
    }
  }
}
