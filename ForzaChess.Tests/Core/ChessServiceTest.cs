using System;
using ForzaChess.Core;
using ForzaChess.Core.Fen;
using ForzaChess.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForzaChess.Tests.Core
{
  [TestClass]
  public class ChessServiceTest
  {

    [TestMethod]
    public void PawnMove()
    {
      var chess = FenParser.GenerateMatch("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 0");
      Assert.AreEqual(chess.MovePiece("e2", "e3"), MoveResult.Ok);
      Assert.AreEqual(chess.Chessboard.PieceAt("e2"), null);
      Assert.AreEqual(chess.Chessboard.PieceAt("e3").PieceType, PieceType.Pawn);
      Assert.AreEqual(chess.MovePiece("e3", "e4"), MoveResult.NotInTurn);
      Assert.AreEqual(chess.MovePiece("e7", "e5"), MoveResult.Ok);
      Assert.AreEqual(chess.MovePiece("c2", "c3"), MoveResult.Ok);
      Assert.AreEqual(chess.MovePiece("a7", "a5"), MoveResult.Ok);
      Assert.AreEqual(chess.MovePiece("c3", "c5"),MoveResult.NotPossible);
      Assert.AreEqual(chess.MovePiece("e3", "e4"), MoveResult.Ok);
      Assert.AreEqual(chess.MovePiece("e5", "e4"), MoveResult.NotPossible);
    }

    [TestMethod]
    public void PawnCapture()
    {
      var chess = FenParser.GenerateMatch("rnbqkbnr/1ppppppp/p7/4P3/8/8/PPPP1PPP/RNBQKBNR b KQkq - 0 2");
      Assert.AreEqual(chess.MovePiece("d7", "d6"), MoveResult.Ok);
      Assert.AreEqual(chess.MovePiece("e5", "d6"), MoveResult.Ok);
      chess = FenParser.GenerateMatch("rnbqkbnr/pppppppp/6P1/8/8/8/PPPP1P1P/RNBQKBNR w KQkq - 0 1");
      Assert.IsTrue(chess.GetAvailablePositions("g6").Contains("h7"));
      Assert.IsTrue(chess.GetAvailablePositions("g6").Contains("f7"));
      Assert.AreEqual(chess.GetAvailablePositions("g6").Count,2);
      Assert.AreEqual(chess.MovePiece("g6", "h7"), MoveResult.Ok);
    }

    [TestMethod]
    public void EnPassant()
    {
      var chess = FenParser.GenerateMatch("rnbqkbnr/ppp1p1pp/8/3pPp2/8/8/PPPP1PPP/RNBQKBNR w KQkq f6 0 3");
      Assert.IsTrue(chess.GetAvailablePositions("e5").Contains("e6"));
      Assert.IsTrue(chess.GetAvailablePositions("e5").Contains("f6"));
      Assert.AreEqual(chess.GetAvailablePositions("e5").Count, 2);
      Assert.AreNotEqual(chess.Chessboard.PieceAt("f5"), null);
      Assert.AreEqual(chess.MovePiece("e5", "f6"), MoveResult.Ok);
      Assert.AreEqual(chess.Chessboard.PieceAt("f5"), null);
    }

    [TestMethod]
    public void Promotion()
    {

    }

    [TestMethod]
    public void Check()
    {

    }

    [TestMethod]
    public void CheckMate()
    {

    }

    [TestMethod]
    public void Draw()
    {

    }

    [TestMethod]
    public void KnightMove()
    {

    }

    [TestMethod]
    public void BishopMove()
    {

    }

    [TestMethod]
    public void RookMove()
    {

    }

    [TestMethod]
    public void KingMove()
    {

    }

    [TestMethod]
    public void AvailablePositions()
    {

    }
  }
}