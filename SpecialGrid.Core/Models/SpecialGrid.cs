using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialGrid.Core.Models
{
    public class SpecialGrid
    {
        public SpecialGrid(int size,IEnumerable<ShadedCell> initList,IEnumerable<int> numberList)
        {
            Size = size;
            InitList = initList;
            NumberList = numberList;

            Data = new Cell[size, size];

            InitializeGrid(InitList);
        }

        private void InitializeGrid(IEnumerable<ShadedCell> initList)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (initList.Any(cell => cell.X == i && cell.Y == j))
                    {
                        Data[i, j] = initList.First(cell => cell.X == i && cell.Y == j);
                    }
                    else
                    {
                        Data[i, j] = new WhiteCell(i, j);
                    }
                }
            }
        }

        public IEnumerable<ShadedCell> InitList { get; private set; }
        public int Size { get; private set; }
        public Cell[,] Data { get; private set; }
        public IEnumerable<int> NumberList { get; private set; }
    }
}
