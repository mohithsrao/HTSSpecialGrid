namespace SpecialGrid.Core.Models
{
    public abstract class Cell
    {
        public Cell(int x,int y)
        {
            X = x;
            Y = y;
        }

        public abstract int Value { get; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
