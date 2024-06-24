
using Microsoft.AspNetCore.Mvc;
using renju;

namespace front.Controllers;

public class HomeController (/*ILogger<HomeController> _logger*/) : Controller
{
    
    private static Renju Renju = new();
    private static RandomBot Bot = new();

    public IActionResult Index()
    {
        return View(Renju);
    }

    [HttpGet]
    public IActionResult MakeMove(int row, int col)
    {
        if (Renju.MakeMove(row, col))
        {
            ViewBag.Message = $"Player {Renju.CurrentPlayer} wins!";
        }
        else if (Renju.IsBoardFull())
        {
            ViewBag.Message = "It's a draw!";
        }
        else
        {
           
            if (Renju.CurrentPlayer != 'O') return View("Index", Renju);
            Tuple<int, int> move = Bot.MakeMove(Renju);
            Console.WriteLine(move);
            Renju.MakeMove(move.Item1, move.Item2);
            Renju.CurrentPlayer = 'X';
        }

        return View("Index", Renju);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Rematch()
    {
        ViewBag.Message = String.Empty;
        Renju = new Renju();
        Bot = new RandomBot();
        return Redirect("Index");
    }
}