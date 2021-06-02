using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    //The type of value a cell in the game is currently at
    public enum MarkType
    {
        //The cell hasnt been clicked yet
        Free,
        //The cell is O
        Nought,
        //The cell is X
        Cross
    }
}
