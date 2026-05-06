using cs449sprint2.Core;
using cs449sprint2.Models;

namespace cs449sprint2
{
    public class MainForm : Form
    {
        private SolitaireGame game = new SolitaireGame();

        private ComboBox boardTypeBox;
        private NumericUpDown sizeBox;
        private Button newGameButton;
        private Label statusLabel;
        private TableLayoutPanel boardPanel;

        private int selectedRow = -1;
        private int selectedCol = -1;

        public MainForm()
        {
            Text = "Solitaire Sprint 2";
            Width = 700;
            Height = 750;

            InitializeControls();
        }

        private void InitializeControls()
        {
            Label sizeLabel = new Label
            {
                Text = "Board Size:",
                Left = 20,
                Top = 20,
                Width = 80
            };

            sizeBox = new NumericUpDown
            {
                Left = 100,
                Top = 18,
                Width = 60,
                Minimum = 5,
                Maximum = 9,
                Value = 7
            };

            Label typeLabel = new Label
            {
                Text = "Board Type:",
                Left = 180,
                Top = 20,
                Width = 80
            };

            boardTypeBox = new ComboBox
            {
                Left = 260,
                Top = 18,
                Width = 120,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            boardTypeBox.Items.Add(BoardType.English);
            boardTypeBox.Items.Add(BoardType.Hexagon);
            boardTypeBox.Items.Add(BoardType.Diamond);
            boardTypeBox.SelectedIndex = 0;

            newGameButton = new Button
            {
                Text = "New Game",
                Left = 400,
                Top = 16,
                Width = 100
            };

            newGameButton.Click += NewGameButton_Click;

            statusLabel = new Label
            {
                Text = "Choose size/type and click New Game.",
                Left = 20,
                Top = 60,
                Width = 600,
                Height = 30
            };

            boardPanel = new TableLayoutPanel
            {
                Left = 20,
                Top = 100,
                Width = 620,
                Height = 580
            };

            Controls.Add(sizeLabel);
            Controls.Add(sizeBox);
            Controls.Add(typeLabel);
            Controls.Add(boardTypeBox);
            Controls.Add(newGameButton);
            Controls.Add(statusLabel);
            Controls.Add(boardPanel);
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            int size = (int)sizeBox.Value;
            BoardType type = (BoardType)boardTypeBox.SelectedItem;

            game.StartNewGame(size, type);

            selectedRow = -1;
            selectedCol = -1;

            statusLabel.Text = $"New {type} game started with board size {size}.";
            DrawBoard();
        }

        private void DrawBoard()
        {
            boardPanel.Controls.Clear();
            boardPanel.RowStyles.Clear();
            boardPanel.ColumnStyles.Clear();

            int size = game.Board.Size;

            boardPanel.RowCount = size;
            boardPanel.ColumnCount = size;

            for (int i = 0; i < size; i++)
            {
                boardPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / size));
                boardPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / size));
            }

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    Button cellButton = new Button();
                    cellButton.Dock = DockStyle.Fill;
                    cellButton.Margin = new Padding(2);
                    cellButton.Tag = new Point(r, c);

                    CellState state = game.Board.GetCell(r, c);

                    if (state == CellState.Peg)
                    {
                        cellButton.Text = "●";
                    }
                    else if (state == CellState.Empty)
                    {
                        cellButton.Text = "";
                    }

                    cellButton.Click += CellButton_Click;

                    boardPanel.Controls.Add(cellButton, c, r);
                }
            }
        }

        private void CellButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            Point point = (Point)clickedButton.Tag;

            int row = point.X;
            int col = point.Y;

            if (selectedRow == -1 && selectedCol == -1)
            {
                if (game.Board.GetCell(row, col) == CellState.Peg)
                {
                    selectedRow = row;
                    selectedCol = col;
                    statusLabel.Text = $"Selected peg at ({row}, {col}). Now select an empty destination.";
                }

                return;
            }

            bool moveMade = game.MakeMove(selectedRow, selectedCol, row, col);

            if (moveMade)
            {
                statusLabel.Text = "Move made successfully.";

                if (game.IsGameOver())
                {
                    statusLabel.Text = "Game over. No valid moves left.";
                }
            }
            else
            {
                statusLabel.Text = "Invalid move. Try again.";
            }

            selectedRow = -1;
            selectedCol = -1;

            DrawBoard();
        }
    }
}
