namespace _2048_3d_game.Model
{
    /// <summary>
    /// Class is used to store data about signle field
    /// </summary>
    class FieldPosition
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public FieldPosition()
        {
            SetFieldPosition(0, 0, 0);
        }
        public FieldPosition(int x, int y, int z)
        {
            SetFieldPosition(x, y, z);
        }
        public FieldPosition(FieldPosition position)
        {
            SetFieldPosition(position.x, position.y, position.z);
        }

        /// <summary>
        /// Method sets data about field position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetFieldPosition(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        /// <summary>
        /// Method sets data about field position
        /// </summary>
        /// <param name="position"></param>
        public void SetFieldPosition(FieldPosition position)
        {
            SetFieldPosition(position.x, position.y, position.z);
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
