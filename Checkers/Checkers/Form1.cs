using Checkers.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _field = Algorithms.GetInitialField();
            pnlField.Refresh();
        }

        private CellState[,] _field = Algorithms.GetInitialField();
        private Cell _selectedCell = null;
        private List<List<Cell>> _movements = new List<List<Cell>>();
        private List<Cell> _currentPath = new List<Cell>();
        int _currentPathIndex = 0;

        private void pnlField_Paint(object sender, PaintEventArgs e)
        {
            pnlField.Width = 800;
            pnlField.Height = 800;

            float cellSize = pnlField.Size.Height / 8;
            int width = pnlField.Width;
            int height = pnlField.Height;

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

                    float checkerSize =(float) (cellSize * 0.7);
                    if (state != CellState.Empty)
                    {
                        e.Graphics.FillEllipse(checkerColor, x * cellSize + (cellSize - checkerSize)/2, y * cellSize + (cellSize - checkerSize) / 2, checkerSize, checkerSize);

                        if (state == CellState.BlackKing || state == CellState.WhiteKing)
                        {
                            e.Graphics.DrawString("K", new Font("arial", 30), Brushes.Black, x * cellSize + 30, y * cellSize + 30);
                        }
                        
                    }
                }
            }

            var lineWidth = 3;
            if (_selectedCell != null)
            {
                var p = new Pen(Brushes.Green, lineWidth);
                e.Graphics.DrawRectangle(p, _selectedCell.Col * cellSize, _selectedCell.Row * cellSize, cellSize- lineWidth, cellSize- lineWidth);
            }

            foreach (var cell in _currentPath)
                {
                    var p = new Pen(Brushes.Red, lineWidth);
                    e.Graphics.DrawRectangle(p, cell.Col * cellSize, cell.Row * cellSize, cellSize - lineWidth, cellSize - lineWidth);
                    e.Graphics.DrawString(_currentPath.IndexOf(cell).ToString(), new Font(FontFamily.GenericSerif, 15),
                        Brushes.Black, cell.Col * cellSize, cell.Row * cellSize);
                e.Graphics.DrawString(cell.ToString(), new Font(FontFamily.GenericSerif, 15),
                        Brushes.Black, cell.Col * cellSize, cell.Row * cellSize+25);
            }
        }

        

        private void pnlField_MouseClick(object sender, MouseEventArgs e)
        {
            float cellSize = pnlField.Size.Height / 8;
            var cell = new Cell
            {
                Row = (byte)(e.Y / cellSize),
                Col = (byte)(e.X / cellSize)
            };
            //if (_field[cell.Row, cell.Col] != CellState.Empty)
            {
                _selectedCell = cell;
                pnlField.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetSelectedCellState(CellState.Black);
        }

        private void SetSelectedCellState(CellState state)
        {
            if (_selectedCell != null)
            {
                _field[_selectedCell.Row, _selectedCell.Col] = state;
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
            if (_selectedCell!= null)
            {
                _movements = Algorithms.GetPossibleMovements(_field, _selectedCell);
                pnlField.Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _field = new CellState[8, 8];
            pnlField.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _currentPath = _movements[_currentPathIndex];
            _currentPathIndex++;
            if (_currentPathIndex>=_movements.Count)
            {
                _currentPathIndex = 0;
            }
            pnlField.Refresh();
        }
    }
}
