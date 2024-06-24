namespace renju;

public class HumanPlayer : Player
{
    public override Tuple<int, int> MakeMove(Renju renju)
    {
        while (true)
        {
            Console.Write("Enter row and column: ");
            string input = Console.ReadLine() ?? String.Empty;
            string[] parts = input.Split();
            if (parts.Length == 2 && int.TryParse(parts[0], out int row) && int.TryParse(parts[1], out int col))
            {
                return Tuple.Create(row - 1, col - 1);
            }
            Console.WriteLine("Invalid input. Please enter row and column separated by space.");
        }
    }
}