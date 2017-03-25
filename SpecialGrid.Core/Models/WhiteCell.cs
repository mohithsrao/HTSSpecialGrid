using System;
using System.Collections.Generic;

namespace SpecialGrid.Core.Models
{
    public class WhiteCell : Cell
    {
        public static IEnumerable<int> AvailableNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        private int _value = 0;

        public WhiteCell(int x, int y) : base(x, y)
        {
        }

        public override int Value
        {
            get
            {
                return _value;
            }
        }

        public void SetValue(int number)
        {
            _value = number;
        }
    }
}
