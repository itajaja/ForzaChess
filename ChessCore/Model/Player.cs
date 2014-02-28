namespace ForzaChess.Core.Model
{
    /// <summary>
    /// Represents a player of a chees game
    /// </summary>
    public class Player : ModelBase
    {
        public Player()
        {
            CanCastleKingSide = true;
            CanCastleQueenSide = true;
        }

        /// <summary>
        /// Gets or sets whether the player can castle Queenside
        /// </summary>
        public bool CanCastleQueenSide { get; set; }

        /// <summary>
        /// Gets or sets whether the player can castle Kingside
        /// </summary>
        public bool CanCastleKingSide { get; set; }
    }
}