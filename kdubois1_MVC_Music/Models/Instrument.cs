using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kdubois1_MVC_Music.Models
{
    public class Instrument
    {
        public Instrument()
        {
            this.Musicians = new HashSet<Musician>();
            this.Plays = new HashSet<Plays>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Name cannot be blank")]
        [StringLength(50, ErrorMessage = "Instruments names cannot exceed 50 characters. Sorry Dr. Seus.")]
        public string Name { get; set; }

        public virtual ICollection<Musician> Musicians { get; set; }
        public virtual ICollection<Plays> Plays { get; set; }

    }
}
