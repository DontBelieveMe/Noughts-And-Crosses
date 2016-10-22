using System;
using System.Windows.Forms;

namespace NoughtsAndCrosses
{
    public class Pause
    {
        public Pause(int seconds): this((double) seconds)
        {    
        }

        public Pause(double seconds)
        {
            DateTime time = DateTime.Now.AddSeconds(seconds);
            while (DateTime.Now < time)
            {
                Application.DoEvents();
            }
        }
    }
}
