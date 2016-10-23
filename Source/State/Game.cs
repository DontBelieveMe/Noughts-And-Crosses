/*
TODO:
	- Centralize widths and heights
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
			int width = 300, height = 375;
			if(Environment.OSVersion.Platform == PlatformID.Win32Windows)
			{
				width += 15;
			}
			Window = new Window(width, height, "Noughts and Crosses");

            Window.SetPaintFunction(State.Draw);
            Window.SetTickFunction(State.Update);
            Window.SetClickFunction(State.ProcessClick);
            Window.SetKeyboardFunction(State.ProcessKeyDown);
            Window.SetMoveMoveFunction(State.ProcessMouseMove);

            State.AddNew(new MainMenu());
            
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
