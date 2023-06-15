using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Movie_ProducerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Movie_ProducerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movie_Producer
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Movie_Producers.Include(m => m.Movie).Include(m => m.Producer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Movie_Producer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie_Producers == null)
            {
                return NotFound();
            }

            var movie_Producer = await _context.Movie_Producers
                .Include(m => m.Movie)
                .Include(m => m.Producer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie_Producer == null)
            {
                return NotFound();
            }

            return View(movie_Producer);
        }

        // GET: Movie_Producer/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id");
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Bio");
            return View();
        }

        // POST: Movie_Producer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,ProducerId")] Movie_Producer movie_Producer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie_Producer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", movie_Producer.MovieId);
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Bio", movie_Producer.ProducerId);
            return View(movie_Producer);
        }

        // GET: Movie_Producer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie_Producers == null)
            {
                return NotFound();
            }

            var movie_Producer = await _context.Movie_Producers.FindAsync(id);
            if (movie_Producer == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", movie_Producer.MovieId);
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Bio", movie_Producer.ProducerId);
            return View(movie_Producer);
        }

        // POST: Movie_Producer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,ProducerId")] Movie_Producer movie_Producer)
        {
            if (id != movie_Producer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie_Producer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Movie_ProducerExists(movie_Producer.Id))
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
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", movie_Producer.MovieId);
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Bio", movie_Producer.ProducerId);
            return View(movie_Producer);
        }

        // GET: Movie_Producer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie_Producers == null)
            {
                return NotFound();
            }

            var movie_Producer = await _context.Movie_Producers
                .Include(m => m.Movie)
                .Include(m => m.Producer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie_Producer == null)
            {
                return NotFound();
            }

            return View(movie_Producer);
        }

        // POST: Movie_Producer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie_Producers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movie_Producers'  is null.");
            }
            var movie_Producer = await _context.Movie_Producers.FindAsync(id);
            if (movie_Producer != null)
            {
                _context.Movie_Producers.Remove(movie_Producer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Movie_ProducerExists(int id)
        {
          return (_context.Movie_Producers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
