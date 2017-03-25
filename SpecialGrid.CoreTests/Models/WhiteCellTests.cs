using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace SpecialGrid.Core.Models.Tests
{
    [TestClass()]
    public class WhiteCellTests
    {
        [TestMethod()]
        public void WhiteCellTest()
        {
            var X = 1;
            var Y = 2;
            var cell = new WhiteCell(X, Y);
            Assert.IsTrue(cell.X == X);
            Assert.IsTrue(cell.Y == Y);
            Assert.IsTrue(cell.Value == default(int));
        }
        [TestMethod]
        public void WhiteCellStaticPropertyTest()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };

            for (int i = 0; i < numbers.Count; i++)
            {
                Assert.IsTrue(WhiteCell.AvailableNumbers.ElementAt(i) == numbers[i]);
            }
        }
    }
}