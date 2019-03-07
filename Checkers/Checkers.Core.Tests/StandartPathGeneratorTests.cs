using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.Core.Tests
{
    [TestClass]
    public class StandartPathGeneratorTests
    {
        StandartPathGenerator _target;

        [TestInitialize]
        public void TestInitialize()
        {
            _target = new StandartPathGenerator();
        }

        [TestMethod]
        public void TestMethod1()
        {
            //var field = TestFieldData.Case();
            //var actual = _target.GetPossibleMovements(field, new Cell (3, 4));
        }
    }
}
