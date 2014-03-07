using System;
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
    Chessboard Chessboard { get; }

    /// <summary>
    /// Try to move a piece. If the move is invalid returns an exception
    /// </summary>
    /// <param name="from">The position where the piece resides</param>
    /// <param name="to">The position where to move the piece</param>
    MoveResult MovePiece(Position from, Position to);

    /// <summary>
    /// Get the player that currently has to move
    /// </summary>
    ChessColor CurrentPlayer { get; }

    /// <summary>
    /// Gets a list of all the available positions for the specified piece
    /// </summary>
    /// <param name="position">the position of the specified piece</param>
    /// <returns>A list of all the legal moves</returns>
    IList<Position> GetAvailablePositions(Position position);


    /// <summary>
    /// Gets a list of all the controlled positions for the specified piece
    /// </summary>
    /// <param name="position">the position of the specified piece</param>
    /// <returns>A list of all the controlled positions</returns>
    IList<Position> GetControlledPositions(Position position);

    /// <summary>
    /// Determines if a given position is controlled by a given color
    /// </summary>
    /// <param name="position">The specified position</param>
    /// <param name="color">The specified color</param>
    /// <returns>true if the position is controlled, otherwise false</returns>
    bool IsControlled(Position position, ChessColor color);

    /// <summary>
    /// Gets a list of all the controlled positions by a given color
    /// </summary>
    /// <param name="color">The specified color</param>
    /// <returns>The controlled positions by the given color</returns>
    IList<Position> GetControlledPositions(ChessColor color);

    /// <summary>
    /// Gets the turn number
    /// </summary>
    int Turn { get; }

    /// <summary>
    /// Gets the number of moves since the last capture or the last pawn advancement
    /// </summary>
    int HalfMovesWithoutAdvance { get; }

    /// <summary>
    /// Get the White Player Information
    /// </summary>
    Player WhitePlayer { get; }

    /// <summary>
    /// Get the Black Player Information
    /// </summary>
    Player BlackPlayer { get; }

    /// <summary>
    /// Get the Player that has the specified color
    /// </summary>
    /// <param name="color">The color of the player</param>
    /// <returns>The player with the specified color</returns>
    Player GetPlayer(ChessColor color);

    /// <summary>
    /// Promote the pawn at the end of the board
    /// </summary>
    /// <param name="type">The type of piece to promote to (Queen, Knight, Bishop, Rook)</param>
    MoveResult Promote(PieceType type);

    /// <summary>
    /// Returns true if a draw can be claimed for the fifty moves rule, otherwise false
    /// </summary>
    bool IsFiftyMoveRule { get; }


    /// <summary>
    /// Returns true if a draw can be claimed for the three-fold repetition rule, otherwise false
    /// </summary>
    bool IsThreeFoldRepetition { get; }
  }
}
