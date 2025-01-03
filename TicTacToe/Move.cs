using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class Move
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Player { get; set; }

        public Move(int row, int col, int player)
        {
            Row = row;
            Col = col;
            Player = player;
        }

        public override bool Equals(object obj)
        {
            if (obj is Move)
            {
                var move = obj as Move;
                if (this.Row == move.Row && this.Col == move.Col && this.Player == move.Player) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        internal object Text()
        {
            return $"{Row},{Col}";
        }
    }
}
