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
        private Label title = new Label("Noughts and Crosses", Global.BoardCentre, Global.BoardCentre, Label.DefaultColor);
        private Label singlePlayer = new Label("Singleplayer (AI)", Global.BoardCentre, Global.BoardCentre + 100, Label.DefaultColor);
        private Label multiPlayer = new Label("Multiplayer", Global.BoardCentre, Global.BoardCentre + 125, Label.DefaultColor);

        protected override void KeyPressed(Keys keyData)
        {
        }

        protected override void Render(Graphics g)
        {
            title.Draw(g);
            singlePlayer.Draw(g);
            multiPlayer.Draw(g);
        }

        protected override void MouseMove(Point location)
        {
            if(singlePlayer.Contains(location))
            {
                singlePlayer.ChangeSize(singlePlayer.StartSize + 1);
            } else
            {
                singlePlayer.ResetFont();
            }

            if (multiPlayer.Contains(location))
            {
                multiPlayer.ChangeSize(multiPlayer.StartSize + 1);
            }
            else
            {
                multiPlayer.ResetFont();
            }
        }

        protected override void OnClick(Point location)
        {
            if(singlePlayer.Contains(location))
            {
                State.AddNew(new Singleplayer(Global.BoardWidth, Global.BoardHeight));
                GotoNextState();
            } else if(multiPlayer.Contains(location))
            {
                State.AddNew(new Multiplayer(Global.BoardWidth, Global.BoardHeight));
                GotoNextState();
            }
        }
    }
}
