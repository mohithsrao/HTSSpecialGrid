using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecialGrid.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialGrid.Core.Models.Tests
{
    [TestClass()]
    public class ShadedCellTests
    {
        [TestMethod()]
        public void ShadedCellTest()
        {
            var x = 1;
            var y = 2;
            var value = 25;
            var cell = new ShadedCell(x, y, value);
            
            Assert.IsTrue(cell.X == x);
            Assert.IsTrue(cell.Y == y);
            Assert.IsTrue(cell.Value == value);
        }
    }
}