using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsAndCrosses
{
    public class AIMove
    {
        public int X;
        public int Y;
        public int Score;

        public AIMove(int x, int y)
        {
            X = x;
            Y = y;
        }

        public AIMove(int score)
        {
            Score = score;
        }
    };

    public class AI
    {
        private static int depth = 0;

        public void Execute(GameBoard board)
        {
            AIMove bestMove = GetBestMove(ref board, board.PlayerTwoType());
            board.SetTile(bestMove.X, bestMove.Y, board.PlayerTwoType());
        }
        
        private AIMove CalculateScore(GameBoard board, Winner winner)
        {
            switch (winner)
            {
                case Winner.PlayerOne:
                    return new AIMove((10 - depth));
                case Winner.PlayerTwo:
                    return new AIMove((depth - 10));
                case Winner.Draw:
                    return new AIMove(0);
            }

            return null;
        }

        private AIMove GetBestMove(ref GameBoard board, ObjectType player)
        {
            EvaluateWinLoose winLoose = new EvaluateWinLoose();
            Winner winner = winLoose.Evaluate(board);

            if (winner != Winner.Continue)
                return CalculateScore(board, winner);

            depth += 1;

            List<AIMove> moves = new List<AIMove>();

            for(int y = 0; y < board.TileBoardSize(); y++)
            {
                for(int x = 0; x < board.TileBoardSize(); x++)
                {
                    // No tile at point, evaluate this tile
                    if(!board.TileAt(x, y))
                    {
                        AIMove move = new AIMove(x, y);
                        board.SetTile(x, y, player);

                        if(player == board.PlayerTwoType())
                        {
                            move.Score = GetBestMove(ref board, board.PlayerType()).Score;
                        }
                        else if(player == board.PlayerType())
                        {
                            move.Score = GetBestMove(ref board, board.PlayerTwoType()).Score;
                        }

                        moves.Add(move);

                        // Reset the tile
                        board.SetTile(x, y, ObjectType.Empty);
                    }
                }
            }

            // Sort the moves out, and find the best one.
            int bestMove = 0;
            if(player == board.PlayerTwoType())
            {
                int score = -1000000;
                for(int i = 0; i < moves.Count; i++)
                {
                    if(moves[i].Score > score)
                    {
                        bestMove = i;
                        score = moves[i].Score;
                    }
                }
            } else if(player == board.PlayerType())
            {
                int score = 1000000;
                for (int i = 0; i < moves.Count; i++)
                {
                    if (moves[i].Score < score)
                    {
                        bestMove = i;
                        score = moves[i].Score;
                    }
                }
            }

            return moves[bestMove];
        }
    }
}
