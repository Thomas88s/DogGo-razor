using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Repositories;
using DogGo.Models;

namespace DogGo.Controllers
{
    public class WalkController : Controller
    {
        private readonly IWalkRepository _walkRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkController(IWalkRepository walkRepository)
        {
            _walkRepo = walkRepository;
        }

        // GET: WalkController
        public ActionResult Index()
        {
            List<Walk> walks = _walkRepo.GetAllWalks();
            
            return View(walks);
        }

        // GET: WalkController/Details/5
        public ActionResult Details(int id)
        {
            Walk walk = _walkRepo.GetWalkById(id);

            if (walk == null)
            {
                return NotFound();
            }

            return View(walk);
        }

        // GET: WalkController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
