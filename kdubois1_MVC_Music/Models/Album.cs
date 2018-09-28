﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace kdubois1_MVC_Music.Models
{
    public class Album
    {
        public Album()
        {
            this.Songs = new HashSet<Song>(); // initialize Songs so it's not null
        }

        public int ID { get; set; }

        [Display(Name = "Album Name")]
        [Required(ErrorMessage = "The album name cannot be blank.")]
        [StringLength(50, ErrorMessage = "The album name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Display(Name = "Production Year")]
        [Required(ErrorMessage = "Production year cannot be blank")]
        [RegularExpression("^\\d{4}$", ErrorMessage = "Production year must have exactly 4 digits")]
        public int YearProduced { get; set; }

        [Required(ErrorMessage = "The price cannot be blank")]
        [Range(1.00, 200000.00, ErrorMessage = "The price must be between 1 and 200,000.00")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please indicate the genre.")]
        [Display(Name = "Album Genre")]
        public int GenreID { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
