using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieCatalog.Models;

namespace MovieCatalog.Controllers
{
    public class MoviesController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Movies

        public ActionResult Index(string searchString, string sortOrder, string currentFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "Name_asc" ? "Name_desc" : "Name_asc";
            ViewBag.GenreSortParm = sortOrder == "Genre_asc" ? "Genre_desc" : "Genre_asc";
            ViewBag.DirectorSortParm = sortOrder == "Director_asc" ? "Director_desc" : "Director_asc";
            ViewBag.ReleaseDateSortParm = sortOrder == "Date_asc" ? "Date_desc" : "Date_asc";

            var movies = from m in db.Movies select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                //searchString = currentFilter;
                ViewBag.CurrentFilter = searchString;
                movies = movies.Where(s => s.Name.StartsWith(searchString) || s.Genre.StartsWith(searchString));
            }
            
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Movie", movies);
                
            }
           

            switch (sortOrder)
            {
                case "Name_desc":
                    movies = movies.OrderByDescending(s => s.Name);
                    break;
                case "Director_desc":
                    movies = movies.OrderByDescending(s => s.Director);
                    break;
                case "Director_asc":
                    movies = movies.OrderBy(s => s.Director);
                    break;
                case "Genre_desc":
                    movies = movies.OrderByDescending(s => s.Genre);
                    break;
                case "Genre_asc":
                    movies = movies.OrderBy(s => s.Genre);
                    break;
                case "Date_asc":
                    movies = movies.OrderBy(s => s.ReleaseDate);
                    break;
                case "Date_desc":
                    movies = movies.OrderByDescending(s => s.ReleaseDate);
                    break;
                case "Name_asc":
                default:
                    movies = movies.OrderBy(s => s.Name);
                    break;
            }

            return View(movies);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Director,ReleaseDate,Genre")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Director,ReleaseDate,Genre")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        //[HttpPost, ActionName("Search")]
        //public ActionResult Search(string name)
        //{
        //    var message = Server.HtmlEncode(name);
        //    return View(db.Movies.ToList());
        //}

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
