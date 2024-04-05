using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class SurveyController : Controller
    {
        public readonly SurveyInterface _surveyInterface;
        public SurveyController(SurveyInterface surveyInterface)
        {
            _surveyInterface = surveyInterface;
        }

        public async Task<IActionResult> Index()
        {
            var survey = await _surveyInterface.GetAllSurvey();
            return View(survey);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Survey model)
        {
            await _surveyInterface.AddSurvey(model);
            return RedirectToAction(nameof(Index));
            /*if (ModelState.IsValid)
            {
               
            }*/
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var survey = await _surveyInterface.GetByIdSurvey(id);
            if (survey == null)
            {
                return NotFound();
            }
            return View(survey);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Survey model)
        {
            if (id != model.SurveyID)
            {
                return BadRequest();
            }

            await _surveyInterface.UpdateSurvey(model);
            return RedirectToAction(nameof(Index));
            /* if (ModelState.IsValid)
             {

             }*/
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var survey = await _surveyInterface.GetByIdSurvey(id);
            if (survey == null)
            {
                return NotFound();
            }
            return View(survey);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _surveyInterface.DeleteSurvey(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
