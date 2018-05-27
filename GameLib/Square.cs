using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameLib
{
    public class Square
    {
        private int image;
        private int x;
        private int y;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Image { get => image; set => image = value; }
    }
}