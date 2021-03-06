﻿using Checkers.Core;
using Checkers.Core.Tests;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const int cellSize = 60;
        private StandartPathGenerator _standartPathGenerator = new StandartPathGenerator();
        private KingPathGenerator _kingPathGenerator = new KingPathGenerator();

        private void button1_Click(object sender, EventArgs e)
        {
            _field = TestFieldData.Standart_Moves_Case6();
            pnlField.Refresh();
        }

        private CellState[] _field = Helpers.GetInitialField();
        private Cell _selectedCell = null;
        private List<Move> _movements = new List<Move>();
        private Move _currentPath = new Move();
        int _currentPathIndex = 0;

        private void pnlField_Paint(object sender, PaintEventArgs e)
        {
            pnlField.Width = cellSize * 8;
            pnlField.Height = cellSize * 8;

            int width = pnlField.Width;
            int height = pnlField.Height;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    int color = (x + (y + 1 % 2)) % 2;
                    var brush = color == 0 ? Brushes.LightGray : Brushes.White;
                    e.Graphics.FillRectangle(brush, x * cellSize, y * cellSize, cellSize, cellSize);

                    CellState state = Field.GetValue(_field,y, x);
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
                             y * cellSize+cellSize*0.7f);
                    }
                }
            }

            var lineWidth = 3;
            foreach (var cell in _currentPath)
                {
                    var p = new Pen(Brushes.Red, lineWidth);
                    e.Graphics.DrawRectangle(p,
                        cell.Col * cellSize, 
                        cell.Row * cellSize,
                        cellSize - lineWidth,
                        cellSize - lineWidth);

                var cellIndex = _currentPath.IndexOf(cell);
                int yshift = 0;
                if (_currentPath.Take(cellIndex).Contains(cell, new CellComparer()))
                {
                    yshift = 15;
                }

                    e.Graphics.DrawString(cellIndex.ToString(),
                        new Font(FontFamily.GenericSerif, cellSize * 0.2f),
                        Brushes.Black,
                        cell.Col * cellSize,
                        cell.Row * cellSize + yshift);                
            }

            if (_selectedCell != null)
            {
                var p = new Pen(Brushes.Green, lineWidth);
                e.Graphics.DrawRectangle(p, _selectedCell.Col * cellSize, _selectedCell.Row * cellSize, cellSize - lineWidth, cellSize - lineWidth);
            }
        }

        private void pnlField_MouseClick(object sender, MouseEventArgs e)
        {
            var cell = new Cell
            (
                e.Y / cellSize,
                e.X / cellSize
            );
            _selectedCell = cell;
            pnlField.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetSelectedCellState(CellState.Black);
        }

        private void SetSelectedCellState(CellState state)
        {
            if (_selectedCell != null)
            {
                Field.SetValue(_field, _selectedCell.Row, _selectedCell.Col, state);
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
                var state = Helpers.GetCellState(_field, _selectedCell);
                if (state == CellState.White || state == CellState.Black)
                {
                    _movements = _standartPathGenerator.GetPossibleMovements(_field, _selectedCell);
                }
                if (state == CellState.WhiteKing || state == CellState.BlackKing)
                {
                    _movements = _kingPathGenerator.GetPossibleMovements(_field, _selectedCell);
                }
                _currentPathIndex = 0;
                pnlField.Refresh();
                button4_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_movements.Any())
            {
                _currentPath = _movements[_currentPathIndex];
                button4.Text = _movements.IndexOf(_currentPath).ToString();
                _currentPathIndex++;
                if (_currentPathIndex >= _movements.Count)
                {
                    _currentPathIndex = 0;
                }
                pnlField.Refresh();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("new List<Move>");
            builder.Append("{");
            foreach (var path in _movements)
            {
                builder.Append("new Move{");
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
                    var value = Field.GetValue(_field, row, col);
                    if (value != CellState.Empty)
                    {
                        builder.AppendLine(string.Format("result[{0}, {1}] = CellState.{2};", row, col, value.ToString()));
                    }
                }
            if (_selectedCell != null)
            {
                builder.AppendLine(string.Format("//selectedCell {0} {1}", _selectedCell.Row, _selectedCell.Col));
            }
            MessageBox.Show(builder.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
