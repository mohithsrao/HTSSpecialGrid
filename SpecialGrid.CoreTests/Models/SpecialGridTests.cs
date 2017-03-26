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
        int testSize = 5;
        IEnumerable<int> testNumberList = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
        IEnumerable<ShadedCell> testShadedCellList = new List<ShadedCell>() {
                new ShadedCell(1,1,26),
                new ShadedCell(1,3,35),
                //new ShadedCell(1,5,37),

                new ShadedCell(3,1,31),
                new ShadedCell(3,3,36),
                //new ShadedCell(3,5,39),

                //new ShadedCell(5,1,38),
                //new ShadedCell(5,3,26),
                //new ShadedCell(5,5,24),
            };

        [TestMethod()]
        public void SpecialGridTest()
        {
            SpecialGrid grid = InitSpecialGrid();

            Assert.IsTrue(grid.Size == testSize);
            Assert.IsTrue(grid.InitList == testShadedCellList);
            Assert.IsTrue(grid.NumberList == testNumberList);
        }

        private SpecialGrid InitSpecialGrid()
        {
            return new SpecialGrid(testSize, testShadedCellList, testNumberList);
        }

        [TestMethod]
        public void SpecialGridDataTerst()
        {
            var grid = InitSpecialGrid();

            Assert.IsTrue(grid.Data.GetType() == typeof(Cell[,]));
            Assert.IsTrue(grid.Data[0, 0].GetType() == typeof(WhiteCell));
            Assert.IsTrue(grid.Data[1, 1].GetType() == typeof(ShadedCell));

            Assert.IsTrue(grid.Data[0, 0].Value == default(int));
            Assert.IsTrue(grid.Data[1, 1].Value == 26);
        }

        [TestMethod]
        public void FillOneRowTest()
        {
            var grid = InitSpecialGrid();
            var zeroIindex = 0;

            grid.FillRow(zeroIindex);

            Assert.IsTrue(grid.Data[zeroIindex, 0].Value == 1);
            //Assert.IsTrue(grid.Data[zeroIindex, 6].Value == 7);
            Assert.IsTrue(grid.IsRowValid(zeroIindex));

            //var oneIndex = 1;
            //grid.FillRow(oneIndex);
            //Assert.IsTrue(grid.Data[oneIndex, 0].Value == 2);
            //Assert.IsTrue(grid.Data[oneIndex, 6].Value == 1);
            //Assert.IsTrue(grid.IsRowValid(oneIndex));
        }

        //[TestMethod]
        //public void FillOneColumnTest()
        //{
        //    var grid = InitSpecialGrid();
        //    var index = 0;

        //    grid.FillColumn(index);

        //    Assert.IsTrue(grid.Data[0, 0].Value == 1);
        //    Assert.IsTrue(grid.Data[6, 0].Value == 7);
        //    Assert.IsTrue(grid.IsColumnValid(index));

        //    var oneIndex = 1;
        //    grid.FillColumn(oneIndex);
        //    Assert.IsTrue(grid.Data[0, oneIndex].Value == 2);
        //    Assert.IsTrue(grid.Data[6, oneIndex].Value == 1);
        //    Assert.IsTrue(grid.IsColumnValid(oneIndex));
        //}

        //[TestMethod]
        //public void FillAlternateRows()
        //{
        //    var grid = InitSpecialGrid();

        //    for (int i = 0; i < grid.Size; i++)
        //    {
        //        if (i % 2 == 0)
        //        {
        //            grid.FillRow(i);
        //            Assert.IsTrue(grid.IsRowValid(i));
        //        }
        //    }
        //}

        [TestMethod]
        public void FillSpecialGridTest()
        {
            var grid = InitSpecialGrid();
            grid.FillGrid_recursive();
            var stringGrid = grid.ToString();
        }
    }
}