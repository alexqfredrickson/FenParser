using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FenParser
{
    /// <summary>
    /// Determines board state from a given FEN string.
    /// </summary>
    public class FenParser
    {
        public FenString FenString { get; set; }

        /// <summary>
        /// Parses a FEN string and returns data related to the board state.
        /// </summary>
        /// <returns>Returns board state metadata.</returns>
        public BoardState Parse()
        {
            BoardState board = new BoardState();

            board.Ranks = this.GetPiecePlacement(this.FenString.PiecePlacement);
            board.ActivePlayer = this.GetActiveColor(this.FenString.ActiveColor);
            board.WhiteCanKingsideCastle = this.GetWhiteQueensideCastlingAvailability(this.FenString.CastlingAvailability);
            board.WhiteCanQueensideCastle = this.GetWhiteQueensideCastlingAvailability(this.FenString.CastlingAvailability);
            board.BlackCanKingsideCastle = this.GetBlackKingsideCastlingAvailability(this.FenString.CastlingAvailability);
            board.BlackCanQueensideCastle = this.GetBlackQueensideCastlingAvailability(this.FenString.CastlingAvailability);

            board.EnPassantSquare = this.GetEnPassantSquare(this.FenString.EnPassantSquare);
            board.HalfMoveCounter = this.GetHalfmoveClock(this.FenString.HalfmoveClock);
            board.FullMoveNumber = this.GetFullmoveNumber(this.FenString.FullmoveNumber);

            return board;
        }

        public List<string[]> GetPiecePlacement(string fenSubstring)
        {
            List<string[]> ranks = new List<string[]>();

            string[] fenRanks = fenSubstring.Split('/');

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

                ranks.Add(newRank.ToArray<string>());
            }

            return ranks;
        }

        public string GetActiveColor(string fenSubstring)
        {
            if (fenSubstring.ToLower().Equals("b"))
            {
                return "Black";
            }
            else if (fenSubstring.ToLower().Equals("w"))
            {
                return "White";
            }

            return String.Empty;
        }

        public bool GetWhiteKingsideCastlingAvailability(string fenSubstring)
        {
            if (fenSubstring.Contains("K"))
            {
                return true;
            }
            else if (fenSubstring.Contains("-"))
            {
                return false;
            }

            return false;
        }

        public bool GetWhiteQueensideCastlingAvailability(string fenSubstring)
        {
            if (fenSubstring.Contains("Q"))
            {
                return true;
            }
            else if (fenSubstring.Contains("-"))
            {
                return false;
            }

            return false;
        }

        public bool GetBlackKingsideCastlingAvailability(string fenSubstring)
        {
            if (fenSubstring.Contains("k"))
            {
                return true;
            }
            else if (fenSubstring.Contains("-"))
            {
                return false;
            }

            return false;
        }

        public bool GetBlackQueensideCastlingAvailability(string fenSubstring)
        {
            if (fenSubstring.Contains("q"))
            {
                return true;
            }
            else if (fenSubstring.Contains("-"))
            {
                return false;
            }

            return false;
        }

        public string GetEnPassantSquare(string fenSubstring)
        {
            if (fenSubstring.Contains("-"))
            {
                return String.Empty;
            }
            else if (!String.IsNullOrEmpty(fenSubstring))
            {
                return fenSubstring;
            }

            return String.Empty;
        }

        public int? GetHalfmoveClock(string fenSubstring)
        {
            int n;
            return int.TryParse(fenSubstring, out n) ? int.Parse(fenSubstring) : (int?)null;
        }

        public int? GetFullmoveNumber(string fenSubstring)
        {
            int n;
            return int.TryParse(fenSubstring, out n) ? int.Parse(fenSubstring) : (int?)null;
        }

        public FenParser()
        {
            FenString = new FenString();
        }

        /// <summary>
        /// Loads a FenParser object with a FEN string.
        /// </summary>
        public FenParser(FenString fen)
        {
            FenString = fen;
        }
    }

}
