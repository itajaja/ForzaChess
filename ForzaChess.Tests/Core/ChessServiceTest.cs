using ForzaChess.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForzaChess.Tests.Core
{
  [TestClass]
  public class ChessServiceTest
  {
    [TestMethod]
    public void Match()
    {
      var chess = new ChessService();
      //            var board = chess.GetChessboardCopy();
      //            var newPos = new Position(0, 0);
      //            var oldPos = new Position(0, 3);
      //            chess.MovePiece(newPos,oldPos);
    }
  }
}