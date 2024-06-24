namespace renju;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Choose game:");
        Console.WriteLine("1. Single Player");
        Console.WriteLine("2. PvP");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
        {
            Console.WriteLine("Invalid choice. Please enter 1 or 2.");
        }

        Renju renju = new();
        Player player1 = new HumanPlayer();
        Player player2 = choice == 1 ? new RandomBot() : new HumanPlayer();

        while (true)
        {
            renju.PrintBoard();
            Player currentPlayer = (renju.CurrentPlayer == 'X') ? player1 : player2;

            Tuple<int, int> move = currentPlayer.MakeMove(renju);
            if (renju.MakeMove(move.Item1, move.Item2))
            {
                renju.PrintBoard();
                Console.WriteLine($"Player {renju.CurrentPlayer} wins!");
                break;
            }

            if (!renju.IsBoardFull()) continue;
            Console.WriteLine("No available moves. It's a draw.");
            break;
        }
    }
}