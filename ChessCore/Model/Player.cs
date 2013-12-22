namespace ForzaChess.Core.Model
{
    /// <summary>
    /// Represents a player of a chees game
    /// </summary>
    public class Player : ModelBase
    {
        /// <summary>
        /// Gets or sets whether the player has castled
        /// </summary>
        public bool HasCastled { get; set; }
    }
}