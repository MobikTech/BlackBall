namespace BlackBall.Common.Math.Types
{
    public struct Coords
    {
        public readonly int X;
        public readonly int Y;
        
        public static readonly Coords[] Directions = {
            new(0, 1),
            new(1, 0),
            new(0, -1),
            new(-1, 0),
        };

        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator (int, int)(Coords coords) => (coords.X, coords.Y);
        public static implicit operator Coords((int x, int y) coords) => new Coords(coords.x, coords.y);

        public static bool operator ==(Coords @this, Coords that) => 
            @this.X == that.X && @this.Y == that.Y;

        public static bool operator !=(Coords @this, Coords that) => 
            !(@this == that);

        public static Coords operator +(Coords @this, Coords that) =>
            new Coords(@this.X + that.X, @this.Y + that.Y);
        
        public static Coords operator -(Coords @this, Coords that) =>
            new Coords(@this.X - that.X, @this.Y - that.Y);
        
        public static Coords operator /(Coords @this, int divider) =>
            new Coords(@this.X / divider,@this.Y / divider);
        public static Coords operator *(Coords @this, int multiplier) =>
            new Coords(@this.X * multiplier,@this.Y * multiplier);

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}