namespace ForzaChess.Core.Model
{
    /// <summary>
    /// Represents the base class for any piece on the chessboard
    /// </summary>
    public class Piece : ModelBase
    {
        /// <summary>
        /// Creates a new piece passing the initial position and the type
        /// </summary>
        /// <param name="x">The file</param>
        /// <param name="y">The rank</param>
        /// <param name="type">The type of piece</param>
        public Piece(int x, int y, PieceType type)
        {
            Position = new Position(x, y);
            PieceType = type;
        }

        /// <summary>
        /// The current position of the piece on the field
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// The type of piece
        /// </summary>
        public PieceType PieceType { get; set; }
    }

    public enum PieceType
    {
        Pawn,
        King,
        Queen,
        Bishop,
        Knight,
        Rook
    }
}
