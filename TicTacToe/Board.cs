using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    public class Board : List<List<int>>
    {
        public List<Move> Moves { get; set; } = new List<Move>();
        public int StartingPlayer { get; set; }


        public Board(int startingPlayer)
        {
            StartingPlayer = startingPlayer;
            for (int row = 0; row < 3; row++)
            {
                this.Add(new List<int>() { 0, 0, 0 });
            }

        }

        public void Play(int row, int col, int player)
        {
            this[row][col] = player;
            Moves.Add(new Move(row, col, player));
        }
        public void Play(Move move)
        {
            this[move.Row][move.Col] = move.Player;
            Moves.Add(move);
        }

        public void Print()
        {
            foreach (var row in this)
            {
                Console.WriteLine($" {XorO(row[0])} | {XorO(row[1])} | {XorO(row[2])}");
                if (row != this[2]) Console.WriteLine("-----------");
            }
        }

        private string XorO(int play)
        {
            if (play == 0) return " ";
            if (play == 1) return "X";
            if (play == -1) return "O";
            return "?";
        }

        /// <summary>
        /// Return 0 if cat game. 1 if player. -1 if computer. Null if no winner.
        /// </summary>
        /// <returns></returns>
        public int? Winner()
        {
            // Rows
            foreach (var row in this)
            {
                if (row.Sum(f => f) == 3) return 1;
                if (row.Sum(f => f) == -3) return -1;
            }

            // Columns
            for (int col = 0; col < 3; col++)
            {
                if (this[0][col] + this[1][col] + this[2][col] == 3) return 1;
                if (this[0][col] + this[1][col] + this[2][col] == -3) return -1;
            }

            // Diagonal 1
            if (this[0][0] + this[1][1] + this[2][2] == 3) return 1;
            if (this[0][0] + this[1][1] + this[2][2] == -3) return -1;

            // Diagonal 2
            if (this[0][2] + this[1][1] + this[2][0] == 3) return 1;
            if (this[0][2] + this[1][1] + this[2][0] == -3) return -1;

            // Cat game
            if (Moves.Count() == 9) return 0;

            // No winner.
            return null;
        }


        /// <summary>
        /// Returns true if the input board matches this board so far. 
        /// </summary>
        /// <param name="board">The in progress board</param>
        /// <returns></returns>
        public bool MatchBoard(Board board)
        {
            if (this.StartingPlayer != board.StartingPlayer) return false;

            for(int row = 0; row <3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row][col] != 0)
                    {
                        if (this[row][col] != board[row][col]) return false;
                    }
                }
            }

            return true;
        }

        public string WinnerText()
        {
            if (Winner() == 0) return "Cat Game";
            if (Winner() == -1) return "Comptuer Won";
            if (Winner() == 1) return "Player Won";
            return "No Winner Yet" ;
        }

        public bool IsValidMove(int row, int col)
        {
            if (row < 0 || row > 2) return false;
            if (col < 0 || col > 2) return false;
            return this[row][col] == 0;
        }
    }
}
