using System;
using Unity;
using renju;

namespace renj_unity
{
    public interface IGameBot
    {
        Tuple<int, int> MakeMove(Renju renju);
    }

    public class RandomBot : IGameBot
    {
        private readonly Random _random;

        public RandomBot()
        {
            _random = new Random();
        }

        public Tuple<int, int> MakeMove(Renju renju)
        {
            int size = renju.Size;
            while (true)
            {
                int row = _random.Next(size);
                int col = _random.Next(size);
                if (renju.BoardAt(row, col) == Renju.EmptyCell)
                {
                    return Tuple.Create(row, col);
                }
            }
        }
    }

    public class Program
    {
        private readonly Renju _renju;
        private readonly IGameBot _bot;

        public Program(Renju renju, IGameBot bot)
        {
            _renju = renju;
            _bot = bot;
        }

        public void Run()
        {
            while (true)
            {
                _renju.PrintBoard();
                if (_renju.CurrentPlayer == 'X')
                {
                    Console.Write("Player X's turn (row col): ");
                    string input = Console.ReadLine() ?? string.Empty;
                    string[] inputParts = input.Split();

                    if (inputParts.Length != 2)
                    {
                        Console.WriteLine("Invalid input format. Please provide row and column numbers separated by space.");
                        continue;
                    }

                    if (!int.TryParse(inputParts[0], out int row) || !int.TryParse(inputParts[1], out int col))
                    {
                        Console.WriteLine("Invalid input. Please enter valid row and column numbers.");
                        continue;
                    }

                    if (_renju.MakeMove(row, col))
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Bot O's turn:");
                    Tuple<int, int> move = _bot.MakeMove(_renju);
                    int row = move.Item1;
                    int col = move.Item2;
                    Console.WriteLine($"Bot O chooses: {row} {col}");
                    if (_renju.MakeMove(row, col))
                    {
                        break;
                    }
                }

                if (_renju.IsBoardFull())
                {
                    Console.WriteLine("No available moves. It's a draw.");
                    break;
                }
            }
        }

        static void Main(string[] args)
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IGameBot, RandomBot>();
            container.RegisterType<Renju>();

            Renju renju = container.Resolve<Renju>();
            IGameBot bot = container.Resolve<IGameBot>();

            Program program = new Program(renju, bot);
            program.Run();
        }
    }
}
