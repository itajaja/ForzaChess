﻿using System;
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
  }
}
