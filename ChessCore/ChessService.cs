using System;
using System.Collections.Generic;
using ForzaChess.Core.Model;

namespace ForzaChess.Core
{
    class ChessService : IChessService
    {
        public ChessService()
        {
            _chessboard = new Chessboard();
            Turn = 0;
        }

        private Chessboard _chessboard;

        public Chessboard GetChessboardCopy()
        {
            throw new NotImplementedException();
        }

        public void MovePiece(Piece piece, Position position)
        {
            throw new NotImplementedException();
        }

        public IList<Position> GetAvailablePositions(Piece piece)
        {
            throw new NotImplementedException();
        }

        public int Turn { get; private set; }

        private bool IsStaleMate()
        {
            throw new NotImplementedException();            
        }

        private bool IsMate()
        {
            throw new NotImplementedException();            
        }
    }
}