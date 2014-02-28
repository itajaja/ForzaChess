﻿using System;
using System.Collections.Generic;
using ForzaChess.Core.Model;
using ForzaChess.Core.Utils;

namespace ForzaChess.Core
{
    public class ChessService : IChessService
    {
        public ChessService()
        {
            _chessboard = Chessboard.InitialChessboard;
            Turn = 0;
        }

        private readonly Chessboard _chessboard;

        public Chessboard GetChessboardCopy()
        {
            return Cloner.DeepClone(_chessboard);
        }

        public void MovePiece(Position from, Position to)
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