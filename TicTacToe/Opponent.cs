using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    public static class Opponent
    {

        /// <summary>
        /// Returns true if moves. False if gives up.
        /// </summary>
        /// <param name="boards">List of all game boards.</param>
        /// <param name="board">Current Board</param>
        /// <returns></returns>
        public static bool MakeMove(List<Board> boards, Board board)
        {
            // Try to Win
            var move = SmartMove(board, -1, -1);
            if (move != null)
            {
                board.Play(move);
                return true;
            }

            // Try to Block
            move = SmartMove(board, 1, -1);
            if (move != null)
            {
                board.Play(move);
                return true;
            }

            // Go for a winning move.
            var winningBoards = boards.Where(f => f.MatchBoard(board) && f.Winner() == -1 && f.Moves.Count() > board.Moves.Count()).ToList();
            var losingBoards = boards.Where(f => f.MatchBoard(board) && f.Winner() == 1 && f.Moves.Count() > board.Moves.Count()).ToList();

            if (winningBoards.Count() > 0)
            {
                // Only pick the most winning next move
                var winStats = winningBoards.GroupBy(f => f.Moves[board.Moves.Count].Text());
                var loseStats = losingBoards.GroupBy(f => f.Moves[board.Moves.Count].Text());
                int mostWins = -1000000;
                Move bestMove = null;
                foreach (var winStat in winStats)
                {
                    int winCount = winStat.Count();
                    foreach (var loseStat in loseStats)
                    {
                        if (loseStat.Key.ToString() == winStat.Key.ToString())
                        {
                            winCount = winCount - loseStat.Count();
                            break;
                        }
                    }

                    if (winCount > mostWins)
                    {
                        mostWins = winStat.Count();
                        bestMove = winStat.First().Moves[board.Moves.Count];
                    }
                }

                //var rnd = new Random();
                //// Randomly choose the next move.
                //var targetBoard = winningBoards[rnd.Next(winningBoards.Count())];
                //var nextMove = targetBoard.Moves[board.Moves.Count];
                board.Play(bestMove);
                return true;
            }


            // Prevent losing moves.
            // Find the first non losing move
            var madeMove = false;
            var count = 0;
            do
            {
                move = GetNextMove(board, -1);
                var goodMove = true;
                // Check every losing board to see if this is a way to lose.
                foreach (var losingBoard in losingBoards)
                {
                    // This space is free. See if it is not a bad move.
                    var nextMove = losingBoard.Moves[board.Moves.Count];
                    if (nextMove.Row == move.Row && nextMove.Col == move.Col)
                    {
                        goodMove = false;
                        break;
                    }
                }
                if (goodMove)
                {
                    board.Play(move);
                    return true;
                }
                count++;
            } while (!madeMove && count < 100);
            move = GetNextMove(board, -1);
            board.Play(move);
            return true;
        }

        public static List<Board> GetSmart(int gameCount)
        {
            var boards = new List<Board>();
            var startingPlayer = 1;



            for (int x = 0; x < gameCount; x++)
            {

                Board board = new Board(startingPlayer);
                //Console.WriteLine("-- Getting Smarter by Playing a New Game --");
                var player = startingPlayer;

                var gameOver = false;
                do
                {
                    if (player == 1)
                    {
                        board.Play(GetNextMove(board, 1));
                    }
                    else
                    {
                        // Computer plays
                        Opponent.MakeMove(boards, board);
                    }
                    if (board.Winner() != null)
                    {
                        //Console.WriteLine(board.WinnerText());
                        gameOver = true;
                    }
                    player = player * -1;
                } while (!gameOver);

                // Save this game;
                if (board.Winner() != null)
                {
                    boards.Add(board);
                    //board.Print();
                    //if (board.Winner() == -1) Console.WriteLine("I'll remember that strategy.");
                    //if (board.Winner() == 1) Console.WriteLine("I won't make that mistake again.");
                }
                startingPlayer = startingPlayer * -1;
            }




            return boards;
        }

        private static Move GetNextMove(Board board, int player)
        {
            // Try to Win
            var move = SmartMove(board, player, player);
            if (move != null) return move;

            // Try to Block
            move = SmartMove(board, player * -1, player);
            if (move != null) return move;

            var rnd = new Random();
            do
            {
                int row = rnd.Next(3);
                int col = rnd.Next(3);
                if (board.IsValidMove(row, col)) return new Move(row, col, player);
            } while (true);

        }

        public static Move SmartMove(Board board, int testPlayer, int player)
        {
            // Rows
            for (int row = 0; row < 3; row++)
            {
                if (board[row][0] + board[row][1] + board[row][2] == 2 * testPlayer)
                {
                    // Play in the blank spot in the row.
                    if (board[row][0] == 0) return new Move(row, 0, player);
                    if (board[row][1] == 0) return new Move(row, 1, player);
                    return new Move(row, 2, player);
                }
            }

            // Columns
            for (int col = 0; col < 3; col++)
            {
                if (board[0][col] + board[1][col] + board[2][col] == 2 * testPlayer)
                {
                    if (board[0][col] == 0) return new Move(0, col, player);
                    if (board[1][col] == 0) return new Move(1, col, player);
                    return new Move(2, col, player);
                }
            }

            // Diagonal 1
            if (board[0][0] + board[1][1] + board[2][2] == 2 * testPlayer)
            {
                if (board[0][0] == 0) return new Move(0, 0, player);
                if (board[1][1] == 0) return new Move(1, 1, player);
                return new Move(2, 2, player);
            }

            // Diagonal 2
            if (board[0][2] + board[1][1] + board[2][0] == 2 * testPlayer)
            {
                if (board[0][2] == 0) return new Move(0, 2, player);
                if (board[1][1] == 0) return new Move(1, 1, player);
                return new Move(2, 0, player);
            }

            // No Smart Move.
            return null;
        }
    }
}
