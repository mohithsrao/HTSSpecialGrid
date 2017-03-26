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

        private void FillCell(bool isRowData, List<Cell> data, int index, int position = 0,int numIndex = 0)
        {
            if (data.All(x => x.Value != 0)) return;
            if (isRowData ? IsRowValid(index) : IsColumnValid(index)) return;
            if (position >= Size || numIndex >= WhiteCell.AvailableNumbers.Count()) return;

            var list = WhiteCell.AvailableNumbers.ToList();
            if (data[position].Value == 0)
            {
                var cell = data[position] as WhiteCell;
                if (IsValueValidForCell(cell, WhiteCell.AvailableNumbers.ElementAt(numIndex)))
                {
                    cell.SetValue(WhiteCell.AvailableNumbers.ElementAt(numIndex));
                    var nextIndex = GetIndexOfMaxCell(cell);
                    
                    FillCell(isRowData, data, index, ++position, nextIndex);
                }
                else
                {
                    var nextIndex = GetIndexOfMaxCell(cell);
                    FillCell(isRowData, data, index, position, nextIndex);
                }
            }
            else
            {
                var cell = data[position];
                var nextIndex = GetIndexOfMaxCell(cell);
                FillCell(isRowData, data, index, ++position, nextIndex);
            }
        }

        private int GetIndexOfMaxCell(Cell cell)
        {
            var list = WhiteCell.AvailableNumbers.ToList();
            var row = GetRowData(cell.X).Where(x => x is WhiteCell);
            var col = GetColumnData(cell.Y).Where(x => x is WhiteCell);
            var maxIndex = list.IndexOf(Math.Max(row.Max(x => x.Value), col.Max(x => x.Value)));
            if (maxIndex >= list.Count-1)
            {
                return 0;
            }
            else
            {
                return  ++maxIndex;
            }
        }

        public void FillGrid_recursive()
        {
            var str = string.Empty;
            FillGridInitial();
            FillCell_Recursive(Data,GetCellAtPosition(0));
            str = ToString();
        }

        private void FillCell_Recursive(Cell[,] data, Cell cell, int index = 1)
        {
            var pos = GetindexOfCell(cell);
            var nxtPos = pos == Math.Pow(Size, 2) ? 0 : ++pos;
            if (IsGridValid()) return;
            if (cell is ShadedCell) FillCell_Recursive(data, GetCellAtPosition(nxtPos), index);

            if (cell is WhiteCell)
            {
                var whiteCell = cell as WhiteCell;

                if (IsValueValidForCell(whiteCell, index))
                {
                    whiteCell.SetValue(index);
                    FillCell_Recursive(data, GetCellAtPosition(nxtPos), GetNextNumber(index));
                }
                else
                {
                    FillCell_Recursive(data, GetCellAtPosition(nxtPos), GetNextNumber(index));
                }
            }
        }

        private bool IsGridValid()
        {
            var isValid = true;
            for (int i = 0; i < Size; i++)
            {
                isValid = IsRowValid(i);
                if (!isValid) return false;
                isValid = IsColumnValid(i);
                if (!isValid) return false;
            }
            var sumBool = InitList.All(x => ValidateSumArroundShadedCell(x));
            return isValid && sumBool;
        }

        private bool ValidateSumArroundShadedCell(ShadedCell cell)
        {
            return GetCellsArroundShadedCell(cell).Sum(x => x.Value) == cell.Value;
        }

        private int GetNextNumber(int index)
        {
            if (index >= WhiteCell.AvailableNumbers.Max())
            {
                return 1;
            }
            else
            {
                return ++index;
            }
        }

        private Cell GetCellAtPosition(int index)
        {
            if (index < 0 || index >= Math.Pow(Size,2)) index = 0;
            var x = (int)Math.Floor(Convert.ToDecimal(index / Size));
            var y = (int)Math.Floor(Convert.ToDecimal(index % Size));
            return Data[x, y];
        }

        private int GetindexOfCell(Cell cell)
        {
            int count = 0;
            foreach (var item in Data)
            {
                
                if (item == cell)
                {
                    return count;
                }
                count++;
            }
            return count;
        }

        private IEnumerable<WhiteCell> GetCellsArroundShadedCell(ShadedCell shadedCell)
        {
            var list = new List<WhiteCell>();
            for (int i = shadedCell.X - 1; i <= shadedCell.X + 1; i++)
            {
                for (int j = shadedCell.Y - 1; j <= shadedCell.Y + 1; j++)
                {
                    if (i < 0 || i >= Size || j < 0 || j >= Size) continue;
                    if (Data[i, j] is WhiteCell)
                    {
                        list.Add(Data[i, j] as WhiteCell);
                    }
                }
            }
            return list;
        }

        public void FillGridInitial()
        {
            Dictionary<int, List<int>> rowSolutions = GetAllRowSolutions();
            var count = 1;
            string grid1 = string.Empty;
            for (int i = 1; i < Size; i = i + 2)
            {
                for (int j = (i - 1); j <= (i + 1); j++)
                {
                    if (j % 2 == 0)
                    {
                        if (!IsRowValid(j))
                        {
                            FillRow(j, rowSolutions[count++]);
                        }
                    }
                }
            }
        }

        private Dictionary<int, List<int>> GetAllRowSolutions()
        {
            var rowSolutions = new Dictionary<int, List<int>>();
            for (int i = 0; i < Size; i++)
            {
                var rowList = new List<int>();
                for (int j = 0; j < Size; j++)
                {
                    rowList.Add(((i + j) % 7) + 1);
                }
                rowSolutions.Add(i + 1, rowList);
            }

            return rowSolutions;
        }

        private bool IsValueValidForCell(WhiteCell cell, int value)
        {
            var row = GetRowData(cell.X);
            var col = GetColumnData(cell.Y);
            return IsValueValidForSum(cell, value) && (row.All(x => x.Value != value) && col.All(x => x.Value != value));
        }

        private bool IsValueValidForSum(WhiteCell cell, int value)
        {
            var shadedList = GetShadedCellArround(cell);
            return shadedList.All(x => ValidateSumArroundShadedCell(x as ShadedCell,cell, value));
        }

        private bool ValidateSumArroundShadedCell(ShadedCell shadedCell, WhiteCell whitecell, int value)
        {
            var sum = value;
            for (int i = shadedCell.X - 1; i <= shadedCell.Y + 1; i++)
            {
                for (int j = shadedCell.Y - 1; j <= shadedCell.Y + 1; j++)
                {
                    if (i < 0 || i >= Size || j < 0 || j >= Size) continue;
                    if (Data[i, j] is WhiteCell && Data[i, j] != whitecell)
                    {
                        sum += Data[i, j].Value;
                    }
                }
            }
            return sum <= shadedCell.Value;
        }

        private List<ShadedCell> GetShadedCellArround(WhiteCell cell)
        {
            var list = new List<ShadedCell>();
            for (int i = cell.X - 1; i <= cell.X + 1; i++)
            {
                for (int j = cell.Y - 1; j <= cell.Y + 1; j++)
                {
                    if (i < 0 || i >= Size || j < 0 || j >= Size) continue;
                    if (Data[i, j] is ShadedCell)
                    {
                        list.Add(Data[i, j] as ShadedCell);
                    }
                }
            }
            return list;
        }

        public bool IsRowValid(int index)
        {
            var rowData = GetRowData(index);
            return rowData.Where(cell => cell.Value != 0).GroupBy(x => x.Value).Count() == Size;
        }

        public void FillRow(int index)
        {
            var rowData = GetRowData(index);
            //FillCell(true, rowData, index);
            for (int i = 0; i < rowData.Count; i++)
            {
                if (rowData[i] is WhiteCell)
                {
                    (rowData[i] as WhiteCell).SetValue(((index + i) % 7) + 1);
                }
            }
        }

        public void FillRow(int index,List<int> data)
        {
            var rowData = GetRowData(index);
            for (int i = 0; i < rowData.Count; i++)
            {
                var cell = rowData[i] as WhiteCell;
                if (cell != null)
                {
                    if (cell.Value == 0)
                    {
                        cell.SetValue(data[i]);
                    }
                }
            }
        }

        public bool IsColumnValid(int index)
        {
            var columnData = GetColumnData(index);
            return columnData.Where(cell => cell.Value != 0).GroupBy(x => x.Value).Count() == Size;
        }

        public void FillColumn(int index)
        {
            var columnData = GetColumnData(index);
            FillCell(false, columnData, index);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    builder.Append(Data[i, j] + "\t");
                }
                builder.Append("\n");
            }
            var str = builder.ToString();
            return builder.ToString();
        }
    }
}
