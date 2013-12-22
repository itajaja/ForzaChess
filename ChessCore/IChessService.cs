using System.Collections.Generic;
using ForzaChess.Core.Model;

namespace ForzaChess.Core
{
    public interface IChessService
    {
        /// <summary>
        /// Get a copy of the current state of the match
        /// </summary>
        /// <returns>a copy of the chessboard state</returns>
        Chessboard GetChessboardCopy();

        /// <summary>
        /// Try to move a piece. If the move is invalid returns an exception
        /// </summary>
        /// <param name="piece">The targeted piece</param>
        /// <param name="position">The position where to move the piece</param>
        void MovePiece(Piece piece, Position position);

        /// <summary>
        /// Gets a list of all the available positions for the specified piece
        /// </summary>
        /// <param name="piece">The targeted piece</param>
        /// <returns>A list of all the legal moves</returns>
        IList<Position> GetAvailablePositions(Piece piece);

        /// <summary>
        /// Gets the turn number
        /// </summary>
        int Turn { get; }
    }
}
