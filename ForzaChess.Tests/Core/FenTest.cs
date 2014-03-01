using System;
using System.Linq;
using ForzaChess.Core.Fen;
using ForzaChess.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForzaChess.Tests.Core
{
    [TestClass]
    public class FenTest
    {
        [TestMethod]
        public void GenerateBoardTest()
        {
            var parser = new FenParser();
            var chess = parser.GenerateBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"); //first position
            var board = chess.GetChessboardCopy();
            var piece = board.PieceAt(0, 0);
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
            for (var x = 0; x < ChessConstants.ChessboardWidth; x++)
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
            for (var x = 0; x < ChessConstants.ChessboardWidth; x++)
            {
                piece = board.PieceAt(x, 6);
                Assert.AreEqual(piece.PieceType, PieceType.Pawn);
                Assert.AreEqual(piece.Color, ChessColor.Black);
            }
            Assert.AreEqual(board.PieceAt(0, 0).PieceType, PieceType.Rook);
            Assert.IsTrue(board.BlackPlayer.CanCastleKingSide);
            Assert.IsTrue(board.BlackPlayer.CanCastleQueenSide);
            Assert.IsTrue(board.WhitePlayer.CanCastleKingSide);
            Assert.IsTrue(board.WhitePlayer.CanCastleQueenSide);
            parser.GenerateBoard("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1"); //after 1. e4
            Assert.AreEqual(chess.Turn, 1);
            parser.GenerateBoard("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2"); //after 1. ... c5
            Assert.AreEqual(chess.Turn, 2);
            parser.GenerateBoard("rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2"); //after 2. Nf3
            Assert.AreEqual(chess.Turn, 2);
            parser.GenerateBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 0 1"); //no one can castle
            parser.GenerateBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq e4 0 1"); //en passant test
            parser.GenerateBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"); //halfmove clock test

            try
            {
                parser.GenerateBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0"); //false: missing part
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
                parser.GenerateBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w Kekq - 0 1"); //false: wrong castle notation
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
                parser.GenerateBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR g KQkq - 0 1"); //false: wrong player's turn
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
                parser.GenerateBoard("rnbqkbnr/ppdpppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"); //false: line too long
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
                parser.GenerateBoard("rnbqkbnr/pppppipp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"); //false: piece doesn't exist
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
                parser.GenerateBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQQBNR w KQkq - 0 1"); //false: no king
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
