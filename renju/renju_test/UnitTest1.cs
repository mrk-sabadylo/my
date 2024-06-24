using renju;

namespace renju_test;

[TestFixture]
public class RenjuTests
{
    [Test]
    public void MakeMove_ValidMove_ReturnsTrue()
    {
        Renju renju = new Renju();
        bool result = renju.MakeMove(1, 1);
        Assert.That(result, Is.False);
    }

    [Test]
    public void MakeMove_InvalidMove_ReturnsFalse()
    {
        Renju renju = new Renju();
        bool result = renju.MakeMove(-1, 0);
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsBoardFull_EmptyBoard_ReturnsFalse()
    {
        Renju renju = new Renju();
        bool result = renju.IsBoardFull();
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsBoardFull_FullBoard_ReturnsTrue()
    {
        Renju renju = new Renju();
        FillBoard(renju);
        bool result = renju.IsBoardFull();
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckWinner_NoWinner_ReturnsFalse()
    {
        Renju renju = new Renju();
        bool result = renju.CheckWinner(0, 0);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckWinner_HorizontalWin_ReturnsTrue()
    {
        Renju renju = new Renju();
        for (int i = 0; i < 5; i++)
        {
            renju.MakeMove(0, i);
        }
        bool result = renju.CheckWinner(0, 4);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckWinner_VerticalWin_ReturnsTrue()
    {
        Renju renju = new Renju();
        for (int i = 0; i < 5; i++)
        {
            renju.MakeMove(i, 0);
        }
        bool result = renju.CheckWinner(4, 0);
        Assert.That(result, Is.False);
    }

    private void FillBoard(Renju renju)
    {
        int size = renju.Size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                renju.MakeMove(row, col);
            }
        }
    }
}