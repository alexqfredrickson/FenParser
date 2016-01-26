using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FenParser
{

    /// <summary>
    /// Contains data about the board state.
    /// </summary>
    public class BoardState
    {
        public List<string[]> Ranks { get; set; } // reversed; from rank #8 to rank #1
        public string ActivePlayer { get; set; }
        public bool WhiteCanKingsideCastle { get; set; }
        public bool WhiteCanQueensideCastle { get; set; }
        public bool BlackCanKingsideCastle { get; set; }
        public bool BlackCanQueensideCastle { get; set; }
        public string EnPassantSquare { get; set; }
        public int? HalfMoveCounter { get; set; }
        public int? FullMoveNumber { get; set; }

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
            Console.WriteLine("The active player is " + this.ActivePlayer + ".");

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

        public BoardState()
        {
            Ranks = new List<string[]>();
        }

    }
}
