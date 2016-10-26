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

        private const double animationTime = 0.002;

		private void singlePlayerClicked() {
            AddNew(new Difficulty());
			GotoNextState();
		}
		
		private void multiPlayerClicked() {
            AddNew(new Multiplayer(300, 300));
			GotoNextState();
		}
		
		public MainMenu()
		{
			singlePlayer = singlePlayer.SetButton(singlePlayerClicked, ButtonAnimation.SlideOffRight);	
			multiPlayer = multiPlayer.SetButton(multiPlayerClicked, ButtonAnimation.SlideOffLeft);
		}

        protected override void Reset()
        {
            singlePlayer.ResetPosition();
            multiPlayer.ResetPosition();
        }

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
            singlePlayer.MouseMove(location);
			multiPlayer.MouseMove(location);	
        }

        protected override void OnClick(Point location)
        {
            singlePlayer.MouseClick(location);
			multiPlayer.MouseClick(location);
        }
        
    }
}
