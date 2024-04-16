using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly FAQInterface _FAQInterface;
        private readonly CompetitionInterface _competitionInterface;
        public HomeController(FAQInterface FAQInterface, CompetitionInterface competitionInterface)
        {
            _FAQInterface = FAQInterface;
            _competitionInterface = competitionInterface;
        }

        public async Task<IActionResult> Index()
        {
            var competition = await _competitionInterface.GetAll();
            return  View(competition);
        }
    }
}
