using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FenParser
{
    /// <summary>
    /// Represents each component of a FEN string.
    /// </summary>
    public class FenString
    {
        /// <summary>
        /// The actual FEN string used to construct this object.
        /// </summary>
        public string ActualFenString { get; set; }

        /// <summary>
        /// The position of the white and black pieces on the board.
        /// </summary>
        public string PiecePlacement { get; set; }

        /// <summary>
        /// The active player.
        /// </summary>
        public string ActiveColor { get; set; }

        /// <summary>
        /// The availability of castling for each player.
        /// </summary>
        public string CastlingAvailability { get; set; }

        /// <summary>
        /// The square which is currently available for "en passant" capture ('-' if a square is not available).  
        /// </summary>
        public string EnPassantSquare { get; set; }

        /// <summary>
        /// The number of half moves since the last pawn advance or piece capture (used to determine stalemate).
        /// </summary>
        public string HalfmoveClock { get; set; }

        /// <summary>
        /// The game turn (incremented after Black moves).
        /// </summary>
        public string FullmoveNumber { get; set; }

        /// <summary>
        /// The default constructor for a RawFenData object.
        /// </summary>
        public FenString()
        {
            this.ActualFenString = String.Empty;
            this.PiecePlacement = String.Empty;
            this.ActiveColor = String.Empty;
            this.CastlingAvailability = String.Empty;
            this.EnPassantSquare = String.Empty;
            this.HalfmoveClock = String.Empty;
            this.FullmoveNumber = String.Empty;
        }

        /// <summary>
        /// Parses a FEN string into a FenString object.
        /// </summary>
        public FenString(string fenString)
        {
            this.ActualFenString = fenString;

            string[] fenSubstrings = this.ActualFenString.Split(' ');

            this.PiecePlacement = fenSubstrings[0];
            this.ActiveColor = fenSubstrings[1];
            this.CastlingAvailability = fenSubstrings[2];
            this.EnPassantSquare = fenSubstrings[3];
            this.HalfmoveClock = fenSubstrings[4];
            this.FullmoveNumber = fenSubstrings[5];
        }

        /// <summary>
        /// Used to manually construct a FenString object.
        /// </summary>
        /// <param name="piecePlacement">The position of the white and black pieces on the board.</param>
        /// <param name="activeColor">The active player.</param>
        /// <param name="castlingAvailability">The availability of castling for each player.</param>
        /// <param name="enPassantSquare">The square which is currently available for "en passant" capture ('-' if a square is not available).  </param>
        /// <param name="halfmoveClock"></param>
        /// <param name="fullmoveNumber"></param>
        public FenString(string piecePlacement, string activeColor, string castlingAvailability,
            string enPassantSquare, string halfmoveClock, string fullmoveNumber)
        {
            this.PiecePlacement = piecePlacement;
            this.ActiveColor = activeColor;
            this.CastlingAvailability = castlingAvailability;
            this.EnPassantSquare = enPassantSquare;
            this.HalfmoveClock = halfmoveClock;
            this.FullmoveNumber = fullmoveNumber;
        }
    }
}
