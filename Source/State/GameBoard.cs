using System.Drawing;
using System.Media;

namespace NoughtsAndCrosses
{
    public class GameBoard : State
    {
        private enum WhooseTurn
        {
            PlayerOne, PlayerTwo
        };

        public Winner LastWinner = Winner.Continue;
        private WhooseTurn turn = WhooseTurn.PlayerOne;
        private ObjectType playerType = ObjectType.Cross;

        private int width, height;
        private const int tileSize = 100;
        private const int boardSize = 3;
        
        private Object[,] tiles = new Object[3, 3];
        private Cross indicator = new Cross(0, 0);

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

            string whooseTurn = (turn == WhooseTurn.PlayerOne) ? "Player One's turn (" + playerType + ")" : "Player Two's turn (" + PlayerTwoType() + ") Thinking...";
            g.DrawString(whooseTurn, Game.Font, Game.FontColor, 5, 320);

            indicator.Render(g);
        }

        private void RunAI()
        {
            turn = WhooseTurn.PlayerTwo;

            Pause pause = new Pause(1);
            AI ai = new AI();
            ai.Execute(this);

            turn = WhooseTurn.PlayerOne;
        }

        protected override void MouseMove(Point location)
        {
            // Don't handle GUI clicks.
            if (location.Y >= height)
                return;

            // Don't allow clicks if the AI is 'thinking'
            if (turn == WhooseTurn.PlayerTwo)
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

        protected override void OnClick(Point point)
        {
            // Don't handle GUI clicks.
            if (point.Y > height)
                return;

            // Don't allow clicks if the AI is 'thinking'
            if (turn == WhooseTurn.PlayerTwo)
                return;

            int snappedX = point.X / 100;
            int snappedY = point.Y / 100;

            if (TileAt(snappedX, snappedY))
                return;

            if(playerType == ObjectType.Cross)
                tiles[snappedX, snappedY] = new Cross(snappedX, snappedY);
            else if(playerType == ObjectType.Nought)
                tiles[snappedX, snappedY] = new Nought(snappedX, snappedY);

            SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.Place);
            soundPlayer.Play();

            if (SomebodyWon())
                return;

            RunAI();

            if (SomebodyWon())
                return;
        }
        
        private bool SomebodyWon()
        {
            EvaluateWinLoose winLoose = new EvaluateWinLoose();
            Winner result = winLoose.Evaluate(this);
            if (result != Winner.Continue)
            {
                Pause pause = new Pause(.5);
                GetState<GameOver>().Winner = result.ToString();
                GotoNextState();
                return true;
            }

            return false;
        }

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
