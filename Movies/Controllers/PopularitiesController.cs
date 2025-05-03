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
    public class PopularitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PopularitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Popularities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Popularity.ToListAsync());
        }

        // GET: Popularities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var popularity = await _context.Popularity
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (popularity == null)
            {
                return NotFound();
            }

            return View(popularity);
        }

        // GET: Popularities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Popularities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Views")] Popularity popularity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(popularity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(popularity);
        }

        // GET: Popularities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var popularity = await _context.Popularity.FindAsync(id);
            if (popularity == null)
            {
                return NotFound();
            }
            return View(popularity);
        }

        // POST: Popularities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Views")] Popularity popularity)
        {
            if (id != popularity.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(popularity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PopularityExists(popularity.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(popularity);
        }

        // GET: Popularities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var popularity = await _context.Popularity
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (popularity == null)
            {
                return NotFound();
            }

            return View(popularity);
        }

        // POST: Popularities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var popularity = await _context.Popularity.FindAsync(id);
            if (popularity != null)
            {
                _context.Popularity.Remove(popularity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PopularityExists(int id)
        {
            return _context.Popularity.Any(e => e.MovieId == id);
        }
    }
}
