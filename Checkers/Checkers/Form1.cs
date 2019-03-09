using Checkers.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Checkers
{
    public partial class fmCheckersMain : Form
    {
        public fmCheckersMain()
        {
            InitializeComponent();
            UpdateUiState();
        }
        const int cellSize = 60;
        private PathGenerator _pathGenerator = new PathGenerator();
        private CheckersAlgorithm _checkersAlgorithm = new CheckersAlgorithm();

        private void btnNewWhite_Click(object sender, EventArgs e)
        {
            StartGame(Side.White);
        }

        private void btnNewBlack_Click(object sender, EventArgs e)
        {
            StartGame(Side.Black);
        }

        private void StartGame(Side side)
        {
            _currentSide = side;
            _movements?.Clear();
            _aiMove?.Clear();
            _startCell = null;

            _gameStarted = true;
            _field = Helpers.GetInitialField();
            if (_currentSide == Side.Black)
            {
                MakeAiMove();
            }
            GetAvaliableCells();
            UpdateUiState();

            pnlField.Refresh();
        }

        private void UpdateUiState()
        {
            pnlField.Enabled = _gameStarted;
        }

        private CellState[,] _field = Helpers.GetInitialField();
        private Cell _startCell = null;
        private List<Move> _movements = new List<Move>();
        private CellComparer _cellComparer = new CellComparer();

        private Move _aiMove = null;
        private Side _currentSide = Side.White;

        private List<Cell> _avaliableCells = new List<Cell>();
        private bool _gameStarted = false;

        private void pnlField_Paint(object sender, PaintEventArgs e)
        {
            pnlField.Width = cellSize * 8;
            pnlField.Height = cellSize * 8;

            RenderField(e);

            if (_gameStarted)
            {
                var lineWidth = 2;
                if (_aiMove != null)
                {
                    RenderPath(e, lineWidth, _aiMove, Brushes.Red);
                }
                if (_avaliableCells != null && _avaliableCells.Any())
                {
                    RenderPath(e, 1, _avaliableCells, Brushes.Blue);
                }

                if (_startCell != null)
                {
                    int row = GetRow(_startCell.Row);
                    int col = GetCol(_startCell.Col);

                    var possibleTargets = _movements.Select(m => m.Last());
                    foreach (var path in _movements)
                    {
                        RenderPath(e, lineWidth, path, Brushes.Red);
                    }

                    var p = new Pen(Brushes.Green, lineWidth);
                    e.Graphics.DrawRectangle(p,
                        col * cellSize,
                        row * cellSize,
                        cellSize - lineWidth,
                        cellSize - lineWidth);
                }
            }
        }

        private int GetRow(int row)
        {
            return _currentSide == Side.White ? row : 7 - row;
        }

        private int GetCol(int col)
        {
            return _currentSide == Side.White ? col : 7 - col;
        }

        private void RenderPath(PaintEventArgs e, int lineWidth, List<Cell> path, Brush brush)
        {
            foreach (var cell in path)
            {
                int row = GetRow(cell.Row);
                int col = GetCol(cell.Col);

                var p = new Pen(brush, lineWidth);
                e.Graphics.DrawRectangle(p,
                    col * cellSize,
                    row * cellSize,
                    cellSize - lineWidth,
                    cellSize - lineWidth);        
            }
        }

        private void RenderField(PaintEventArgs e)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    int row = GetRow(y);
                    int col = GetCol(x);
                    
                    int color = (x + (y + 1 % 2)) % 2;
                    var brush = color == 0 ? Brushes.LightGray : Brushes.White;
                    e.Graphics.FillRectangle(brush, x * cellSize, y * cellSize, cellSize, cellSize);

                    CellState state = _field[row, col];
                    Brush checkerColor = Brushes.White;
                    switch (state)
                    {
                        case CellState.White:
                        case CellState.WhiteKing:
                            checkerColor = Brushes.White;
                            break;

                        case CellState.Black:
                        case CellState.BlackKing:
                            checkerColor = Brushes.DarkGray;
                            break;

                    }

                    float checkerSize = (float)(cellSize * 0.7);
                    if (state != CellState.Empty)
                    {
                        e.Graphics.FillEllipse(checkerColor,
                            x * cellSize + (cellSize - checkerSize) / 2,
                            y * cellSize + (cellSize - checkerSize) / 2,
                            checkerSize,
                            checkerSize);

                        if (state == CellState.BlackKing || state == CellState.WhiteKing)
                        {
                            e.Graphics.DrawString("K", new Font("arial", cellSize * 0.2f),
                                Brushes.Black,
                                x * cellSize + cellSize * 0.35f,
                                y * cellSize + cellSize * 0.35f);
                        }

                    }

                    if (row % 2 != col % 2)
                    {
                        e.Graphics.DrawString(string.Format("{0} {1}", col, row),
                             new Font(FontFamily.GenericSerif, cellSize * 0.15f),
                             Brushes.DarkGray,
                             x * cellSize,
                             y * cellSize + cellSize * 0.7f);
                    }
                }
            }
        }

        private void GetAvaliableCells()
        {
            if (_gameStarted)
            {
                _avaliableCells = _checkersAlgorithm.GetAvaliableCells(_field, _currentSide);
                if (!_avaliableCells.Any())
                {
                    _gameStarted = false;
                    UpdateUiState();
                    MessageBox.Show("You loose!");
                }
            }
        }

        private void pnlField_MouseClick(object sender, MouseEventArgs e)
        {

            var cell = new Cell(GetRow(e.Y / cellSize), GetCol(e.X / cellSize));

            if (cell.Row % 2 != cell.Col % 2) 
            {

                if (_startCell == null && _avaliableCells.Contains(cell, _cellComparer))
                {
                    SetStartCell(cell);
                }
                else
                {
                    if (_movements.Select(m => m.Last()).Contains(cell, _cellComparer))
                    {
                        _startCell = null;
                        var path = _movements.Single(m => Helpers.CompareCells(m.Last(), cell));
                        _avaliableCells.Clear();
                        Helpers.MakeMove(_field, path);
                        this.Refresh();
                        _movements.Clear();
                        MakeAiMove();
                        GetAvaliableCells();
                        this.Refresh();
                    }
                    else if (_avaliableCells.Contains(cell, _cellComparer))
                    { // clicked in not allowed place - refresh start cell
                        SetStartCell(cell);
                    }
                }
                

                pnlField.Refresh();
            }
            GetAvaliableCells();
        }

        private void MakeAiMove()
        {
            var aiSide = Helpers.GetOppositeSide(_currentSide);
            _aiMove = _checkersAlgorithm.GetNextMove(_field, aiSide);
            if (_aiMove != null)
            {
                Helpers.MakeMove(_field, _aiMove);
            }
            else
            {
                MessageBox.Show("You win!");
                _gameStarted = false;
            }
        }

        private void SetStartCell(Cell cell)
        {
            _startCell = cell;
            _aiMove = null;
            _movements = _pathGenerator.GetPossibleMovements(_field, _startCell);
        }

     
    }
}
