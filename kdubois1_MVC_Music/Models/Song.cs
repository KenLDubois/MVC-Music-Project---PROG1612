using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kdubois1_MVC_Music.Models
{
    public class Song
    {
        public Song()
        {
            this.Performances = new HashSet<Performance>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Song titles cannot be blank")]
        [StringLength(100, ErrorMessage = "Song titles cannot be longer than 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please select a genre.")]
        [Display(Name = "Song Genre")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a genre.")]
        public int GenreID { get; set; }

        [Required(ErrorMessage = "Please select an album.")]
        [Display(Name = "Album")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select an album.")]
        public int AlbumID { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Album Album { get; set; }

        public ICollection<Performance> Performances { get; set; }

    }
}
