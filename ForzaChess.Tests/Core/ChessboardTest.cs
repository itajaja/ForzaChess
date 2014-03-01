using System;
using System.Linq;
using ForzaChess.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForzaChess.Tests.Core
{
  [TestClass]
  public class ChessboardTest
  {
    [TestMethod]
    public void InitialChessboardTest()
    {
      Chessboard board = Chessboard.InitialChessboard;
      Piece piece = board.PieceAt(0, 0);
      Assert.AreEqual(board.BlackPieces.Count(), 16);
      Assert.AreEqual(board.WhitePieces.Count(), 16);
      Assert.AreEqual(piece.PieceType, PieceType.Rook);
      Assert.AreEqual(piece.Color, ChessColor.White);
      piece = board.PieceAt(1, 0);
      Assert.AreEqual(piece.PieceType, PieceType.Knight);
      Assert.AreEqual(piece.Color, ChessColor.White);
      piece = board.PieceAt(2, 0);
      Assert.AreEqual(piece.PieceType, PieceType.Bishop);
      Assert.AreEqual(piece.Color, ChessColor.White);
      piece = board.PieceAt(3, 0);
      Assert.AreEqual(piece.PieceType, PieceType.Queen);
      Assert.AreEqual(piece.Color, ChessColor.White);
      piece = board.PieceAt(4, 0);
      Assert.AreEqual(piece.PieceType, PieceType.King);
      Assert.AreEqual(piece.Color, ChessColor.White);
      piece = board.PieceAt(5, 0);
      Assert.AreEqual(piece.PieceType, PieceType.Bishop);
      Assert.AreEqual(piece.Color, ChessColor.White);
      piece = board.PieceAt(6, 0);
      Assert.AreEqual(piece.PieceType, PieceType.Knight);
      Assert.AreEqual(piece.Color, ChessColor.White);
      piece = board.PieceAt(7, 0);
      Assert.AreEqual(piece.PieceType, PieceType.Rook);
      Assert.AreEqual(piece.Color, ChessColor.White);
      for (int x = 0; x < ChessConstants.ChessboardWidth; x++)
      {
        piece = board.PieceAt(x, 1);
        Assert.AreEqual(piece.PieceType, PieceType.Pawn);
        Assert.AreEqual(piece.Color, ChessColor.White);
      }
      piece = board.PieceAt(0, 7);
      Assert.AreEqual(piece.PieceType, PieceType.Rook);
      Assert.AreEqual(piece.Color, ChessColor.Black);
      piece = board.PieceAt(1, 7);
      Assert.AreEqual(piece.PieceType, PieceType.Knight);
      Assert.AreEqual(piece.Color, ChessColor.Black);
      piece = board.PieceAt(2, 7);
      Assert.AreEqual(piece.PieceType, PieceType.Bishop);
      Assert.AreEqual(piece.Color, ChessColor.Black);
      piece = board.PieceAt(3, 7);
      Assert.AreEqual(piece.PieceType, PieceType.Queen);
      Assert.AreEqual(piece.Color, ChessColor.Black);
      piece = board.PieceAt(4, 7);
      Assert.AreEqual(piece.PieceType, PieceType.King);
      Assert.AreEqual(piece.Color, ChessColor.Black);
      piece = board.PieceAt(5, 7);
      Assert.AreEqual(piece.PieceType, PieceType.Bishop);
      Assert.AreEqual(piece.Color, ChessColor.Black);
      piece = board.PieceAt(6, 7);
      Assert.AreEqual(piece.PieceType, PieceType.Knight);
      Assert.AreEqual(piece.Color, ChessColor.Black);
      piece = board.PieceAt(7, 7);
      Assert.AreEqual(piece.PieceType, PieceType.Rook);
      Assert.AreEqual(piece.Color, ChessColor.Black);
      for (int x = 0; x < ChessConstants.ChessboardWidth; x++)
      {
        piece = board.PieceAt(x, 6);
        Assert.AreEqual(piece.PieceType, PieceType.Pawn);
        Assert.AreEqual(piece.Color, ChessColor.Black);
      }
    }


    [TestMethod]
    public void InsertPieceTest()
    {
      var board = new Chessboard();
      var p1 = new Piece(PieceType.Rook, ChessColor.White);
      var p2 = new Piece(PieceType.Rook, ChessColor.White);
      var p3 = new Piece(PieceType.Rook, ChessColor.White);
      var pos = new Position(3, 3);
      Assert.AreEqual(board.PieceAt(0, 0), null);
      board.InsertPiece(0, 0, p1);
      Assert.AreEqual(board.PieceAt(0, 0), p1);
      Assert.AreEqual(board.PieceAt(pos), null);
      board.InsertPiece(pos, p2);
      Assert.AreEqual(board.PieceAt(pos), p2);
      try
      {
        board.InsertPiece(ChessConstants.ChessboardWidth, 1, p3);
        Assert.Fail();
      }
      catch (AssertFailedException)
      {
        throw;
      }
      catch (Exception)
      {
        Assert.IsTrue(true);
      }
      try
      {
        board.InsertPiece(-1, 1, p3);
        Assert.Fail();
      }
      catch (AssertFailedException)
      {
        throw;
      }
      catch (Exception)
      {
        Assert.IsTrue(true);
      }
      try
      {
        board.InsertPiece(-1, ChessConstants.ChessboardHeight, p3);
        Assert.Fail();
      }
      catch (AssertFailedException)
      {
        throw;
      }
      catch (Exception)
      {
        Assert.IsTrue(true);
      }
    }


    [TestMethod]
    public void RemovePieceTest()
    {
      var board = new Chessboard();
      var p1 = new Piece(PieceType.Rook, ChessColor.White);
      var p2 = new Piece(PieceType.Rook, ChessColor.White);
      var pos = new Position(3, 3);
      board.InsertPiece(0, 0, p1);
      board.InsertPiece(pos, p2);
      board.RemovePiece(pos);
      board.RemovePiece(0, 0);
      Assert.AreEqual(board.PieceAt(pos), null);
      Assert.AreEqual(board.PieceAt(0, 0), null);
    }

    [TestMethod]
    public void IsValidBoardTest()
    {
      var board = new Chessboard();
      try
      {
        board.ValidateBoard();
        Assert.Fail();
      }
      catch (AssertFailedException)
      {
        throw;
      }
      catch (Exception)
      {
        Assert.IsTrue(true);
      }
      board.InsertPiece(0, 0, new Piece(PieceType.King, ChessColor.Black));
      try
      {
        board.ValidateBoard();
        Assert.Fail();
      }
      catch (AssertFailedException)
      {
        throw;
      }
      catch (Exception)
      {
        Assert.IsTrue(true);
      }
      board.InsertPiece(0, 1, new Piece(PieceType.King, ChessColor.White));
      try
      {
        board.ValidateBoard();
        Assert.Fail();
      }
      catch (AssertFailedException)
      {
        throw;
      }
      catch (Exception)
      {
        Assert.IsTrue(true);
      }
      board.RemovePiece(0, 1);
      board.InsertPiece(0, 2, new Piece(PieceType.King, ChessColor.White));
      board.ValidateBoard();
      board.InsertPiece(0, 3, new Piece(PieceType.King, ChessColor.White));
      try
      {
        board.ValidateBoard();
        Assert.Fail();
      }
      catch (AssertFailedException)
      {
        throw;
      }
      catch (Exception)
      {
        Assert.IsTrue(true);
      }
      board.RemovePiece(0, 3);
      board.InsertPiece(7, 7, new Piece(PieceType.Pawn, ChessColor.White));
      try
      {
        board.ValidateBoard();
        Assert.Fail();
      }
      catch (AssertFailedException)
      {
        throw;
      }
      catch (Exception)
      {
        Assert.IsTrue(true);
      }
      board.RemovePiece(7, 7);
      board.ValidateBoard();
      board.InsertPiece(0, 0, new Piece(PieceType.Pawn, ChessColor.Black));
      try
      {
        board.ValidateBoard();
        Assert.Fail();
      }
      catch (AssertFailedException)
      {
        throw;
      }
      catch (Exception)
      {
        Assert.IsTrue(true);
      }
    }
  }
}