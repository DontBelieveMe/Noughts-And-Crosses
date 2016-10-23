using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NoughtsAndCrosses
{
    public class Label
    {
        public static Font DefaultFont = new Font("Consolas", 10, FontStyle.Bold);
        public static Color DefaultColor = Color.DarkSlateGray;

        private Font font = DefaultFont;

        public string Text { get; set; }
        public Point Position { get; private set; }
        public int Size { get { return (int)font.Size; } }

        public int StartSize
        {
            get; private set;
        }
        
        private Brush brush;
        private StringAlignment alignment = StringAlignment.Center;

        public Rectangle Bounds;

        public Label(string text, int x, int y, Color color)
        {
            Text = text;
            Position = new Point(x, y);
            Bounds = new Rectangle(x, y, 0, 0);

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
    }
}
