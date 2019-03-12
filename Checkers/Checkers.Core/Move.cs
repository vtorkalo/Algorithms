using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Core
{
    public class Move : List<Cell>
    {
        public int Kills { get; set; }
        public int NewKings { get; set; }
        public int KingKills { get; set; }
        public Move()
        {

        }
        public Move(IEnumerable<Cell> cells): base(cells)
        {

        }
    }
}
