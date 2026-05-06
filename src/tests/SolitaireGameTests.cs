using Xunit;
using cs449sprint2.Core;
using cs449sprint2.Models;

namespace cs449sprint2.Tests
{
    public class SolitaireGameTests
    {
        [Fact]
        public void NewGame_CreatesBoard()
        {
            var game = new SolitaireGame();

            game.StartNewGame(7, BoardType.English);

            Assert.NotNull(game.Board);
        }

        [Fact]
        public void NewGame_HasCorrectSizeAndType()
        {
            var game = new SolitaireGame();

            game.StartNewGame(7, BoardType.English);

            Assert.Equal(7, game.Board.Size);
            Assert.Equal(BoardType.English, game.Board.Type);
        }

        [Fact]
        public void NewGame_CenterCellIsEmpty()
        {
            var game = new SolitaireGame();

            game.StartNewGame(7, BoardType.English);

            Assert.Equal(CellState.Empty, game.Board.GetCell(3, 3));
        }

        [Fact]
        public void ValidMove_Works()
        {
            var game = new SolitaireGame();
            game.StartNewGame(7, BoardType.English);

            game.Board.SetCell(3, 1, CellState.Peg);
            game.Board.SetCell(3, 2, CellState.Peg);
            game.Board.SetCell(3, 3, CellState.Empty);

            Assert.True(game.MakeMove(3, 1, 3, 3));
        }

        [Fact]
        public void MakeMove_UpdatesBoardCorrectly()
        {
            var game = new SolitaireGame();
            game.StartNewGame(7, BoardType.English);

            game.Board.SetCell(3, 1, CellState.Peg);
            game.Board.SetCell(3, 2, CellState.Peg);
            game.Board.SetCell(3, 3, CellState.Empty);

            game.MakeMove(3, 1, 3, 3);

            Assert.Equal(CellState.Empty, game.Board.GetCell(3, 1));
            Assert.Equal(CellState.Empty, game.Board.GetCell(3, 2));
            Assert.Equal(CellState.Peg, game.Board.GetCell(3, 3));
        }

        [Fact]
        public void InvalidMove_ReturnsFalse()
        {
            var game = new SolitaireGame();
            game.StartNewGame(7, BoardType.English);

            Assert.False(game.MakeMove(0, 0, 0, 1));
        }

        [Fact]
        public void InvalidMove_OutsideBoard_ReturnsFalse()
        {
            var game = new SolitaireGame();
            game.StartNewGame(7, BoardType.English);

            Assert.False(game.IsValidMove(0, 0, -2, 0));
        }

        [Fact]
        public void IsGameOver_DoesNotCrash()
        {
            var game = new SolitaireGame();
            game.StartNewGame(7, BoardType.English);

            bool result = game.IsGameOver();

            Assert.False(result);
        }
    }
}
