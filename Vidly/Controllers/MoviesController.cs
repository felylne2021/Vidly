using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers {
    public class MoviesController : Controller {
        private ApplicationDbContext _context;

        public MoviesController() {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        public ViewResult Index() {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        public ActionResult Details(int id) {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }
        // GET: Movies/Random
        public ActionResult Random() {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer> {
                new Customer{Name = "Customer 1"},
                new Customer{Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleasedDate(int year, int month) {
            return Content(year + "/" + month);
        }

        public ActionResult New() {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel {
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id) {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie) {
            if (movie.Id == 0) {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else {
                var MovInDB = _context.Movies.Single(m => m.Id == movie.Id);

                MovInDB.GenreId = movie.GenreId;
                MovInDB.ReleaseDate = movie.ReleaseDate;
                movie.DateAdded = DateTime.Now;
                MovInDB.DateAdded = movie.DateAdded;
                MovInDB.Stock = movie.Stock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }
    }
}