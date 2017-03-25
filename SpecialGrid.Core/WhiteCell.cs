using System.Collections.Generic;

namespace SpecialGrid.Core
{
    public class WhiteCell : Cell
    {
        public static IEnumerable<int> AvailableNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7 };

        public WhiteCell(int x, int y) : base(x, y, 0)
        {
        }
    }
}
