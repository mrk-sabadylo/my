using System;
using System.Collections.Generic;
using System.Linq;

namespace renju
{
    public interface IGame
    {
        void PrintBoard();
        bool MakeMove(int row, int col);
        bool CheckWinner(int row, int col);
        bool IsBoardFull();
        char CurrentPlayer { get; }
    }

    public class Renju : IGame
    {
        public const char EmptyCell = '-';
        private readonly int _winLength;
        private readonly Dictionary<Tuple<int, int>, char> _board;

        public char BoardAt(int x, int y)
        {
            bool exists = _board.TryGetValue(Tuple.Create(x, y), out char value);
            return exists ? value : throw new Exception();
        }

        public Renju(int size = 16, int winLength = 5)
        {
            this.Size = size;
            this._winLength = winLength;
            _board = new Dictionary<Tuple<int, int>, char>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _board.Add(Tuple.Create(i, j), EmptyCell);
                }
            }

            CurrentPlayer = 'X';
        }

        public void PrintBoard()
        {
            Console.WriteLine("    " + string.Join("   ", Enumerable.Range(0, Size )));

            for (int i = 0; i < Size; i++)
            {
                if (i <= 9)
                {
                    Console.Write(i  + ":  ");
                }
                else Console.Write(i + ": ");

                for (int j = 0; j < Size; j++)
                {
                    Console.Write(_board[Tuple.Create(i, j)] + "   ");
                }

                Console.WriteLine();
            }
        }

        public bool CheckWinner(int row, int col)
        {
            int[,] directions = { { 1, 0 }, { 0, 1 }, { 1, 1 }, { 1, -1 } };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dr = directions[i, 0];
                int dc = directions[i, 1];
                int count = 1;
                int r = row + dr;
                int c = col + dc;
                while (count < _winLength && r >= 0 && r < Size && c >= 0 && c < Size && _board[Tuple.Create(r, c)] == CurrentPlayer)
                {
                    count++;
                    r += dr;
                    c += dc;
                }

                r = row - dr;
                c = col - dc;
                while (count < _winLength && r >= 0 && r < Size && c >= 0 && c < Size && _board[Tuple.Create(r, c)] == CurrentPlayer)
                {
                    count++;
                    r -= dr;
                    c -= dc;
                }

                if (count >= _winLength)
                {
                    return true;
                }
            }

            return false;
        }

        public bool MakeMove(int row, int col)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size)
            {
                Console.WriteLine("Invalid move. Row and column numbers should be between 0 and " + (Size - 1) + ".");
                return false;
            }

            if (_board[Tuple.Create(row, col)] != EmptyCell)
            {
                Console.WriteLine("This cell is already taken.");
                return false;
            }

            _board[Tuple.Create(row, col)] = CurrentPlayer;
            if (CheckWinner(row, col))
            {
                Console.WriteLine($"Player {CurrentPlayer} wins!");
                return true;
            }

            CurrentPlayer = CurrentPlayer == 'X' ? 'O' : 'X';
            return false;
        }

        public bool IsBoardFull() => _board.All(cell => cell.Value != EmptyCell);

        public int Size { get; }

        public char CurrentPlayer { get; set; }

        public char GetCell(int row, int col)
        {
            return _board[Tuple.Create(row, col)];
        }
    }
}
