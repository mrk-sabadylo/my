namespace renju;

public class RandomBot : Player
{
    private readonly Random _random = new();

    public override Tuple<int, int> MakeMove(Renju renju)
    {
        int size = renju.Size;
        while (true)
        {
            int row = _random.Next(size);
            int col = _random.Next(size);
            if (renju.GetCell(row, col) == Renju.EmptyCell)
            {
                return Tuple.Create(row, col);
            }
        }
    }
}