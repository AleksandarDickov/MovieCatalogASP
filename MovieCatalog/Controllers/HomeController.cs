using MovieCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Web;
using System.Web.Mvc;


namespace MovieCatalog.Controllers
{
    public class HomeController : Controller
    {
        MovieContext _db = new MovieContext();

       
        public ActionResult Index(string searchTerm = null)
        {
            var model =
            from r in _db.Movies
            orderby r.Name
            select r;

            return View(model);
        }

       
        public ActionResult About()
        {
            var model = new AboutModel();
            model.Name = "Aca, Ana, Jelena i Zdravka";
            model.Location = "Novi Sad, Serbia";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}