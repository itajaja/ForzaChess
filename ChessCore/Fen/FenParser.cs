using System;
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
      //"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
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

    private static Chessboard GenerateBoard(string boardString)
    {
      var b = new Chessboard();
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
          throw new ChessException("Fen Parse: " + playerString + " doesn't represent the current player");
      }
    }

    private static void GeneratePlayers(string castleString, out Player white, out Player black)
    {
      white = new Player();
      black = new Player();
    }

    private static Position GenerateEnPassant(string enPassantString)
    {
      throw new NotImplementedException();
    }
  }
}
