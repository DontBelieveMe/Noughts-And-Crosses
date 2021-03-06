using System;
using System.Drawing;

namespace NoughtsAndCrosses
{
	public class Difficulty : State
	{
		private Label levelOne = new Label("Level One", Global.BoardCentre, Global.BoardCentre, Label.DefaultColor);
		private Label unbeatable = new Label("UNBEATABLE", Global.BoardCentre, Global.BoardCentre + 55, Color.DarkRed);
		private Label levelTwo = new Label("Level Two", Global.BoardCentre, Global.BoardCentre + 25, Label.DefaultColor);
		
		public Difficulty()
		{
			levelOne.SetButton(levelOneClicked);
			unbeatable.SetButton(unbeatableClicked);
			levelTwo.SetButton(levelTwoClicked);
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
		}
		
		protected override void OnClick (Point location)
		{
			levelOne.MouseClick(location);
			unbeatable.MouseClick(location);
			levelTwo.MouseClick(location);
		}
		
		protected override void MouseMove (Point location)
		{
			if(unbeatable.Contains(location)) {
				unbeatable.MoveMove(location);
				unbeatable.Underline();	
			} else {
				unbeatable.ResetFont();	
			}
			
			levelOne.MoveMove(location);
			levelTwo.MoveMove(location);
		}
	}
}

