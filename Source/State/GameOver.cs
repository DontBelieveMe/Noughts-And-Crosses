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
        public Winner Winner { set; get; }

        private Label clickToContinue = new Label("Click to continue", Global.BoardCentre, Global.BoardCentre + 100, Label.DefaultColor);
        private Label wonLabel = new Label("", Global.BoardCentre, Global.BoardCentre, Label.DefaultColor);

        public GameOver(): base()
        {
        }

        protected override void KeyPressed(Keys keyData)
        {
        }

        protected override void OnClick(Point location)
        {
            State.ClearStack();
            State.AddNew(new MainMenu());
            GotoFirstState();
        }

        protected override void Render(Graphics g)
        {
            // Allign the string in the middle.
            StringFormat formatter = new StringFormat();
            formatter.LineAlignment = StringAlignment.Center;
            formatter.Alignment = StringAlignment.Center;
            string whooseWon = "";
            switch (Winner)
            {
                case Winner.PlayerOne:
                    whooseWon = "Player One Won!";
                    break;
                case Winner.PlayerTwo:
                    whooseWon = "Player Two Won!";
                    break;
                case Winner.Draw:
                    whooseWon = "Its a Draw!";
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

            wonLabel.Text = whooseWon;

            wonLabel.Draw(g);
            clickToContinue.Draw(g);
        }
    }
}
