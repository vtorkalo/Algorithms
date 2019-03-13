using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.Core.Tests
{
    [TestClass]
    public class StandartPathGeneratorTests
    {
        StandartPathGenerator _target;
        PathComparer _pathComparer = new PathComparer();

        [TestInitialize]
        public void TestInitialize()
        {
            _target = new StandartPathGenerator();
        }

        [TestMethod]
        public void GetPossibleMovements_Case1()
        {
            var field = TestFieldData.Standart_Moves_Case1();
            var actual = _target.GetPossibleMovements(field, new Cell(6, 3));
            var expected = new List<Move> { new Move { new Cell(6, 3), new Cell(5, 2), new Cell(4, 1) }, new Move { new Cell(6, 3), new Cell(5, 4), new Cell(4, 5), new Cell(3, 4), new Cell(2, 3), new Cell(1, 4), new Cell(0, 5), new Cell(1, 6), new Cell(2, 7), new Cell(3, 6), new Cell(4, 5), new Cell(5, 6), new Cell(6, 7) }, new Move { new Cell(6, 3), new Cell(5, 4), new Cell(4, 5), new Cell(3, 6), new Cell(2, 7), new Cell(1, 6), new Cell(0, 5), new Cell(1, 4), new Cell(2, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 7) }, new Move { new Cell(6, 3), new Cell(5, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 7) } };


            AreEquivalent(actual, expected);
        }

        [TestMethod]
        public void GetPossibleMovements_Case2()
        {
            var field = TestFieldData.Standart_Moves_Case2();
            var actual = _target.GetPossibleMovements(field, new Cell(7, 2));
            var expected = new List<Move>();


            AreEquivalent(actual, expected);
        }

        [TestMethod]
        public void GetPossibleMovements_Case3()
        {
            var field = TestFieldData.Standart_Moves_Case3();
            var actual = _target.GetPossibleMovements(field, new Cell(5, 4));
            var expected = new List<Move> { new Move { new Cell(5, 4), new Cell(4, 3) }, new Move { new Cell(5, 4), new Cell(4, 5) } };

            AreEquivalent(actual, expected);
        }

        [TestMethod]
        public void GetPossibleMovements_Case4()
        {
            var field = TestFieldData.Standart_Moves_Case4();
            var actual = _target.GetPossibleMovements(field, new Cell(4, 3));
            var expected = new List<Move> { new Move { new Cell(4, 3), new Cell(3, 4), new Cell(2, 5) } };

            AreEquivalent(actual, expected);
        }

        [TestMethod]
        public void GetPossibleMovements_Case5()
        {
            var field = TestFieldData.Standart_Moves_Case5();
            var actual = _target.GetPossibleMovements(field, new Cell(7, 0));
            var expected = new List<Move> { new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) } };

            AreEquivalent(actual, expected);
        }

        [TestMethod]
        public void GetPossibleMovements_Case6()
        {
            var field = TestFieldData.Standart_Moves_CaseStandardToKingDuringMove();
            var actual = _target.GetPossibleMovements(field, new Cell(3, 4));
            var expected = new List<Move> { new Move { new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 7), new Cell(3, 6), new Cell(2, 5), new Cell(1, 4), new Cell(0, 3) } };

            AreEquivalent(actual, expected);
        }


        private void AreEquivalent(List<Move> actual, List<Move> expected)
        {
            Assert.IsTrue(actual.Intersect(expected, _pathComparer).SequenceEqual(actual, _pathComparer));
        }
    }
}
