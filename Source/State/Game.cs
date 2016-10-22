/*
TODO:
    - Two player
*/

using System;
using System.Windows.Forms;
using System.Drawing;

namespace NoughtsAndCrosses
{
    public class Game
    {
        public Window Window;

        public static Font Font = new Font("Consolas", 10, FontStyle.Bold);
        public static Brush FontColor = Brushes.DarkSlateGray;

        public Game()
        {
            Window = new Window(315, 375, "Noughts and Crosses");

            Window.SetPaintFunction(State.Draw);
            Window.SetTickFunction(State.Update);
            Window.SetClickFunction(State.ProcessClick);
            Window.SetKeyboardFunction(State.ProcessKeyDown);
            Window.SetMoveMoveFunction(State.ProcessMouseMove);

            State.AddNew(new MainMenu());
            State.AddNew(new GameBoard(300, 300));
            State.AddNew(new GameOver());

            State.Start();
        }

        [STAThread]
        public static void Main()
        {
            Game game = new Game();
            Application.Run(game.Window);
        }
        
    }
}
