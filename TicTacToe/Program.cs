using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to Tic Tac Toe");
            Console.WriteLine("Play by entering a coordinate (row, col): 1,1; 2,3; 3,1; etc");
            Console.WriteLine();

            var playAgain = true;

            int trainingGames = 1000;
            Console.WriteLine($"Just a second, I am playing {trainingGames} games just to get some strategy down...");
            var boards = Opponent.GetSmart(trainingGames);
            var startingPlayer = 1;
            do
            {
                bool gameOver = false;
                Board board = new Board(startingPlayer);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("-- New Game --");
                Console.WriteLine();

                var player = startingPlayer;

                do
                {
                    if (player == 1)
                    {
                        board.Print();
                        var moveIsValid = false;
                        int row = -1, col = -1;
                        do
                        {
                            var rowcol = Console.ReadLine();
                            if (rowcol.StartsWith("q"))
                            {
                                Environment.Exit(0);
                            }
                            if (rowcol.Length == 3)
                            {
                                row = int.Parse(rowcol.Substring(0, 1));
                                col = int.Parse(rowcol.Substring(2, 1));
                                moveIsValid = board.IsValidMove(row - 1, col - 1);
                            }
                        } while (!moveIsValid);
                        board.Play(row - 1, col - 1, 1);
                    }
                    else
                    {
                        // Computer plays
                        Opponent.MakeMove(boards, board);
                    }
                    if (board.Winner() != null)
                    {
                        Console.WriteLine(board.WinnerText());
                        gameOver = true;
                    }
                    player = player * -1;
                } while (!gameOver);

                // Print final board;
                board.Print();

                // Save this game;
                if (board.Winner() != null)
                {
                    boards.Add(board);
                    if (board.Winner() == -1) Console.WriteLine("I'll remember that strategy.");
                    if (board.Winner() == 1) Console.WriteLine("I won't make that mistake again.");
                }
                startingPlayer = startingPlayer * -1;

            } while (playAgain);


        }
    }
}