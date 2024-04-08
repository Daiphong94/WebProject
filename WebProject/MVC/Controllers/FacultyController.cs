﻿using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class FacultyController : Controller
    {
        private readonly FacultyInterface _facultyInterface;
        public FacultyController(FacultyInterface facultyInterface)
        {
            _facultyInterface = facultyInterface;
        }

        public async Task<IActionResult> Index()
        {
            var faculties = await _facultyInterface.GetAllFaculty();
            return View(faculties);
        }

        public IActionResult Create()
        {
            ViewBag.UserID = HttpContext.Session.GetString("UserID");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Faculty model)
        {
            var userIdString = HttpContext.Session.GetString("UserID");
            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId))
            {

                var email = HttpContext.Session.GetString("Email");


                model.UserID = userId;
                model.Email = email;
                model.JoiningDate = DateTime.Now;

                await _facultyInterface.AddFaculty(model);

                return RedirectToAction("Index");
            }
            else
            {

                return RedirectToAction("Login", "User");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var faculty = await _facultyInterface.GetByIdFaculty(id);
            if (faculty == null)
            {
                return NotFound();
            }
            return View(faculty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Faculty model)
        {
            if (id != model.FacultyID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _facultyInterface.UpdateFaculty(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var faculty = await _facultyInterface.GetByIdFaculty(id);
            if (faculty == null)
            {
                return NotFound();
            }
            return View(faculty);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _facultyInterface.DeleteFaculty(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Account()
        {
            return View();
        }
    }
}
