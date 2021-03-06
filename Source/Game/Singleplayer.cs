﻿using System;
using System.Windows.Forms;
using System.Drawing;

namespace NoughtsAndCrosses
{
    public class Singleplayer : GameBoard
    {
        public Singleplayer(int width, int height) : base(width, height)
        {
            SetPlayerType(ObjectType.Cross);
        }

        private void RunAI()
        {
            turn = WhooseTurn.PlayerTwo;

            new Pause(1);
            AI ai = new AI();
            ai.Execute(this);

            turn = WhooseTurn.PlayerOne;
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

        protected override void RenderTurnString(Graphics g)
        {
            string whooseTurn = (turn == WhooseTurn.PlayerOne) ? "Player One's turn (" + playerType + ")" : "Player Two's turn (" + PlayerTwoType() + ")";
            g.DrawString(whooseTurn, Game.Font, Game.FontColor, 5, 320);
        }

        protected override void OnClick(Point point)
        {
            // Don't handle GUI clicks.
            if (point.Y > height)
                return;

            // Don't allow clicks if the AI is 'thinking'
            if (turn == WhooseTurn.PlayerTwo)
                return;

            int snappedX = point.X / Global.TileSize;
            int snappedY = point.Y / Global.TileSize;

            if (TileAt(snappedX, snappedY))
                return;

            DeterminteObjectType(out tiles[snappedX, snappedY], snappedX, snappedY);

            if (SomebodyWon())
                return;

            RunAI();

            if (SomebodyWon())
                return;
        }
    }
}
