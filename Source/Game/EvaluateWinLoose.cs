using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsAndCrosses
{
    public enum Winner {
        PlayerOne, PlayerTwo, Draw, Continue
    };
    
    public class EvaluateWinLoose
    {
        private Winner CheckDiagonal(GameBoard board, TileContainer3 diaganol)
        {
            var diagOneType = diaganol.AllOfSameType();
            if (diagOneType == board.PlayerType())
                return Winner.PlayerOne;
            else if (diagOneType == board.PlayerTwoType())
                return Winner.PlayerTwo;
            return Winner.Continue;
        }

        public Winner Evaluate(GameBoard board)
        {
            // Check rows
            for(int y = 0; y < board.TileBoardSize(); y++)
            {
                var row = board.GetRow(y);
                var rowType = row.AllOfSameType();
                if (rowType == board.PlayerType())
                    return Winner.PlayerOne;
                else if (rowType == board.PlayerTwoType())
                    return Winner.PlayerTwo;
            }

            // Check columns
            for(int x = 0; x < board.TileBoardSize(); x++)
            {
                var column = board.GetColumn(x);
                var type = column.AllOfSameType();
                if (type == board.PlayerType())
                    return Winner.PlayerOne;
                else if (type == board.PlayerTwoType())
                    return Winner.PlayerTwo;
            }

            // Check diags
            Winner diagOne = CheckDiagonal(board, board.GetDiagOne());
            Winner diagTwo = CheckDiagonal(board, board.GetDiagTwo());
            if (diagOne != Winner.Continue)
                return diagOne;

            if (diagTwo != Winner.Continue)
                return diagTwo;
            
            // Check draw condition
            bool gridFull = true;
            for(int y = 0; y < board.TileBoardSize(); y++)
            {
                for(int x = 0; x < board.TileBoardSize(); x++)
                {
                    if (!board.TileAt(x, y)) gridFull = false;
                }
            }

            if (gridFull)
                return Winner.Draw;

            return Winner.Continue;
        }
    }
}
