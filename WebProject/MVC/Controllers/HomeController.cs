using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CompetitionInterface _competitionInterface;
        public HomeController(ILogger<HomeController> logger, CompetitionInterface competitionInterface)
        {
            _logger = logger;
            _competitionInterface = competitionInterface;
        }

        public async Task<IActionResult> Index()
        {
            var competition = await _competitionInterface.GetAll();
            return View(competition);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
