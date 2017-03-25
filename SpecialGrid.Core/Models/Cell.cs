namespace SpecialGrid.Core.Models
{
    public abstract class Cell
    {
        public Cell(int x,int y,int value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        public int Value { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
    }
}
