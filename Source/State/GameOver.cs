using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoughtsAndCrosses
{
    public class GameOver : State
    {
        public string Winner { set; get; }

        public GameOver(): base()
        {
        }

        protected override void KeyPressed(Keys keyData)
        {
            if(keyData == Keys.Space)
            {
                GotoFirstState();
            }
        }

        protected override void Render(Graphics g)
        {
            // Allign the string in the middle.
            StringFormat formatter = new StringFormat();
            formatter.LineAlignment = StringAlignment.Center;
            formatter.Alignment = StringAlignment.Center;
            
            // TODO: Use the global widths/heights
            g.DrawString(Winner, Game.Font, Game.FontColor, 150, 150, formatter);
            g.DrawString("Press space to continue...", Game.Font, Game.FontColor, 150, 250, formatter);
        }
    }
}
