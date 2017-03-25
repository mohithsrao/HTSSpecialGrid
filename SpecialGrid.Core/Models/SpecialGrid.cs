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
        
        public IEnumerable<ShadedCell> InitList { get; private set; }
        public int Size { get; private set; }
        public Cell[,] Data { get; private set; }
        public IEnumerable<int> NumberList { get; private set; }

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

        private List<Cell> GetRowData(int index)
        {
            var rowData = new List<Cell>();
            for (int i = 0; i < Size; i++)
            {
                rowData.Add(Data[index, i]);
            }
            return rowData;
        }

        private List<Cell> GetColumnData(int index)
        {
            var columnData = new List<Cell>();
            for (int i = 0; i < Size; i++)
            {
                columnData.Add(Data[i, index]);
            }
            return columnData;
        }

        private void FillCell(bool isRowData, List<Cell> data, int index, int position = 0)
        {
            if (data.All(x => x.Value != 0)) return;
            if (isRowData ? IsRowValid(index) : IsColumnValid(index)) return;
            //if (position >= WhiteCell.AvailableNumbers.Count()) return;

            var list = WhiteCell.AvailableNumbers.ToList();
            var numIndex = 0;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Value == 0)
                {
                    var cell = data[i] as WhiteCell;
                    cell.SetValue(WhiteCell.AvailableNumbers.ElementAt(numIndex));
                    if (++numIndex >= WhiteCell.AvailableNumbers.Count())
                    {
                        numIndex = 0;
                    }
                }
                else
                {
                    if (data[i] is WhiteCell)
                    {
                        numIndex = list.IndexOf(data[i].Value) + 1;
                    }
                }
            }
        }

        public bool IsRowValid(int index)
        {
            var rowData = GetRowData(index);
            return rowData.Where(cell => cell.Value != 0).GroupBy(x => x.Value).Count() == WhiteCell.AvailableNumbers.Count();
        }

        public void FillRow(int index)
        {
            var rowData = GetRowData(index);
            FillCell(true, rowData, index);
        }

        public bool IsColumnValid(int index)
        {
            var columnData = GetColumnData(index);
            return columnData.Where(cell => cell.Value != 0).GroupBy(x => x.Value).Count() == WhiteCell.AvailableNumbers.Count();
        }

        public void FillColumn(int index)
        {
            var columnData = GetColumnData(index);
            FillCell(false, columnData, index);
        }
    }
}
