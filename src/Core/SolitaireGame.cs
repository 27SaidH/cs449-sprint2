using cs449sprint2.Models;

namespace cs449sprint2.Core
{
    public class SolitaireGame
    {
        public Board Board { get; private set; }

        public void StartNewGame(int size, BoardType type)
        {
            Board = new Board(size, type);
        }

        public bool IsValidMove(int fr, int fc, int tr, int tc)
        {
            if (Board == null) return false;

            if (fr < 0 || fr >= Board.Size || fc < 0 || fc >= Board.Size ||
                tr < 0 || tr >= Board.Size || tc < 0 || tc >= Board.Size)
            {
                return false;
            }

            if (Board.GetCell(fr, fc) != CellState.Peg) return false;
            if (Board.GetCell(tr, tc) != CellState.Empty) return false;

            int dr = tr - fr;
            int dc = tc - fc;

            bool validDistance =
                (Math.Abs(dr) == 2 && dc == 0) ||
                (Math.Abs(dc) == 2 && dr == 0);

            if (!validDistance) return false;

            int mr = (fr + tr) / 2;
            int mc = (fc + tc) / 2;

            return Board.GetCell(mr, mc) == CellState.Peg;
        }

        public bool MakeMove(int fr, int fc, int tr, int tc)
        {
            if (!IsValidMove(fr, fc, tr, tc)) return false;

            int mr = (fr + tr) / 2;
            int mc = (fc + tc) / 2;

            Board.SetCell(fr, fc, CellState.Empty);
            Board.SetCell(mr, mc, CellState.Empty);
            Board.SetCell(tr, tc, CellState.Peg);

            return true;
        }

        public bool IsGameOver()
        {
            if (Board == null) return true;

            for (int r = 0; r < Board.Size; r++)
            {
                for (int c = 0; c < Board.Size; c++)
                {
                    if (Board.GetCell(r, c) != CellState.Peg) continue;

                    if (IsValidMove(r, c, r + 2, c)) return false;
                    if (IsValidMove(r, c, r - 2, c)) return false;
                    if (IsValidMove(r, c, r, c + 2)) return false;
                    if (IsValidMove(r, c, r, c - 2)) return false;
                }
            }

            return true;
        }
    }
}
