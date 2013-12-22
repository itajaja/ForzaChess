using System;
using System.Globalization;

namespace ForzaChess.Core.Model
{
    /// <summary>
    /// Defines a position on the chessboard
    /// </summary>
    public struct Position
    {
        private int _x;
        private int _y;

        public Position(int x, int y ) : this()
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets the file of the position, a vertical column of the chessboard
        /// </summary>
        public string File { get { return (Y + 1).ToString(CultureInfo.InvariantCulture); } }

        /// <summary>
        /// Gets the rank of the position, an horizontal line of the chessboard
        /// </summary>
        public string Rank{get { return char.ConvertFromUtf32(X); }}

        /// <summary>
        /// Gets or sets the horizontal coordinate
        /// </summary>
        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                if (value >= 0 && value < ChessConstants.ChessboardWidth)
                    _x = value;
                else
                    throw new ArgumentOutOfRangeException("value");
            }
        }

        /// <summary>
        /// Gets or sets the vertical coordinate
        /// </summary>
        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (value >= 0 && value < ChessConstants.ChessboardHeight)
                    _y = value;
                else
                    throw new ArgumentOutOfRangeException("value");
            }
        }
    }
}