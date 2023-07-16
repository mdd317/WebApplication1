using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: API TO DB

        public async Task<IActionResult> SaveDataFromAPI()
        {
            _context.Database.ExecuteSqlRaw("ALTER TABLE Movie NOCHECK CONSTRAINT FK_Movie_Cinemas_CinemaId");

            // Retrieve data from the API
            var apiData = await GetAPIData();

            // Map API data to modified movie model
            var movieData = MapAPIDataToModel(apiData);

            // Save the data to the database
            _context.Movie.AddRange(movieData);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Movies");
        }

        private async Task<dynamic> GetAPIData()
        {
            using (var client = new HttpClient())
            {
                var apiKey = "470821513db1ef1d834a642dd5133006";
                var url = $"https://api.themoviedb.org/3/movie/popular?api_key={apiKey}";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<dynamic>(content);

                return apiResponse;
            }
        }

        private List<Movie> MapAPIDataToModel(dynamic apiData)
        {
            // Map API data to modified movie model
            var movieData = new List<Movie>();

            foreach (var item in apiData.results)
            {
                var movie = new Movie
                {
                    Name = item.title,
                    Description = item.overview,
                    Popularity = item.popularity,
                    original_language = item.original_language,
                    release_date = item.release_date,
                    video = item.video,
                    vote_average = item.vote_average,
                    vote_count = item.vote_count,

                };

                movieData.Add(movie);
            }

            return movieData;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Movie
            .Include(m => m.Cinema)
            .Include(m => m.Movie_Producers)
            .ThenInclude(mp => mp.Producer)
                .ThenInclude(mp => mp.Studio);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Cinema)
                .Include(m => m.Movie_Producers)
                    .ThenInclude(mp => mp.Producer)
                    .ThenInclude(mp => mp.Studio)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "Description");
            ViewData["StudioId"] = new SelectList(_context.Studios, "Id", "Id");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,original_language,release_date,video,vote_average, vote_count,CinemaId,StudioId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "Description", movie.CinemaId);
            ViewData["ProducerId"] = new SelectList(_context.Studios, "ProducerId", "ProducerId", movie.Movie_Producers);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Movie_Producers)
            .ThenInclude(mp => mp.Producer)
            .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            if (movie.Movie_Producers == null)
            {
                movie.Movie_Producers = new List<Movie_Producer>();
            }
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "Description", movie.CinemaId);
            ViewData["ProducerId"] = new SelectList(_context.Studios, "ProducerId", "ProducerId", movie.Movie_Producers);
            return View(movie);
        }


        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,original_language,release_date,video,vote_average, vote_count,CinemaId,StudioId, Movie_Producers")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dbMovie = await _context.Movie
                        .Include(m => m.Movie_Producers)
                        .ThenInclude(mp => mp.Producer)
                        .FirstOrDefaultAsync(m => m.Id == movie.Id);

                    if (dbMovie == null)
                    {
                        return NotFound();
                    }

                    dbMovie.Name = movie.Name;
                    dbMovie.Description = movie.Description;
                    dbMovie.Price = movie.Price;
                    dbMovie.release_date = movie.release_date;
                    dbMovie.video = movie.video;
                    dbMovie.vote_average = movie.vote_average;
                    dbMovie.vote_count = movie.vote_count;
                    dbMovie.CinemaId = movie.CinemaId;

                    for (var i = 0; i < movie.Movie_Producers.Count; i++)
                    {
                        var movieProducer = movie.Movie_Producers[i];
                        var dbProducer = dbMovie.Movie_Producers[i].Producer;

                        dbProducer.FullName = movieProducer.Producer.FullName;
                        dbProducer.Bio = movieProducer.Producer.Bio;
                        dbProducer.ProfilePictureURL = movieProducer.Producer.ProfilePictureURL;
                    }

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


            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "Description", movie.CinemaId);
            return View(movie);

        }


        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Cinema)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
