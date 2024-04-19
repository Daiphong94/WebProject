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
        private readonly EventInterface _eventInterface;
        public HomeController(FAQInterface FAQInterface, CompetitionInterface competitionInterface, EventInterface eventInterface)
        {
            _FAQInterface = FAQInterface;
            _competitionInterface = competitionInterface;
            _eventInterface = eventInterface;
        }

        public async Task<IActionResult> Index()
        {
            var viewmodel = new ViewModelHome
            {
                Competition = await _competitionInterface.GetAll(),
                FAQ = await _FAQInterface.GetAllFAQ(),
                Events = await _eventInterface.GetAll()
                
            };
            
            return  View(viewmodel);
        }
    }
}
