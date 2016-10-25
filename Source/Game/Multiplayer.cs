using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NoughtsAndCrosses
{
    public class Multiplayer : GameBoard
    {
        public Multiplayer(int width, int height): base(width, height) { }

        protected override void RenderTurnString(Graphics g)
        {
            if(turn == WhooseTurn.PlayerOne)
                g.DrawString("Player One's turn (Cross)", Game.Font, Game.FontColor, 5, 320);
            else
                g.DrawString("Player Twos's turn (Nought)", Game.Font, Game.FontColor, 5, 320);

        }

        protected override void OnClick(Point point)
        {
            // Don't handle GUI clicks.
            if (point.Y > height)
                return;

            
            int snappedX = point.X / Global.TileSize;
            int snappedY = point.Y / Global.TileSize;

            if (TileAt(snappedX, snappedY))
                return;

            if (turn == WhooseTurn.PlayerOne)
                tiles[snappedX, snappedY] = new Cross(snappedX, snappedY);
            else if (turn == WhooseTurn.PlayerTwo)
                tiles[snappedX, snappedY] = new Nought(snappedX, snappedY);

            turn = (turn == WhooseTurn.PlayerOne) ? WhooseTurn.PlayerTwo : WhooseTurn.PlayerOne;

            if (SomebodyWon())
                return;

            if (turn == WhooseTurn.PlayerTwo)
                indicator = new Nought(snappedX, snappedY);
            else
                indicator = new Cross(snappedX, snappedY);

        }

        protected new bool SomebodyWon()
        {
            EvaluateWinLoose winLoose = new EvaluateWinLoose();
            Winner result = winLoose.Evaluate(this);
            if (result != Winner.Continue)
            {
                new Pause(.5);
                State.AddNew(new GameOver());
                GetState<GameOver>().Winner = result;
                GotoNextState();
                return true;
            }

            return false;
        } 
    }
}
