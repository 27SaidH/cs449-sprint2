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
        public void ValidMove_Works()
        {
            var game = new SolitaireGame();
            game.StartNewGame(7, BoardType.English);

            game.Board.SetCell(3,1,CellState.Peg);
            game.Board.SetCell(3,2,CellState.Peg);
            game.Board.SetCell(3,3,CellState.Empty);

            Assert.True(game.MakeMove(3,1,3,3));
        }
    }
}
