using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Core
{
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool Kill { get; set; }
        public bool IsBack { get; set; }
        public bool IsVisited { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1}", Row, Col);
        }
    }
}
