using System.Drawing;

namespace NoughtsAndCrosses
{
    public enum ObjectType
    {
        Nought, Cross, Base, Empty, NA
    };

    public abstract class Object
    {
        public ObjectType Type = ObjectType.Base;

        public Object(int indexX, int indexY)
        {
            IndexX = indexX;
            IndexY = indexY;

            Opacity = 255;
        }

        public Object() { }

        protected int Size = 100;

        public int Opacity { get; set; }
        public Brush Brush { get; set; }

        public int IndexX = -1, IndexY = -1;
        public abstract void Render(Graphics g);
    }

    public class Cross : Object
    {
        public Cross(int indexX, int indexY)
            : base(indexX, indexY)
        {
            Type = ObjectType.Cross;
            Brush = Brushes.DarkSeaGreen;
        }

        public override void Render(Graphics g)
        {
            int brushWidth = 10;
            
            Pen pen = null;
            using (Pen tmpPen = new Pen(Brush))
            {
                pen = new Pen(Color.FromArgb(Opacity, tmpPen.Color), brushWidth);
            }

            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            
            g.DrawLine(pen, 
                new Point(IndexX * Size + brushWidth * 2, IndexY * Size + brushWidth * 2), 
                new Point((IndexX * Size) + Size - brushWidth*2, (IndexY * Size) + (Size - brushWidth*2)));

            g.DrawLine(pen, 
                new Point(IndexX * Size + brushWidth * 2, (IndexY * Size) + (Size - brushWidth*2)), 
                new Point((IndexX * Size) + (Size - brushWidth*2), IndexY * Size + brushWidth * 2));

            pen.Dispose();
        }
    }

    public class Nought : Object
    {
        public Nought(int indexX, int indexY)
            : base(indexX, indexY)
        {
            Type = ObjectType.Nought;
            Brush = Brushes.PaleVioletRed;
        }

        public override void Render(Graphics g)
        {
            int brushWidth = 10;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Pen pen = null;
            using (Pen tmpPen = new Pen(Brush))
            {
                pen = new Pen(Color.FromArgb(Opacity, tmpPen.Color), brushWidth);
            }

            g.DrawEllipse(pen, IndexX * Size + brushWidth, IndexY * Size + brushWidth, Size - brushWidth * 2 , Size - brushWidth * 2);
            pen.Dispose();
        }
    }

}
