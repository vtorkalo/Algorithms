using Checkers.Core;
using Checkers.Core.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class fmCheckersMain : Form
    {
        public fmCheckersMain()
        {
            InitializeComponent();
        }
        const int cellSize = 60;
        private PathGenerator _pathGenerator = new PathGenerator();
        private CheckersAlgorithm _checkersAlgorithm = new CheckersAlgorithm();

        private void button1_Click(object sender, EventArgs e)
        {
            _field = Helpers.GetInitialField();
            pnlField.Refresh();
        }

        private CellState[,] _field = Helpers.GetInitialField();
        private Cell _startCell = null;
        private List<List<Cell>> _movements = new List<List<Cell>>();
        int _currentPathIndex = 0;
        private CellComparer _cellComparer = new CellComparer();

        private List<Cell> _aiMove = null;

        private void pnlField_Paint(object sender, PaintEventArgs e)
        {
            pnlField.Width = cellSize * 8;
            pnlField.Height = cellSize * 8;

            int width = pnlField.Width;
            int height = pnlField.Height;
            RenderField(e);


            //var lineWidth = 3;
            //foreach (var cell in _currentPath)
            //{
            //    var p = new Pen(Brushes.Red, lineWidth);
            //    e.Graphics.DrawRectangle(p,
            //        cell.Col * cellSize,
            //        cell.Row * cellSize,
            //        cellSize - lineWidth,
            //        cellSize - lineWidth);

            //    var cellIndex = _currentPath.IndexOf(cell);
            //    int yshift = 0;
            //    if (_currentPath.Take(cellIndex).Contains(cell, new CellComparer()))
            //    {
            //        yshift = 15;
            //    }

            //    e.Graphics.DrawString(cellIndex.ToString(),
            //        new Font(FontFamily.GenericSerif, cellSize * 0.2f),
            //        Brushes.Black,
            //        cell.Col * cellSize,
            //        cell.Row * cellSize + yshift);
            //}


            var lineWidth = 3;


            if (_aiMove != null)
            {
                RenderPath(e, lineWidth, _aiMove);
            }

            if (_startCell != null)
            {
                var possibleTargets = _movements.Select(m => m.Last());
                foreach (var path in _movements)
                {
                    RenderPath(e, lineWidth, path);
                }
            }

            if (_startCell != null)
            {
                var p = new Pen(Brushes.Green, lineWidth);
                e.Graphics.DrawRectangle(p, _startCell.Col * cellSize, _startCell.Row * cellSize, cellSize - lineWidth, cellSize - lineWidth);
            }
        }

        private static void RenderPath(PaintEventArgs e, int lineWidth, List<Cell> path)
        {
            foreach (var cell in path)
            {
                var p = new Pen(Brushes.Red, lineWidth);
                e.Graphics.DrawRectangle(p,
                    cell.Col * cellSize,
                    cell.Row * cellSize,
                    cellSize - lineWidth,
                    cellSize - lineWidth);

                var cellIndex = path.IndexOf(cell);
                e.Graphics.DrawString(cellIndex.ToString(),
                    new Font(FontFamily.GenericSerif, cellSize * 0.2f),
                    Brushes.Black,
                    cell.Col * cellSize,
                    cell.Row * cellSize);
            }
        }

        private void RenderField(PaintEventArgs e)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    int color = (x + (y + 1 % 2)) % 2;
                    var brush = color == 0 ? Brushes.LightGray : Brushes.White;
                    e.Graphics.FillRectangle(brush, x * cellSize, y * cellSize, cellSize, cellSize);

                    CellState state = _field[y, x];
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
                        e.Graphics.FillEllipse(checkerColor, x * cellSize + (cellSize - checkerSize) / 2, y * cellSize + (cellSize - checkerSize) / 2, checkerSize, checkerSize);

                        if (state == CellState.BlackKing || state == CellState.WhiteKing)
                        {
                            e.Graphics.DrawString("K", new Font("arial", cellSize * 0.2f),
                                Brushes.Black,
                                x * cellSize + cellSize * 0.35f,
                                y * cellSize + cellSize * 0.35f);
                        }

                    }

                    if (y % 2 != x % 2)
                    {
                        e.Graphics.DrawString(string.Format("{0} {1}", y, x),
                             new Font(FontFamily.GenericSerif, cellSize * 0.15f),
                             Brushes.DarkGray,
                             x * cellSize,
                             y * cellSize + cellSize * 0.7f);
                    }
                }
            }
        }

        private void pnlField_MouseClick(object sender, MouseEventArgs e)
        {
            var cell = new Cell
            (
                e.Y / cellSize,
                e.X / cellSize
            );
            if (cell.Row % 2 != cell.Col % 2)
            {

                if (_startCell == null)
                {
                    _startCell = cell;
                    _aiMove = null;
                    _movements = _pathGenerator.GetPossibleMovements(_field, _startCell);
                }
                else
                {
                    if (_movements.Select(m => m.Last()).Contains(cell, _cellComparer))
                    {
                        _startCell = null;
                        var path = _movements.Single(m => Helpers.CompareCells(m.Last(), cell)).ToList();
                        Helpers.MakeMove(_field, path);
                        _movements.Clear();
                        this.Refresh();
                        Thread.Sleep(1000);

                        _aiMove = _checkersAlgorithm.GetNextMove(_field, Side.Black);
                        if (_aiMove != null)
                        {
                            Helpers.MakeMove(_field, _aiMove);
                        }
                    }
                    else
                    { // clicked in not allowed place - refresh start cell
                        _startCell = cell;
                        _aiMove = null;
                        _movements = _pathGenerator.GetPossibleMovements(_field, _startCell);
                    }
                }

                pnlField.Refresh();
            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            SetSelectedCellState(CellState.Black);
        }

        private void SetSelectedCellState(CellState state)
        {
            if (_startCell != null)
            {
                _field[_startCell.Row, _startCell.Col] = state;
                pnlField.Refresh();
            }
        }

        private void btnSetBlackKing_Click(object sender, EventArgs e)
        {
            SetSelectedCellState(CellState.BlackKing);
        }

        private void btnSetWhite_Click(object sender, EventArgs e)
        {
            SetSelectedCellState(CellState.White);
        }

        private void btnSetWhiteKing_Click(object sender, EventArgs e)
        {
            SetSelectedCellState(CellState.WhiteKing);
        }

        private void btnSetEmpty_Click(object sender, EventArgs e)
        {
            SetSelectedCellState(CellState.Empty);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (_startCell!= null)
            {
                _movements = _pathGenerator.GetPossibleMovements(_field, _startCell);
                _currentPathIndex = 0;
                pnlField.Refresh();
                button4_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _field = new CellState[8, 8];
            pnlField.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("new List<List<Cell>>");
            builder.Append("{");
            foreach (var path in _movements)
            {
                builder.Append("new List<Cell>{");
                foreach (var cell in path)
                {
                    builder.Append(string.Format("new Cell({0}, {1})", cell.Row, cell.Col));
                    if (cell != path.Last())
                    {
                        builder.Append(",");
                    }
                }
                builder.Append("}");
                if (path != _movements.Last())
                {
                    builder.Append(",");
                }
            }
            builder.Append("};");
            MessageBox.Show(builder.ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            for (int row =0; row<8; row++)
                for (int col = 0; col < 8; col++)
                {
                    if (_field[row, col] != CellState.Empty)
                    {
                        builder.AppendLine(string.Format("result[{0}, {1}] = CellState.{2};", row, col, _field[row, col].ToString()));
                    }
                }
            if (_startCell != null)
            {
                builder.AppendLine(string.Format("selectedCell {0} {1}", _startCell.Row, _startCell.Col));
            }
            MessageBox.Show(builder.ToString());
        }
    }
}
