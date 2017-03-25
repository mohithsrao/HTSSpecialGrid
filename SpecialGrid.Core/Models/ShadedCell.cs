using System;

namespace SpecialGrid.Core.Models
{
    public class ShadedCell : Cell
    {
        private int _value = 0;

        public ShadedCell(int x, int y, int value) : base(x, y)
        {
            _value = value;
        }

        public override int Value
        {
            get
            {
                return _value;
            }
        }
    }
}
