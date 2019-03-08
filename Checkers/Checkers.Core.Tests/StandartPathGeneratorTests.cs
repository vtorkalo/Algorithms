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
            var expected = new List<List<Cell>> { new List<Cell> { new Cell(5, 2), new Cell(4, 1) }, new List<Cell> { new Cell(5, 4), new Cell(4, 5), new Cell(3, 4), new Cell(2, 3), new Cell(1, 4), new Cell(0, 5), new Cell(1, 6), new Cell(2, 7), new Cell(3, 6), new Cell(4, 5), new Cell(5, 6), new Cell(6, 7) }, new List<Cell> { new Cell(5, 4), new Cell(4, 5), new Cell(3, 6), new Cell(2, 7), new Cell(1, 6), new Cell(0, 5), new Cell(1, 4), new Cell(2, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 7) }, new List<Cell> { new Cell(5, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 7) } };

            Assert.IsTrue(actual.SequenceEqual(expected, _pathComparer));
        }

        [TestMethod]
        public void GetPossibleMovements_Case2()
        {
            var field = TestFieldData.Standart_Moves_Case2();
            var actual = _target.GetPossibleMovements(field, new Cell(7, 2));
            var expected = new List<List<Cell>>();

            Assert.IsTrue(actual.SequenceEqual(expected, _pathComparer));
        }

        [TestMethod]
        public void GetPossibleMovements_Case3()
        {
            var field = TestFieldData.Standart_Moves_Case3();
            var actual = _target.GetPossibleMovements(field, new Cell(5, 4));
            var expected = new List<List<Cell>> { new List<Cell> { new Cell(4, 3) }, new List<Cell> { new Cell(4, 5) } };

            Assert.IsTrue(actual.SequenceEqual(expected, _pathComparer));
        }

        [TestMethod]
        public void GetPossibleMovements_Case4()
        {
            var field = TestFieldData.Standart_Moves_Case4();
            var actual = _target.GetPossibleMovements(field, new Cell(4, 3));
            var expected = new List<List<Cell>> { new List<Cell> { new Cell(3, 4), new Cell(2, 5) } };

            Assert.IsTrue(actual.SequenceEqual(expected, _pathComparer));
        }

        [TestMethod]
        public void GetPossibleMovements_Case5()
        {
            var field = TestFieldData.Standart_Moves_Case5();
            var actual = _target.GetPossibleMovements(field, new Cell(7, 0));
            var expected = new List<List<Cell>> { new List<Cell> { new Cell(6, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new List<Cell> { new Cell(6, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new List<Cell> { new Cell(6, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new List<Cell> { new Cell(6, 1), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 3), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new List<Cell> { new Cell(6, 1), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new List<Cell> { new Cell(6, 1), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new List<Cell> { new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(4, 5), new Cell(5, 6), new Cell(6, 5), new Cell(7, 4), new Cell(6, 3), new Cell(5, 2), new Cell(4, 1), new Cell(3, 0), new Cell(2, 1), new Cell(1, 2), new Cell(2, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new List<Cell> { new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 3), new Cell(1, 2), new Cell(2, 1), new Cell(3, 0), new Cell(4, 1), new Cell(5, 2), new Cell(6, 3), new Cell(7, 4), new Cell(6, 5), new Cell(5, 6), new Cell(4, 5), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) }, new List<Cell> { new Cell(6, 1), new Cell(5, 2), new Cell(4, 3), new Cell(3, 4), new Cell(2, 5), new Cell(1, 6) } };

            Assert.IsTrue(actual.SequenceEqual(expected, _pathComparer));
        }
    }
}
