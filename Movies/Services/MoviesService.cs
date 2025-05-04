using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;
using Movies.Services.Abstractions;
using Movies.View;


namespace Movies
{
    public class MoviesSirvice : IMoviesSirvice
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MoviesSirvice(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public MoviesSirvice(ApplicationDbContext context)
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
        public async Task<IActionResult> Catalog(string nameFilter, int? yearFilter, string? sortbyName)
        {
         
        }


        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
          

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
           
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
           
        }
        [Authorize(Roles = "Admin")]

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ReleaseYear,Description,Ratings,GenreId,Image")] Movie movie)
        {
         
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

        public void Movie(int moveX, int moveY)
        {
            throw new NotImplementedException();
        }
    }
}
