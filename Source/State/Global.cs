using System.Drawing;

namespace NoughtsAndCrosses
{
	public static class Global
	{
		public const int BoardWidth  = 300;
		public const int BoardHeight = 300;
		public const int TileSize    = 100;
		
		public static int AIDifficulty = -1;
        public const double AnimationTime = 0.002;
		
		// Whoah, whoah! I can't believe i'm being so bad!
		public const int BoardCentre = Global.BoardWidth / 2;
        public static Point MousePos = new Point(0, 0);
	}
}

