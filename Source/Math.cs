using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsAndCrosses
{
    public static class MathToolbox
    {
        public static float PingPong(float t, float len)
        {
            return t % len;
        }
    }
}
