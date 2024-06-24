namespace renju;

public abstract class Player
{
    public abstract Tuple<int, int> MakeMove(Renju renju);
}
