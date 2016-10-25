using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace NoughtsAndCrosses
{
    public class GameBoard : State
    {
        protected enum WhooseTurn
        {
            PlayerOne, PlayerTwo
        };

        public Winner LastWinner = Winner.Continue;
        protected WhooseTurn turn = WhooseTurn.PlayerOne;
        protected ObjectType playerType = ObjectType.Cross;

        protected int width, height;
        protected const int tileSize = 100;
        protected const int boardSize = 3;

        protected Object[,] tiles = new Object[3, 3];
        protected Object indicator = new Cross(0, 0);

        public GameBoard(int width, int height): base()
        {
            this.width = width;
            this.height = height;

            indicator.Opacity = 200;
        }

        private void RenderGrid(Graphics g)
        {
            Brush brush = Brushes.LightSkyBlue;

            // Draw *most* of the grid
            for(int x = 0; x <= width; x += tileSize * 2)
            {
                for(int y = 0; y <= height; y += tileSize * 2)
                {
                    g.FillRectangle(brush, x, y, tileSize, tileSize);
                }
            }

            // Draw centre tile
            g.FillRectangle(brush, 100, 100, 100, 100);
        }

        protected virtual void RenderTurnString(Graphics g) { }

        protected override void Render(Graphics g)
        {
            RenderGrid(g);

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    var tile = tiles[x, y];
                    if (TileAt(x, y))
                        tile.Render(g);
                }
            }
            RenderTurnString(g);
            

            indicator.Render(g);
        }

        

        protected override void MouseMove(Point location)
        {
            // Don't handle GUI clicks.
            if (location.Y >= height)
                return;

            int snappedX = location.X / 100;
            int snappedY = location.Y / 100;
            
            if(TileAt(snappedX, snappedY))
            {
                for (int x = 0; x < 3; x++)
                    for (int y = 0; y < 3; y++)
                        if (TileAt(x, y) && tiles[x, y].Opacity != 255)
                            tiles[x, y].Opacity = 255;
                                
                tiles[snappedX, snappedY].Opacity = 100;
                
                indicator.Opacity = 0;
                return;
            }
            else
            {
                for (int x = 0; x < 3; x++)
                    for (int y = 0; y < 3; y++)
                        if(TileAt(x, y))
                            tiles[x, y].Opacity = 255;
                indicator.Opacity = 200;
            }

            indicator.IndexX = snappedX;
            indicator.IndexY = snappedY;
        }

        protected virtual bool SomebodyWon() { return false; }

        public Object         GetTile(int indexX, int indexY) { return tiles[indexX, indexY]; }
        public bool           TileAt(int indexX, int indexY)
        {
            if (indexX < 0 || indexX > 2 || indexY < 0 || indexY > 2)
                return false;
            return tiles[indexX, indexY] != null;
        }

        public int            TileSize() { return tileSize; }

        public void           SetTile(int indexX, int indexY, ObjectType type)
        {
            if(type == ObjectType.Cross)
            {
                tiles[indexX, indexY] = new Cross(indexX, indexY);
            } else if(type == ObjectType.Nought)
            {
                tiles[indexX, indexY] = new Nought(indexX, indexY);
            } else if(type == ObjectType.Empty)
            {
                tiles[indexX, indexY] = null;
            }
        }

        public int            TileBoardSize() { return boardSize; }

        public ObjectType     TileType(int indexX, int indexY)
        {
            if (!TileAt(indexX, indexY))
                return ObjectType.Empty;

            return GetTile(indexX, indexY).Type;
        }

        public ObjectType     PlayerType() { return playerType; }

        public ObjectType     PlayerTwoType()
        {
            if (playerType == ObjectType.Cross)
                return ObjectType.Nought;
            else if (playerType == ObjectType.Nought)
                return ObjectType.Cross;
            return ObjectType.NA;
        }

        public TileContainer3 GetRow(int yIndex)
        {
            return new TileContainer3(tiles[0, yIndex], tiles[1, yIndex], tiles[2, yIndex]);
        }
        
        public TileContainer3 GetColumn(int xIndex)
        {
            return new TileContainer3(tiles[xIndex, 0], tiles[xIndex, 1], tiles[xIndex, 2]);
        }

        public TileContainer3 GetDiagOne()
        {
            return new TileContainer3(tiles[0, 0], tiles[1, 1], tiles[2, 2]);
        }

        public TileContainer3 GetDiagTwo()
        {
            return new TileContainer3(tiles[0, 2], tiles[1, 1], tiles[2, 0]);
        }

        protected override void Reset()
        {
            tiles = new Object[3, 3];
        }
    }
}
