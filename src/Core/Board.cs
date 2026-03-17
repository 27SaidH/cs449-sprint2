using cs449sprint2.Models;

namespace cs449sprint2.Core
{
    public class Board
    {
        public int Size { get; }
        public BoardType Type { get; }
        private readonly CellState[,] _cells;

        public Board(int size, BoardType type)
        {
            Size = size;
            Type = type;
            _cells = new CellState[size, size];

            Initialize();
        }

        private void Initialize()
        {
            int center = Size / 2;

            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    _cells[r, c] = CellState.Peg;
                }
            }

            _cells[center, center] = CellState.Empty;
        }

        public CellState GetCell(int r, int c) => _cells[r, c];
        public void SetCell(int r, int c, CellState val) => _cells[r, c] = val;
    }
}
