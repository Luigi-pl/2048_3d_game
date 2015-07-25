namespace _2048_3d_game.Model
{
    class FieldPosition
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public FieldPosition()
        {
            Reset();
        }
        public FieldPosition(int x, int y, int z)
        {
            Reset(x, y, z);
        }
        public FieldPosition(FieldPosition position)
        {
            Reset(position.x, position.y, position.z);
        }

        public void Reset()
        {
            Reset(0, 0, 0);
        }
        public void Reset(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public void Reset(FieldPosition position)
        {
            Reset(position.x, position.y, position.z);
        }

        public bool Equals(FieldPosition position)
        {
            if (this.x == position.x && this.y == position.y && this.z == position.z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public FieldPosition Clone()
        {
            return (FieldPosition)this.MemberwiseClone();
        }
    }

}
