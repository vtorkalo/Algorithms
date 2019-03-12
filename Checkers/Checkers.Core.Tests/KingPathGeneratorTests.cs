using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.Core.Tests
{
    [TestClass]
    public class KingPathGeneratorTests
    {
        CellComparer _cellComparer = new CellComparer();
        PathComparer _pathComparer = new PathComparer();
        KingPathGenerator _target;

        [TestInitialize]
        public void TestInitialize()
        {
            _target = new KingPathGenerator();
        }





        [TestMethod]
        public void GetPossibleKingMovements_Case1()
        {
            var field = TestFieldData.King_Moves_Case1();
            var actual = _target.GetPossibleMovements(field, new Cell (3, 4));
            var expected = new List<Move> { new Move { new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) } };

            Assert.IsTrue(actual.SequenceEqual(expected, _pathComparer));
        }

        [TestMethod]
        public void GetPossibleKingMovements_Case2()
        {
            var field = TestFieldData.King_Moves_Case2();
            var actual = _target.GetPossibleMovements(field, new Cell(3, 4));
            var expected = new List<Move> { new Move { new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) } };

            Assert.IsTrue(actual.SequenceEqual(expected, _pathComparer));
        }

        [TestMethod]
        public void GetPossibleKingMovements_Case3()
        {
            var field = TestFieldData.King_Moves_Case3();
            var actual = _target.GetPossibleMovements(field, new Cell(3, 4));
            var expected = new List<Move> { new Move { new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2) }, new Move { new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(0, 3) }, new Move { new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(6, 1) }, new Move { new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(6, 1), new Cell(7, 0) } };

            Assert.IsTrue(actual.SequenceEqual(expected, _pathComparer));
        }

        [TestMethod]
        public void GetPossibleKingMovements_Case4()
        {
            var field = TestFieldData.King_Moves_Case4();
            var actual = _target.GetPossibleMovements(field, new Cell(7, 0));
            var expected = new List<Move> { new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(5, 4), new Cell(6, 5) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(5, 4), new Cell(6, 5), new Cell(7, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 2), new Cell(2, 1) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 2), new Cell(2, 1), new Cell(1, 0) } };

            Assert.IsTrue(actual.SequenceEqual(expected, _pathComparer));
        }

        [TestMethod]
        public void GetPossibleKingMovements_Case5()
        {
            var field = TestFieldData.King_Moves_Case5();
            var actual = _target.GetPossibleMovements(field, new Cell(4, 3));
            var expected = new List<Move> { new Move { new Cell(4, 3), new Cell(3, 4) }, new Move { new Cell(4, 3), new Cell(3, 4), new Cell(2, 5) }, new Move { new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(4, 3), new Cell(5, 4) }, new Move { new Cell(4, 3), new Cell(5, 4), new Cell(6, 5) }, new Move { new Cell(4, 3), new Cell(5, 4), new Cell(6, 5), new Cell(7, 6) }, new Move { new Cell(4, 3), new Cell(5, 2) }, new Move { new Cell(4, 3), new Cell(5, 2), new Cell(6, 1) }, new Move { new Cell(4, 3), new Cell(5, 2), new Cell(6, 1), new Cell(7, 0) }, new Move { new Cell(4, 3), new Cell(3, 2) }, new Move { new Cell(4, 3), new Cell(3, 2), new Cell(2, 1) }, new Move { new Cell(4, 3), new Cell(3, 2), new Cell(2, 1), new Cell(1, 0) } };

            Assert.IsTrue(actual.SequenceEqual(expected, _pathComparer));
        }

        [TestMethod]
        public void GetPossibleKingMovements_Case6()
        {
            var field = TestFieldData.King_Moves_Case6();
            var actual = _target.GetPossibleMovements(field, new Cell(3, 4));
            var expected = new List<Move> { new Move { new Cell(3, 4), new Cell(4, 5), new Cell(5, 6) }, new Move { new Cell(3, 4), new Cell(2, 3), new Cell(1, 2) }, new Move { new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(0, 1) } };

            Assert.IsTrue(actual.SequenceEqual(expected, _pathComparer));
        }

        [TestMethod]
        public void GetPossibleKingMovements_Case7()
        {
            var field = TestFieldData.King_Moves_Case7();
            var actual = _target.GetPossibleMovements(field, new Cell(7, 0));
            var expected = new List<Move> { new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(7, 0), new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) } };

            Assert.IsTrue(actual.SequenceEqual(expected, _pathComparer));
        }

        [TestMethod]
        public void GetPossibleKingMovements_Case8()
        {
            var field = TestFieldData.King_Moves_Case8();
            var actual = _target.GetPossibleMovements(field, new Cell(5, 2));
            var expected = new List<Move> { new Move { new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(6, 1), new Cell(7, 0) }, new Move { new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(6, 1), new Cell(7, 0) }, new Move { new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(6, 1), new Cell(7, 0) }, new Move { new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(6, 1), new Cell(7, 0) }, new Move { new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(6, 1), new Cell(7, 0) }, new Move { new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(6, 1), new Cell(7, 0) }, new Move { new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new Move { new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6), new Cell(0, 7) }, new Move { new Cell(5, 2), new Cell(6, 1), new Cell(7, 0) } };

            Assert.IsTrue(actual.SequenceEqual(expected, _pathComparer));
        }
    }
}
