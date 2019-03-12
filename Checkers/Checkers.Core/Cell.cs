using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Core
{
    public class Cell
    {
        public int Row { get; private set; }
        public int Col { get; private set; }
        public bool Kill { get; set; }
        public bool IsBack { get; private set; }
        public bool KingKill { get; set;}

        public Cell(int row, int col, bool isBack = false, bool kill = false)
        {
            this.Row = row;
            this.Col = col;
            this.IsBack = isBack;
            this.Kill = kill;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Row, Col, Kill ? "+": string.Empty);
        }
    }
}
