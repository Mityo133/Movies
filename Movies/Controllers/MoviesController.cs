using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Admin Aceses
        [Authorize(Roles = "Admin")]
        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Movie.Include(m => m.Genre);
            return View(await applicationDbContext.ToListAsync());
        }
        //public async Task<IActionResult> Catalog()
        //{
        //    var applicationDbContext = _context.Movie.Include(m => m.Genre);
        //    return View(await applicationDbContext.ToListAsync());
        //}
        public async Task<IActionResult> Catalog(string nameFilter, int? yearFilter,string? OptionToSortBy)
        {
            var movies = from m in _context.Movie
                         select m;

            if (!String.IsNullOrEmpty(nameFilter))
            {
                movies = movies.Where(s => s.Name.Contains(nameFilter));
            }
            if (yearFilter.HasValue)
            {
                movies = movies.Where(s => s.ReleaseYear == yearFilter);
            }
            if (!String.IsNullOrEmpty(OptionToSortBy))
            {
                if (OptionToSortBy == "AZ")
                {
                    movies = movies.OrderByDescending(s => s.Name).Reverse();
                }else if(OptionToSortBy == "ZA")
                {
                    movies = movies.OrderByDescending(s => s.Name);
                }else if(OptionToSortBy =="Year-De")
                {
                    movies = movies.OrderByDescending(s=>s.ReleaseYear);
                }else if(OptionToSortBy == "Year-Ac")
                {
                    movies = movies.OrderBy(s => s.ReleaseYear);
                }else if (OptionToSortBy == "Low-Rating")
                {
                    movies = movies.OrderBy(s => s.Ratings);
                }else if (OptionToSortBy =="High-Rating")
                {
                    movies = movies.OrderByDescending(s => s.Ratings);
                }
            }
            return View("Catalog", await movies.ToListAsync());
        }


        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Genre)
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor) // 👈 This is the key line you're missing
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);

        }
        [Authorize(Roles = "Admin")]
        // GET: Movies/Create
        public IActionResult Create()
        {
            // Use Genre.Name as the display value in the dropdown
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,ReleaseYear,Description,Ratings,GenreId,Image")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", movie.GenreId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", movie.GenreId);
            return View(movie);
        }
        [Authorize(Roles = "Admin")]

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ReleaseYear,Description,Ratings,GenreId,Image")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", movie.GenreId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return NotFound();

            return View(movie);
        }

        // POST: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // First, remove the MovieActors entry
            var movieActors = await _context.MovieActors
                .FirstOrDefaultAsync(ma => ma.MovieId == id); // Assuming MovieId is the foreign key
            if (movieActors != null)
            {
                _context.MovieActors.Remove(movieActors);
            }

            // Now, remove the actual Movie entry (if it's not already removed by the first step)
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            // Commit both deletions
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}