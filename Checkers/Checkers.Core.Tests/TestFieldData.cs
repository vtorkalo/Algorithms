using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Core.Tests
{
    public static class TestFieldData
    {
        public static CellState[] King_Moves_Case1()
        {
            var result = Helpers.GetEmptyField();

            Field.SetValue(result, 3, 4, CellState.WhiteKing);
            Field.SetValue(result, 4, 5, CellState.Black);
            Field.SetValue(result, 4, 3, CellState.Black);
            Field.SetValue(result, 6, 3, CellState.Black);
            Field.SetValue(result, 6, 5, CellState.Black);
            Field.SetValue(result, 1, 6, CellState.Black);

            return result;
        }

        public static CellState[] King_Moves_Case2()
        {
            var result = Helpers.GetEmptyField();

            Field.SetValue(result, 3, 4, CellState.WhiteKing);
            Field.SetValue(result, 4, 5, CellState.Black);
            Field.SetValue(result, 4, 3, CellState.Black);
            Field.SetValue(result, 6, 3, CellState.Black);
            Field.SetValue(result, 6, 5, CellState.Black);
            Field.SetValue(result, 1, 6, CellState.Black);

            return result;
        }

        public static CellState[] King_Moves_Case3()
        {
            var result = Helpers.GetEmptyField();

            Field.SetValue(result, 3, 4, CellState.WhiteKing);
            Field.SetValue(result, 4, 5, CellState.Black);
            Field.SetValue(result, 6, 5, CellState.Black);
            Field.SetValue(result, 5, 2, CellState.Black);
            Field.SetValue(result, 2, 1, CellState.Black);

            return result;
        }

        public static CellState[] King_Moves_Case4()
        {
            var result = Helpers.GetEmptyField();

            Field.SetValue(result, 7, 0, CellState.WhiteKing);
            Field.SetValue(result, 6, 1, CellState.Black);
            Field.SetValue(result, 3, 2, CellState.Black);
            Field.SetValue(result, 2, 3, CellState.Black);
            Field.SetValue(result, 5, 4, CellState.Black);

            return result;
        }


        public static CellState[] King_Moves_Case5()
        {
            var result = Helpers.GetEmptyField();
            Field.SetValue(result, 4, 3, CellState.WhiteKing);

            //selectedCell 4 3

            return result;
        }

        public static CellState[] King_Moves_Case6()
        {
            var result = Helpers.GetEmptyField();
            Field.SetValue(result, 1, 6, CellState.Black);
            Field.SetValue(result, 2, 3, CellState.White);
            Field.SetValue(result, 3, 4, CellState.BlackKing);
            Field.SetValue(result, 4, 5, CellState.White);
            Field.SetValue(result, 5, 2, CellState.White);
            Field.SetValue(result, 6, 1, CellState.Black);
            Field.SetValue(result, 6, 7, CellState.White);

            //selectedCell 3 4

            return result;
        }


        public static CellState[] Standart_Moves_Case1()
        {
            var result = Helpers.GetEmptyField();
            Field.SetValue(result, 0, 7, CellState.Black);
            Field.SetValue(result, 1, 4, CellState.Black);
            Field.SetValue(result, 1, 6, CellState.Black);
            Field.SetValue(result, 3, 0, CellState.Black);
            Field.SetValue(result, 3, 4, CellState.Black);
            Field.SetValue(result, 3, 6, CellState.Black);
            Field.SetValue(result, 5, 2, CellState.Black);
            Field.SetValue(result, 5, 4, CellState.Black);
            Field.SetValue(result, 5, 6, CellState.Black);
            Field.SetValue(result, 6, 3, CellState.White);
            Field.SetValue(result, 7, 6, CellState.White);
            //selectedCell 6 3

            return result;
        }


        public static CellState[] Standart_Moves_Case2()
        {
            var result = Helpers.GetEmptyField();
            Field.SetValue(result, 7, 2, CellState.Black);
            //selectedCell 7 2

            return result;
        }

        public static CellState[] Standart_Moves_Case3()
        {
            var result = Helpers.GetEmptyField();
            Field.SetValue(result, 5, 4, CellState.White);

            //selectedCell 5 4

            return result;
        }

        public static CellState[] Standart_Moves_Case4()
        {
            var result = Helpers.GetEmptyField();
            Field.SetValue(result, 3, 4, CellState.White);
            Field.SetValue(result, 4, 3, CellState.Black);
            //selectedCell 4 3

            return result;
        }

        public static CellState[] Standart_Moves_Case()
        {
            var result = Helpers.GetEmptyField();



            return result;
        }

        public static CellState[] Standart_Moves_Case5()
        {
            var result = Helpers.GetEmptyField();

            Field.SetValue(result, 2, 1, CellState.White);
            Field.SetValue(result, 2, 3, CellState.White);
            Field.SetValue(result, 2, 5, CellState.White);
            Field.SetValue(result, 4, 1, CellState.White);
            Field.SetValue(result, 4, 3, CellState.White);
            Field.SetValue(result, 4, 5, CellState.White);
            Field.SetValue(result, 6, 1, CellState.White);
            Field.SetValue(result, 6, 3, CellState.White);
            Field.SetValue(result, 6, 5, CellState.White);
            Field.SetValue(result, 7, 0, CellState.Black);

            //selectedCell 7 0

            return result;
        }


        public static CellState[] King_Moves_Case7()
        {
            var result = Helpers.GetEmptyField();

            Field.SetValue(result, 2, 1, CellState.White);
            Field.SetValue(result, 2, 3, CellState.White);
            Field.SetValue(result, 2, 5, CellState.White);
            Field.SetValue(result, 4, 1, CellState.White);
            Field.SetValue(result, 4, 3, CellState.White);
            Field.SetValue(result, 4, 5, CellState.White);
            Field.SetValue(result, 6, 1, CellState.White);
            Field.SetValue(result, 6, 3, CellState.White);
            Field.SetValue(result, 6, 5, CellState.White);
            Field.SetValue(result, 7, 0, CellState.BlackKing);

            //selectedCell 7 0

            return result;
        }

        public static CellState[] Standart_Moves_Case6()
        {
            var result = Helpers.GetEmptyField();
            Field.SetValue(result, 0, 1, CellState.Black);
            Field.SetValue(result, 0, 3, CellState.Black);
            Field.SetValue(result, 0, 5, CellState.Black);
            Field.SetValue(result, 0, 7, CellState.Black);
            Field.SetValue(result, 1, 0, CellState.Black);
            Field.SetValue(result, 1, 2, CellState.Black);
            Field.SetValue(result, 1, 4, CellState.Black);
            Field.SetValue(result, 1, 6, CellState.Black);
            Field.SetValue(result, 2, 1, CellState.Black);
            Field.SetValue(result, 2, 3, CellState.Black);
            Field.SetValue(result, 2, 5, CellState.Black);
            Field.SetValue(result, 2, 7, CellState.Black);
            Field.SetValue(result, 3, 2, CellState.White);
            Field.SetValue(result, 3, 4, CellState.White);
            Field.SetValue(result, 5, 0, CellState.White);
            Field.SetValue(result, 5, 4, CellState.White);
            Field.SetValue(result, 5, 6, CellState.White);
            Field.SetValue(result, 6, 1, CellState.White);
            Field.SetValue(result, 6, 5, CellState.White);
            Field.SetValue(result, 6, 7, CellState.White);
            Field.SetValue(result, 7, 0, CellState.White);
            Field.SetValue(result, 7, 2, CellState.White);
            Field.SetValue(result, 7, 4, CellState.White);
            Field.SetValue(result, 7, 6, CellState.White);
            // selectedCell 2 3

            return result;
        }


        public static CellState[] King_Moves_Case8()
        {
            var result = Helpers.GetEmptyField();
            Field.SetValue(result, 2, 1, CellState.White);

            Field.SetValue(result, 2, 3, CellState.White);

            Field.SetValue(result, 2, 5, CellState.White);

            Field.SetValue(result, 4, 1, CellState.White);

            Field.SetValue(result, 4, 3, CellState.White);

            Field.SetValue(result, 4, 5, CellState.White);

            Field.SetValue(result, 5, 2, CellState.BlackKing);

            Field.SetValue(result, 6, 1, CellState.White);

            Field.SetValue(result, 6, 3, CellState.White);

            Field.SetValue(result, 6, 5, CellState.White);
            //selectedCell 5 2


            return result;
        }


    }
}
