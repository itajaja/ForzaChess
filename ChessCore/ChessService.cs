using System;
using System.Collections.Generic;
using ForzaChess.Core.Model;
using ForzaChess.Core.Utils;

namespace ForzaChess.Core
{
    public class ChessService : IChessService
    {
        private readonly Chessboard _chessboard = Chessboard.InitialChessboard;
        private readonly Player _whitePlayer = new Player();
        private readonly Player _blackPlayer = new Player();
        private int _turn = ChessConstants.FirstTurn;
        private int _halfMoves = 0;
        private Position _enPassant;
        private ChessColor _currentPlayer = ChessColor.White;

        public ChessService() { }

        public ChessService(Chessboard board, int turn, ChessColor currentPlayer, Player white, Player black, Position enPassant, int halfMoves)
        {
            board.ValidateBoard();
            if (Turn < ChessConstants.FirstTurn)
                throw new ChessException("Turn turn number must be at least " + ChessConstants.FirstTurn);
            _chessboard = board;
            _whitePlayer = white;
            _blackPlayer = black;
            _turn = turn;
            _enPassant = enPassant;
            _halfMoves = halfMoves;
            _currentPlayer = currentPlayer;
        }

        public int HalfMovesWithoutAdvance
        {
            get { return _halfMoves; }
        }

        public Player WhitePlayer
        {
            get { return Cloner.DeepClone(_whitePlayer); }
        }

        public Player BlackPlayer
        {
            get { return Cloner.DeepClone(_blackPlayer); }
        }

        public Player GetPlayer(ChessColor color)
        {
            switch (color)
            {
                case ChessColor.White:
                    return WhitePlayer;
                case ChessColor.Black:
                    return BlackPlayer;
                default:
                    throw new ArgumentOutOfRangeException("color");
            }
        }

        public Chessboard GetChessboardCopy()
        {
            return Cloner.DeepClone(_chessboard);
        }

        public void MovePiece(Position from, Position to)
        {
            throw new NotImplementedException();
        }

        public ChessColor CurrentPlayer
        {
            get { return _currentPlayer; }
        }

        public IList<Position> GetAvailablePositions(Piece piece)
        {
            throw new NotImplementedException();
        }

        public int Turn
        {
            get { return _turn; }
        }

        private bool IsDraw()
        {
            throw new NotImplementedException();
        }

        private bool IsMate()
        {
            throw new NotImplementedException();
        }
    }
}