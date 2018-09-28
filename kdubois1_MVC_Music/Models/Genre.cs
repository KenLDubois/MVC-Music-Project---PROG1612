using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kdubois1_MVC_Music.Models
{
    public class Genre
    {
        public Genre()
        {
            this.Albums = new HashSet<Album>();
            this.Songs = new HashSet<Song>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Genre name cannot be blank")]
        [StringLength(100, ErrorMessage = "Genre names cannot exceed 100 characters")]
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
