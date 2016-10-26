using System;
using System.Drawing;

namespace NoughtsAndCrosses
{
	public class Difficulty : State
	{
		private Label levelOne = new Label("Level One", Global.BoardCentre, Global.BoardCentre, Label.DefaultColor);
		private Label unbeatable = new Label("Maximum Difficulty", Global.BoardCentre, Global.BoardCentre + 55, Color.DarkRed);
		private Label levelTwo = new Label("Level Two", Global.BoardCentre, Global.BoardCentre + 25, Label.DefaultColor);
        private Label back = new Label("Back", Global.BoardCentre, Global.BoardCentre + 150, Label.DefaultColor);

		public Difficulty()
		{
			levelOne.SetButton(levelOneClicked);
			unbeatable.SetButton(unbeatableClicked, ButtonAnimation.HoverShake);
			levelTwo.SetButton(levelTwoClicked);
            back.SetButton(backClicked, ButtonAnimation.HoverUnderline);
		}

        protected override void Reset()
        {
            back.ResetPosition();
        }

        private void backClicked()
        {
            GotoPreviousState();
        }
                
		private void levelTwoClicked()
		{
			Global.AIDifficulty = 7;
			AddNew(new Singleplayer(300, 300));
			GotoNextState();
		}
		
		private void unbeatableClicked() 
		{
			Global.AIDifficulty = 0;
			AddNew(new Singleplayer(300, 300));
			GotoNextState();
		}
		
		private void levelOneClicked() 
		{
			Global.AIDifficulty = 15;
			AddNew(new Singleplayer(300, 300));
			GotoNextState();
		}
		
		protected override void Render (Graphics g)
		{
			levelOne.Draw(g);
			unbeatable.Draw(g);
			levelTwo.Draw(g);
            back.Draw(g);
		}
		
		protected override void OnClick (Point location)
		{
			levelOne.MouseClick(location);
			unbeatable.MouseClick(location);
			levelTwo.MouseClick(location);
            back.MouseClick(location);
		}
		
		protected override void MouseMove (Point location)
		{
            unbeatable.MouseMove(location);
			levelOne.MouseMove(location);
			levelTwo.MouseMove(location);
            back.MouseMove(location);
		}

        protected override void Tick()
        {
            unbeatable.Tick();
            back.Tick();
        }
    }
}

