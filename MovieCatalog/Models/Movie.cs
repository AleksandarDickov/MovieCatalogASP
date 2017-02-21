using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieCatalog.Models
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Director { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [ValidateDateRange]
        //[Range(typeof(DateTime), "01/01/1920", "01/01/2020", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Genre { get; set; }

    }
}