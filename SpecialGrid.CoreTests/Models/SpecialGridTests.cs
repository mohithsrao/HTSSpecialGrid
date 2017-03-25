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
    public class SpecialGridTests
    {
        int testSize = 7;
        IEnumerable<int> testNumberList = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
        IEnumerable<ShadedCell> testShadedCellList = new List<ShadedCell>() {
                new ShadedCell(1,1,26),
                new ShadedCell(1,3,35),
                new ShadedCell(1,5,37),

                new ShadedCell(3,1,31),
                new ShadedCell(3,3,36),
                new ShadedCell(3,5,39),

                new ShadedCell(5,1,38),
                new ShadedCell(5,3,26),
                new ShadedCell(5,5,24),
            };

        [TestMethod()]
        public void SpecialGridTest()
        {
            var grid = new SpecialGrid(testSize, testShadedCellList, testNumberList);

            Assert.IsTrue(grid.Size == testSize);
            Assert.IsTrue(grid.InitList == testShadedCellList);
            Assert.IsTrue(grid.NumberList == testNumberList);
        }

        [TestMethod]
        public void SpecialGridDataTerst()
        {
            var grid = new SpecialGrid(testSize, testShadedCellList, testNumberList);

            Assert.IsTrue(grid.Data.GetType() == typeof(Cell[,]));
            Assert.IsTrue(grid.Data[0, 0].GetType() == typeof(WhiteCell));
            Assert.IsTrue(grid.Data[1, 1].GetType() == typeof(ShadedCell));

            Assert.IsTrue(grid.Data[0, 0].Value == default(int));
            Assert.IsTrue(grid.Data[1, 1].Value == 26);
        }
    }
}