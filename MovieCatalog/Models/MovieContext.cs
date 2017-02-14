using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieCatalog.Models
{
    public class MovieContext : DbContext
    {
        //public MovieContext() : base()
        //{
        //    Database.SetInitializer<MovieContext>(new DropCreateDatabaseIfModelChanges<MovieContext>());
        //}

        public DbSet<Movie> Movies { get; set; }
    }
}