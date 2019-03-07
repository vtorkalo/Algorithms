using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.Core.Tests
{
    [TestClass]
    public class KingPathGeneratorTests
    {
        KingPathGenerator _target;

        [TestInitialize]
        public void TestInitialize()
        {
            _target = new KingPathGenerator();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var field = TestFieldData.King_Moves_Case1();
            var actual = _target.GetPossibleKingMovements(field, new Cell (3, 4));
        }
    }
}
