using ForzaChess.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForzaChess.Tests.Core
{
    [TestClass]
    class ChessServiceTest
    {
        [TestMethod]
        public void Match()
        {
            var chess = new ChessService();
            var board = chess.GetChessboardCopy();

            chess.MovePiece();
        }
    }
}
