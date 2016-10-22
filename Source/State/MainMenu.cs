using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoughtsAndCrosses
{
    public class MainMenu : State
    {
        protected override void KeyPressed(Keys keyData)
        {
            if(keyData.HasFlag(Keys.G))
            {
                GotoNextState();
            }
            else if(keyData.HasFlag(Keys.B))
            {
                GotoNextState();
            }
        }

        protected override void Render(Graphics g)
        {
            StringFormat formatter = new StringFormat();
            formatter.LineAlignment = StringAlignment.Center;
            formatter.Alignment = StringAlignment.Center;

            g.DrawString("Noughts and crosses", Game.Font, Game.FontColor, 150, 50, formatter);
            g.DrawString("Singleplayer (AI) [Press G]", Game.Font, Game.FontColor, 150, 250, formatter);
            g.DrawString("Multiplayer (Two humans) [Press B]", Game.Font, Game.FontColor, 150, 300, formatter);
        }
    }
}
