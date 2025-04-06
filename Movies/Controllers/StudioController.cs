using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
    public class StudioController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject ApplicationDbContext (assumed)
        public StudioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Studio
        public IActionResult Index()
        {
            var studios = _context.Studios.ToList(); // Fetch all studios
            return View(studios); // Pass the list to the view
        }

        // GET: Studio/Details/5
        public IActionResult Details(int id)
        {
            var studio = _context.Studios
                                 .FirstOrDefault(s => s.Id == id);

            if (studio == null)
            {
                return NotFound();
            }

            return View(studio);
        }

        // GET: Studio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,DirectorName,NumberOfStaff,Movie")] Studio studio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studio); // Add the studio to the context
                _context.SaveChanges(); // Save changes to the database
                return RedirectToAction(nameof(Index)); // Redirect to the Index page
            }
            return View(studio); // If the model state is not valid, show the Create view again
        }

        // GET: Studio/Edit/5
        public IActionResult Edit(int id)
        {
            var studio = _context.Studios
                                 .FirstOrDefault(s => s.Id == id);

            if (studio == null)
            {
                return NotFound();
            }

            return View(studio);
        }

        // POST: Studio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,DirectorName,NumberOfStaff,Movie")] Studio studio)
        {
            if (id != studio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studio); // Update the studio in the context
                    _context.SaveChanges(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Studios.Any(s => s.Id == studio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the Index page
            }

            return View(studio); // If the model state is not valid, show the Edit view again
        }

        // GET: Studio/Delete/5
        public IActionResult Delete(int id)
        {
            var studio = _context.Studios
                                 .FirstOrDefault(s => s.Id == id);

            if (studio == null)
            {
                return NotFound();
            }

            return View(studio);
        }

        // POST: Studio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var studio = _context.Studios.Find(id);
            _context.Studios.Remove(studio); // Remove the studio from the context
            _context.SaveChanges(); // Save changes to the database
            return RedirectToAction(nameof(Index)); // Redirect to the Index page
        }
    }
}