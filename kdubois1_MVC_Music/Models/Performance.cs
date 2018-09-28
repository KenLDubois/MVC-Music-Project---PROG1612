using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kdubois1_MVC_Music.Models
{
    public class Performance
    {

        public int SongID { get; set; }
        public int MusicianID { get; set; }

        public virtual Song Song { get; set; }
        public virtual Musician Musician { get; set; }

    }
}
