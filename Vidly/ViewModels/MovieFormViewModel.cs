using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels {
    public class MovieFormViewModel {
        public IEnumerable<Genre> Genres { get; set; }
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please fill in movie's name")]
        public string Name { get; set; }



        [Display(Name = "Genre")]
        public int? GenreId { get; set; }

        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }


        [Display(Name = "Number in Stock")]
        [Range(1, 100)]
        public int? Stock { get; set; }

        public MovieFormViewModel() {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie) {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            Stock = movie.Stock;
            GenreId = movie.GenreId;
        }
    }
}