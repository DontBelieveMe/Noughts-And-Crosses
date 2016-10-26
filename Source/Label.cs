using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NoughtsAndCrosses
{
    [Flags]
    public enum ButtonAnimation
    {
        SlideOffRight  = 0,
        SlideOffLeft   = 1,
        HoverShake     = 4,
        None           = 8,
        HoverUnderline = 16
    }
    
    public class Label
    {
        public static Font DefaultFont = new Font("Consolas", 10, FontStyle.Bold);
        public static Color DefaultColor = Color.DarkSlateGray;

        private Font font = DefaultFont;

        public string Text { get; set; }
        public Point Position { get; private set; }
        public int Size { get { return (int)font.Size; } }

        public int StartSize { get; private set; }
        private bool isButton = false;
        private ButtonAnimation animationArgs = ButtonAnimation.None;

		private Action OnClick;
		private Brush brush;
        private StringAlignment alignment = StringAlignment.Center;

        public Rectangle Bounds;
        private Point InitalPos;

        public void Tick()
        {
            if (Bounds.Contains(Global.MousePos))
            {
                if (animationArgs.HasFlag(ButtonAnimation.HoverShake))
                {
                    TimeSpan time = DateTime.Now - System.Diagnostics.Process.GetCurrentProcess().StartTime;
                    int ms = time.Milliseconds;

                    // Some clever maths'y stuff
                    int xo = (int)(Math.Sin(ms * 6 % 10000 / 1000.0 * Math.PI * 2) * 100);
                    Position = new Point(Position.X + (xo / 50), Position.Y);
                }
            }
        }

        public Label(string text, int x, int y, Color color)
        {
            Text = text;
            Position = new Point(x, y);
            Bounds = new Rectangle(x, y, 0, 0);
            InitalPos = new Point(Position.X, Position.Y);

            brush = new SolidBrush(color);
            StartSize = (int) font.Size;
        }

        public void ChangeSize(int size)
        {
            font = new Font(font.FontFamily, size, font.Style);
        }
        
        public void Draw(Graphics g)
        {
            StringFormat format = new StringFormat();
            format.Alignment = alignment;
            format.LineAlignment = alignment;
            g.DrawString(Text, font, brush, Position.X, Position.Y, format);

            SizeF size = g.MeasureString(Text, font);

            // The divide by two accounts for the fact that the text is centered.
            Bounds.X = Position.X - Bounds.Width / 2;
            Bounds.Y = Position.Y - Bounds.Height / 2;

            Bounds.Width = (int) size.Width;
            Bounds.Height = (int) size.Height;
        }
        
        public bool Contains(Point point)
        {
            return Bounds.Contains(point);
        }

        public void Underline()
        {
            font = new Font(font, FontStyle.Underline | font.Style);
        }
        
        public void ResetFont()
        {
            font = new Font(font, DefaultFont.Style);
            ChangeSize(StartSize);
        }

        public void ResetPosition()
        {
            Position = InitalPos;
        }

       public void MouseMove(Point location) {
            if(!isButton)
				return;

            if(Contains(location)) {
                if (animationArgs.HasFlag(ButtonAnimation.HoverUnderline))
                    Underline();

                ChangeSize(StartSize + 1);	
			} else {
                ResetFont();
            }
		}
		
        public void Move(int x, int y)
        {
            Position = new Point(Position.X + x, Position.Y + y);
        }

		public void MouseClick(Point location) {
			if(!isButton)
				return;
			
			if(Contains(location)) {
                DoClickAnimations();
                OnClick();	
			}
		}

        private void DoClickAnimations()
        {
            // Exclude just hover animations
            if (animationArgs == ButtonAnimation.HoverUnderline || animationArgs == ButtonAnimation.HoverShake)
                return;

            if (animationArgs == ButtonAnimation.None)
                return;

            if (animationArgs.HasFlag(ButtonAnimation.SlideOffLeft))
            {
                
                int bound = -((Bounds.X / 2) * 2);
                while (Position.X > bound)
                {
                    Move(-1, 0);
                    new Pause(Global.AnimationTime);
                }
                return;
            } else if (animationArgs.HasFlag(ButtonAnimation.SlideOffRight))
            {
                while (Position.X - (Bounds.X / 2) < Global.BoardWidth)
                {
                    Move(1, 0);
                    new Pause(Global.AnimationTime);
                }
                return;
            }
        }

		public Label SetButton(Action clickCallback, ButtonAnimation animation = ButtonAnimation.None)
		{
			isButton = true;
			OnClick = clickCallback;
            animationArgs = animation;

			return this;
		}

    }
}
