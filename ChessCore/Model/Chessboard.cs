using System.Collections.Generic;
using System.Linq;

namespace ForzaChess.Core.Model
{
    /// <summary>
    /// Represents the state of the chessboard in terms of pieces
    /// </summary>
    public class Chessboard : ModelBase
    {
        public Player WhitePlayer { get; set; }

        public Player BlackPlayer { get; set; }

        public IEnumerable<Piece> BlackPieces { get; set; }

        public IEnumerable<Piece> WhitePieces { get; set; }

        public IEnumerable<Piece> AllPieces
        {
            get
            {
                return BlackPieces.Union(WhitePieces);
            }
        }

        /// <summary>
        /// Returns the owner of the piece
        /// </summary>
        /// <param name="piece">The specified piece</param>
        /// <returns>The owner of the current piece</returns>
        public Player GetPlayer(Piece piece)
        {
            return BlackPieces.Any(p => p.Id.Equals(piece.Id)) ? BlackPlayer : WhitePlayer;
        }

        /// <summary>
        /// Returns the piece at the specified position
        /// </summary>
        /// <param name="position">The specified position</param>
        /// <returns>The current piece at the specified position, if any </returns>
        public Piece PieceAt(Position position)
        {
            return AllPieces.SingleOrDefault(o => o.Position.Equals(position));
        }

        /// <summary>
        /// Returns a chessboard at its initial position
        /// </summary>
        /// <returns></returns>
        public static Chessboard InitialChessboard
        {
            get
            {
                var board = new Chessboard();
                board.WhitePlayer = new Player();
                board.WhitePieces = new List<Piece>{
                    new Piece(0, 0, PieceType.Rook),
                    new Piece(0, 1, PieceType.Knight),
                    new Piece(0, 2, PieceType.Bishop),
                    new Piece(0, 3, PieceType.Queen),
                    new Piece(0, 4, PieceType.King),
                    new Piece(0, 5, PieceType.Bishop),
                    new Piece(0, 6, PieceType.Knight),
                    new Piece(0, 7, PieceType.Rook),
                    new Piece(1, 0, PieceType.Pawn),
                    new Piece(1, 1, PieceType.Pawn),
                    new Piece(1, 2, PieceType.Pawn),
                    new Piece(1, 3, PieceType.Pawn),
                    new Piece(1, 4, PieceType.Pawn),
                    new Piece(1, 5, PieceType.Pawn),
                    new Piece(1, 6, PieceType.Pawn),
                    new Piece(1, 7, PieceType.Pawn),
                };
                board.BlackPlayer = new Player();
                board.WhitePieces = new List<Piece>{
                    new Piece(7, 0, PieceType.Rook),
                    new Piece(7, 1, PieceType.Knight),
                    new Piece(7, 2, PieceType.Bishop),
                    new Piece(7, 3, PieceType.Queen),
                    new Piece(7, 4, PieceType.King),
                    new Piece(7, 5, PieceType.Bishop),
                    new Piece(7, 6, PieceType.Knight),
                    new Piece(7, 7, PieceType.Rook),
                    new Piece(6, 0, PieceType.Pawn),
                    new Piece(6, 1, PieceType.Pawn),
                    new Piece(6, 2, PieceType.Pawn),
                    new Piece(6, 3, PieceType.Pawn),
                    new Piece(6, 4, PieceType.Pawn),
                    new Piece(6, 5, PieceType.Pawn),
                    new Piece(6, 6, PieceType.Pawn),
                    new Piece(6, 7, PieceType.Pawn),
                };
                return board;
            }
        }
    }
}