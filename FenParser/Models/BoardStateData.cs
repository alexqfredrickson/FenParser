using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FenParser.Models
{
    /// <summary>
    /// Contains data about the board state.
    /// </summary>
    public class BoardStateData
    {
        #region Fields
        /// <summary>
        /// The color of the active player.
        /// </summary>
        public string ActivePlayerColor = String.Empty;

        /// <summary>
        /// The square which is currently available for "en passant" capture ('-' if a square is not available).  
        /// </summary>
        public string EnPassantSquare { get; set; }

        /// <summary>
        /// A list of ranks (reversed; from rank #8 to rank #1)
        /// </summary>
        public List<string[]> Ranks { get; set; }

        /// <summary>
        /// White's kingside castling availability.
        /// </summary>
        public bool WhiteCanKingsideCastle { get; set; }

        /// <summary>
        /// White's queenside castling availability.
        /// </summary>
        public bool WhiteCanQueensideCastle { get; set; }

        /// <summary>
        /// Black's kingside castling availability.
        /// </summary>
        public bool BlackCanKingsideCastle { get; set; }

        /// <summary>
        /// Black's queenside castling availability.
        /// </summary>
        public bool BlackCanQueensideCastle { get; set; }

        /// <summary>
        /// The game's halfmove counter, used to determine a draw.
        /// </summary>
        public int HalfMoveCounter { get; set; }

        /// <summary>
        /// The game's move number.
        /// </summary>
        public int FullMoveNumber { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Parses a FEN substring containing piece placement data into a matrix of ranks.
        /// </summary>
        /// <param name="piecePlacementString"></param>
        /// <returns></returns>
        private void ParseRanks(string piecePlacementString)
        {
            string[] fenRanks = piecePlacementString.Split('/');

            for (int i = 0; i < 8; i++)
            {
                string[] fenRank = Array.ConvertAll(fenRanks[i].ToCharArray(), x => x.ToString());
                List<string> newRank = new List<string>();

                for (int j = 0; j < fenRank.Length; j++)
                {
                    string currentFenSquare = fenRank[j];

                    int n;
                    if (int.TryParse(currentFenSquare, out n))
                    {
                        int fenNullSquares = int.Parse(currentFenSquare);

                        for (int k = 0; k < fenNullSquares; k++)
                        {
                            newRank.Add(" ");
                        }
                    }
                    else
                    {
                        newRank.Add(currentFenSquare);
                    }
                }

                Ranks.Add(newRank.ToArray<string>());
            }
        }

        /// <summary>
        /// Parses a FEN substring containing active player data.
        /// </summary>
        /// <param name="activeColorSubstring"></param>
        /// <returns></returns>
        private void ParseActiveColor(string activeColorSubstring)
        {
            if (activeColorSubstring.ToLower() == "b")
            {
                ActivePlayerColor = Constants.ActivePlayerColor.Black;
            }
            else if (activeColorSubstring.ToLower() == "w")
            {
                ActivePlayerColor = Constants.ActivePlayerColor.White;
            }
        }

        /// <summary>
        /// Parses a FEN substring containing black's and white's queenside/kingside castling availability.
        /// </summary>
        /// <param name="castlingAvailabilityString"></param>
        private void ParseCastlingAvailability(string castlingAvailabilityString)
        {
            BlackCanKingsideCastle = false;
            BlackCanQueensideCastle = false;
            WhiteCanKingsideCastle = false;
            WhiteCanQueensideCastle = false;

            if (castlingAvailabilityString == "-")
            {
                return; // nobody can castle
            }
            else
            {
                if (castlingAvailabilityString.Contains("K"))
                {
                    WhiteCanKingsideCastle = true;
                }

                if (castlingAvailabilityString.Contains("k"))
                {
                    BlackCanKingsideCastle = true;
                }

                if (castlingAvailabilityString.Contains("Q"))
                {
                    WhiteCanQueensideCastle = true;
                }

                if (castlingAvailabilityString.Contains("q"))
                {
                    BlackCanQueensideCastle = true;
                }
            }
        }

        /// <summary>
        /// Parses a FEN substring containing data about the potentially eligible en-passant square.
        /// </summary>
        /// <param name="enPassantSquareString"></param>
        private void ParseEnPassantSquare(string enPassantSquareString)
        {
            if (enPassantSquareString == "-")
            {
                EnPassantSquare = String.Empty;
            }
            else if (!String.IsNullOrEmpty(enPassantSquareString))
            {
                EnPassantSquare = enPassantSquareString;
            }
        }

        /// <summary>
        /// Parses a FEN substring containing data about the halfmove counter.
        /// </summary>
        /// <param name="halfmoveClockString"></param>
        private void ParseHalfMoveCounter(string halfmoveClockString)
        {
            HalfMoveCounter = int.Parse(halfmoveClockString);
        }

        /// <summary>
        /// Parses a FEN substring containing data about the move number.
        /// </summary>
        /// <param name="fullmoveNumberString"></param>
        private void ParseFullmoveNumber(string fullmoveNumberString)
        {
            FullMoveNumber = int.Parse(fullmoveNumberString);
        }

        /// <summary>
        /// Prints a graphical representation of the board state to the console.
        /// </summary>
        public void ToConsole()
        {
            Console.WriteLine("-----------------");

            for (int i = 0; i < 8; i++)
            {
                string[] currentRank = this.Ranks.ElementAt(i);

                Console.WriteLine(
                      "|" + currentRank[0] + "|" + currentRank[1] + "|" + currentRank[2] + "|" + currentRank[3]
                    + "|" + currentRank[4] + "|" + currentRank[5] + "|" + currentRank[6] + "|" + currentRank[7] + "|");
                Console.WriteLine("-----------------");
            }

            Console.WriteLine();
            Console.WriteLine("The active player is " + this.ActivePlayerColor + ".");

            if (this.WhiteCanKingsideCastle && this.WhiteCanQueensideCastle)
            {
                Console.Write("White can castle both king-side and queen-side, and ");
            }
            else if (this.WhiteCanKingsideCastle)
            {
                Console.Write("White can only castle on the king-side, and ");
            }
            else if (this.WhiteCanQueensideCastle)
            {
                Console.Write("White can only castle on the queen-side, and ");
            }
            else
            {
                Console.Write("White cannot castle, and ");
            }

            if (this.BlackCanKingsideCastle && this.BlackCanQueensideCastle)
            {
                Console.Write("Black can castle both king-side and queen-side.");
            }
            else if (this.BlackCanKingsideCastle)
            {
                Console.Write("Black can only castle on the king-side.");
            }
            else if (this.BlackCanQueensideCastle)
            {
                Console.Write("Black can only castle on the queen-side.");
            }
            else
            {
                Console.Write("Black cannot castle.");
            }

            Console.WriteLine();

            if (String.IsNullOrEmpty(this.EnPassantSquare))
            {
                Console.WriteLine("There are no squares eligible for en-passant capture.");
            }
            else
            {
                Console.WriteLine(this.EnPassantSquare + " is eligible for en-passant capture.");
            }

            if (this.HalfMoveCounter != null)
            {
                if (this.HalfMoveCounter == 1)
                {
                    Console.WriteLine("There has been 1 halfmove since the last pawn advance or capture.");
                }
                else if (this.HalfMoveCounter == 0)
                {
                    Console.WriteLine("This is the first halfmove since the last pawn advance or capture.");
                }
                else
                {
                    Console.WriteLine("There have been " + this.HalfMoveCounter + " moves since the last pawn advance or capture.");
                }
            }

            if (this.FullMoveNumber != null)
            {
                if (this.FullMoveNumber == 1)
                {
                    Console.WriteLine("This is the first move.");
                }
                else
                {
                    Console.WriteLine("This is move number " + this.FullMoveNumber + ".");
                }
            }

            Console.ReadKey();
        }

        #endregion

        #region Construtors
        public BoardStateData() { }

        public BoardStateData(string piecePlacementString, string activeColorString, string castlingAvailabilityString,
                string enPassantSquareString, string halfmoveClockString, string fullmoveNumberString)
        {
            Ranks = new List<string[]>();

            ParseRanks(piecePlacementString);
            ParseActiveColor(activeColorString);
            ParseCastlingAvailability(castlingAvailabilityString);
            ParseEnPassantSquare(enPassantSquareString);
            ParseHalfMoveCounter(halfmoveClockString);
            ParseFullmoveNumber(fullmoveNumberString);
        }

        #endregion
    }
}
