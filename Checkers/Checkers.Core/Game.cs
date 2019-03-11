using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Core
{
    public class Game : List<Move>
    {
        public Game()
        {

        }
        public Game(IEnumerable<Move> moves):base(moves)
        {

        }
    }
}
