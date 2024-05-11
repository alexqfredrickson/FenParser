using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FenParser.Models;
using FenParser.Constants;

namespace FenParser.Tests
{
    [TestClass]
    public class FenParserTests
    {
        private string TestFenString = "rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2";
        private FenParser Parser { get; set; }

        [TestMethod]
        public void PiecePlacementIsNotNull()
        {
            bool allRanksAreNotNull = true;

            string[][] ranks = Parser.BoardStateData.Ranks;

            for (int i = 0; i < ranks.Length; i++)
            {
                for (int j = 0; j < ranks[i].Length; j++)
                {
                    if (String.IsNullOrEmpty(ranks[i][j]))
                    {
                        allRanksAreNotNull = false;
                        break;
                    }
                }
            }

            Assert.IsTrue(allRanksAreNotNull);
        }

        [TestMethod]
        public void ActivePlayerIsAccurate()
        {
            string activePlayer = Parser.BoardStateData.ActivePlayerColor;
            Assert.AreEqual(activePlayer, ActivePlayerColor.White);
        }

        [TestMethod]
        public void CastlingAvailabilityIsCorrect()
        {
            bool whiteCanCastleKingside = Parser.BoardStateData.WhiteCanKingsideCastle;
            bool whiteCanCastleQueenside = Parser.BoardStateData.WhiteCanQueensideCastle;
            bool blackCanCastleKingside = Parser.BoardStateData.BlackCanKingsideCastle;
            bool blackCanCastleQueenside = Parser.BoardStateData.BlackCanQueensideCastle;

            Assert.IsTrue(whiteCanCastleKingside && whiteCanCastleQueenside && blackCanCastleKingside && blackCanCastleQueenside);
        }

        [TestMethod]
        public void EnPassantSquareIsCorrect()
        {
            Assert.IsTrue(Parser.BoardStateData.EnPassantSquare == "c6");
        }

        [TestMethod]
        public void HalfMoveCounterIsCorrect()
        {
            Assert.IsTrue(Parser.BoardStateData.HalfMoveCounter == 0);
        }

        [TestMethod]
        public void FullMoveNumberIsCorrect()
        {
            Assert.IsTrue(Parser.BoardStateData.FullMoveNumber == 2);
        }

        public FenParserTests()
        {
            Parser = new FenParser(TestFenString);
        }
    }
}
