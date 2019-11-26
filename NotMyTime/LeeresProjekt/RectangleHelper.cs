using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotMyTime
{
    static class RectangleHelper
    {
        public static bool TouchCheck(this Rectangle r1, Rectangle r2,int size)
        {
            return (r1.X == r2.X && r1.Y == r2.Y);
        }
    }
}
