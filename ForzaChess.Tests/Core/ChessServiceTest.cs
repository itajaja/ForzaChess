﻿using ForzaChess.Core;
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
      var chess = FenParser.GenerateMatch("rnbqkbn1/pppppppP/8/8/8/8/PPPPPPP1/RNBQKBNR w KQq - 0 1");
      Assert.AreEqual(chess.MovePiece("h7", "h8"), MoveResult.Promotion);
      Assert.AreEqual(chess.MovePiece("a2", "a3"), MoveResult.NotInTurn);
      Assert.AreEqual(chess.MovePiece("a7", "a6"), MoveResult.NotInTurn);
      Assert.AreEqual(chess.Promote(PieceType.King),MoveResult.NotPossible);
      Assert.AreEqual(chess.Promote(PieceType.Knight), MoveResult.Ok);
      Assert.AreEqual(chess.MovePiece("a7", "a6"), MoveResult.Ok);
    }

    [TestMethod]
    public void Check()
    {
      Assert.Fail();
    }

    [TestMethod]
    public void CheckMate()
    {
      Assert.Fail();
    }

    [TestMethod]
    public void Draw()
    {
      Assert.Fail();
    }

    [TestMethod]
    public void KingDiscovery()
    {
      Assert.Fail();
    }

    [TestMethod]
    public void KnightMove()
    {
      var chess = FenParser.GenerateMatch("rnbqkbnr/pppp1ppp/8/4p3/8/5N2/PPPPPPPP/RNBQKB1R w KQkq e6 0 2");
      Assert.AreEqual(chess.GetAvailablePositions("f3").Count,5);
      Assert.AreEqual(chess.MovePiece("f3", "f4"), MoveResult.NotPossible);
      Assert.AreEqual(chess.MovePiece("f3", "f6"), MoveResult.NotPossible);
      Assert.AreEqual(chess.MovePiece("f3", "h2"), MoveResult.NotPossible);
      Assert.AreEqual(chess.MovePiece("f3", "e1"), MoveResult.NotPossible);
      Assert.AreEqual(chess.MovePiece("f3", "e5"), MoveResult.Ok);
      Assert.AreEqual(chess.MovePiece("g8", "e7"), MoveResult.Ok);
    }

    [TestMethod]
    public void BishopMove()
    {
      var chess = FenParser.GenerateMatch("rnbqkbnr/pppp1pp1/7p/4p3/2B1P3/8/PPPP1PPP/RNBQK1NR w KQkq - 0 3");
      var moves = chess.GetAvailablePositions("c4");
      Assert.AreEqual(moves.Count, 9);
      Assert.IsTrue(moves.Contains("b3"));
      Assert.IsTrue(moves.Contains("d5"));
      Assert.IsTrue(moves.Contains("e6"));
      Assert.IsTrue(moves.Contains("f7"));
      Assert.IsTrue(moves.Contains("b5"));
      Assert.IsTrue(moves.Contains("a6"));
      Assert.IsTrue(moves.Contains("d3"));
      Assert.IsTrue(moves.Contains("e2"));
      Assert.IsTrue(moves.Contains("f1"));
      Assert.AreEqual(chess.MovePiece("c4", "b3"), MoveResult.Ok);
    }

    [TestMethod]
    public void QueenMove()
    {

    }

    [TestMethod]
    public void RookMove()
    {
      var chess = FenParser.GenerateMatch("rnbqkbnr/pppp1ppp/8/8/1p1R2P1/8/PPPP1PPP/RNBQKBN1 w Qkq - 0 1");
      var moves = chess.GetAvailablePositions("d4");
      Assert.AreEqual(moves.Count, 8);
      Assert.IsTrue(moves.Contains("c4"));
      Assert.IsTrue(moves.Contains("b4"));
      Assert.IsTrue(moves.Contains("e4"));
      Assert.IsTrue(moves.Contains("f4"));
      Assert.IsTrue(moves.Contains("d3"));
      Assert.IsTrue(moves.Contains("d5"));
      Assert.IsTrue(moves.Contains("d6"));
      Assert.IsTrue(moves.Contains("d7"));
      Assert.AreEqual(chess.MovePiece("d4", "d7"), MoveResult.Ok);
    }

    [TestMethod]
    public void KingMove()
    {
      Assert.Fail();
    }

    [TestMethod]
    public void Castling()
    {
      Assert.Fail();
    }
  }
}