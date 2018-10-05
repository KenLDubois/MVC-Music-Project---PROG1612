using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kdubois1_MVC_Music.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace kdubois1_MVC_Music.Data
{
    public class MUSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new kdubois1_MVC_MusicContext(
                serviceProvider.GetRequiredService<DbContextOptions<kdubois1_MVC_MusicContext>>()))
            {
                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(
                        new Genre
                        {
                            Name = "Hip-hop"
                        },
                        new Genre
                        {
                            Name = "Rock"
                        },
                        new Genre
                        {
                            Name = "Folk"
                        },
                        new Genre
                        {
                            Name = "Heavy Metal"
                        },
                        new Genre
                        {
                            Name = "Classical"
                        },
                        new Genre
                        {
                            Name = "Country"
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Albums.Any())
                {
                    context.Albums.AddRange(
                        new Album
                        {
                            Name = "The Impossible Kid",
                            YearProduced = 2016,
                            Price = 25.00m,
                            GenreID = context.Genres.FirstOrDefault(g => g.Name=="Hip-hop").ID
                        },
                        new Album
                        {
                            Name = "Boys & Girls",
                            YearProduced = 2012,
                            Price = 19.00m,
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Rock").ID
                        },
                        new Album
                        {
                            Name = "Bringing It All Back Home",
                            YearProduced = 1965,
                            Price = 500.00m,
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Rock").ID
                        },
                        new Album
                        {
                            Name = "Cassadaga",
                            YearProduced = 2007,
                            Price = 600.00m,
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Folk").ID
                        },
                        new Album
                        {
                            Name = "Conor Oberst",
                            YearProduced = 2008,
                            Price = 45.00m,
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Folk").ID
                        },
                        new Album
                        {
                            Name = "The Battle of Los Angeles",
                            YearProduced = 1999,
                            Price = 100.00m,
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Rock").ID
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Songs.Any())
                {
                    context.Songs.AddRange(
                        new Song
                        {
                            Title = "Lotta Years",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Hip-hop").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "The Impossible Kid").ID
                        },
                        new Song
                        {
                            Title = "Kirby",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Hip-hop").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "The Impossible Kid").ID
                        },
                        new Song
                        {
                            Title = "TUFF",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Hip-hop").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "The Impossible Kid").ID
                        },
                        new Song
                        {
                            Title = "Hold On",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Rock").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "Boys & Girls").ID
                        },
                        new Song
                        {
                            Title = "Hang Loose",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Rock").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "Boys & Girls").ID
                        },
                        new Song
                        {
                            Title = "Maggie's Farm",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Folk").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "Bringing It All Back Home").ID
                        },
                        new Song
                        {
                            Title = "Mr. Tambourine Man",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Folk").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "Bringing It All Back Home").ID
                        },
                        new Song
                        {
                            Title = "Four Winds",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Rock").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "Cassadaga").ID
                        },
                        new Song
                        {
                            Title = "If The Brakeman Turns My Way",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Folk").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "Cassadaga").ID
                        },
                        new Song
                        {
                            Title = "Classic Cars",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Country").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "Cassadaga").ID
                        },
                        new Song
                        {
                            Title = "Cape Canaveral",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Folk").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "Conor Oberst").ID
                        },
                        new Song
                        {
                            Title = "Milk Thistle",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Folk").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "Conor Oberst").ID
                        },
                        new Song
                        {
                            Title = "Calm Like a Bomb",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Heavy Metal").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "The Battle of Los Angeles").ID
                        },
                        new Song
                        {
                            Title = "Sleep No In the Fire",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Heavy Metal").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "The Battle of Los Angeles").ID
                        },
                        new Song
                        {
                            Title = "Born of a Broken Man",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Heavy Metal").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "The Battle of Los Angeles").ID
                        },
                        new Song
                        {
                            Title = "Ashes In the Fall",
                            GenreID = context.Genres.FirstOrDefault(g => g.Name == "Heavy Metal").ID,
                            AlbumID = context.Albums.FirstOrDefault(a => a.Name == "The Battle of Los Angeles").ID
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Instruments.Any())
                {
                    context.Instruments.AddRange(
                        new Instrument
                        {
                            Name = "Vocals"
                        },
                        new Instrument
                        {
                            Name = "Electric Guitar"
                        },
                        new Instrument
                        {
                            Name = "Acoustic Guitar"
                        },
                        new Instrument
                        {
                            Name = "Drums"
                        },
                        new Instrument
                        {
                            Name = "Piano"
                        },
                        new Instrument
                        {
                            Name = "Banjo"
                        },
                        new Instrument
                        {
                            Name = "Beat Machine"
                        },
                        new Instrument
                        {
                            Name = "Ukulele"
                        },
                        new Instrument
                        {
                            Name = "Bass Guitar"
                        },
                        new Instrument
                        {
                            Name = "Harmonica"
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Musicians.Any())
                {
                    context.Musicians.AddRange(
                        new Musician
                        {
                            StageName = "Aesop Rock",
                            FName = "Ian",
                            MName = "Matthias",
                            LName = "Bavitz",
                            Phone = 5555555500,
                            DOB = new DateTime(1976, 07, 05),
                            SIN = "111111100",
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Vocals").ID
                        },
                        new Musician
                        {
                            FName = "Hanni", //Featured on Lotta Years
                            MName = "El",
                            LName = "Khatib",
                            Phone = 5555555501,
                            DOB = new DateTime(1981, 06, 08),
                            SIN = "111111101",
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Vocals").ID
                        },
                        new Musician
                        {
                            FName = "Kimya",//ft. in TUFF
                            LName = "Dawsom",
                            Phone = 5555555502,
                            DOB = new DateTime(1972, 11, 17),
                            SIN = "111111102",
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Vocals").ID
                        },
                        new Musician
                        {
                            StageName = "Carnage the Executioner",
                            FName = "Terrell", //Featured on Lotta Years
                            LName = "Woods",
                            Phone = 5555555503,
                            DOB = new DateTime(1974, 06, 08),
                            SIN = "111111103",
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Beat Machine").ID
                        },
                        new Musician
                        {
                            FName = "Brittany",
                            LName = "Howard",
                            Phone = 5555555504,
                            DOB = new DateTime(1988, 10, 02),
                            SIN = "111111104",
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Vocals").ID
                        },
                        new Musician
                        {
                            StageName = "Bob Dylan",
                            FName = "Robert",
                            MName = "Allen",
                            LName = "Zimmerman",
                            Phone = 5555555505,
                            DOB = new DateTime(1941, 05, 24),
                            SIN = "111111105",
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Vocals").ID
                        },
                        new Musician
                        {
                            FName = "Conor",
                            MName = "Mullen",
                            LName = "Oberst",
                            Phone = 5555555506,
                            DOB = new DateTime(1980, 02, 15),
                            SIN = "111111106",
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Vocals").ID
                        },
                        new Musician
                        {
                            FName = "Michael",
                            MName = "Riley",
                            LName = "Mogis",
                            Phone = 5555555507,
                            DOB = new DateTime(1974, 05, 16),
                            SIN = "111111107",
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Acoustic Guitar").ID
                        },
                        new Musician
                        {
                            FName = "Nathaniel",
                            MName = "Clifford",
                            LName = "Walcott",
                            Phone = 5555555508,
                            DOB = new DateTime(1978, 03, 06),
                            SIN = "111111108",
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Piano").ID
                        },
                        new Musician
                        {
                            FName = "Zacharias",
                            MName = "Manuel",
                            LName = "de la Rocha",
                            Phone = 5555555509,
                            DOB = new DateTime(1970, 01, 12),
                            SIN = "111111109",
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Vocals").ID
                        },
                        new Musician
                        {
                            FName = "Timothy",
                            MName = "Robert",
                            LName = "Commerford",
                            Phone = 5555555510,
                            DOB = new DateTime(1968, 02, 26),
                            SIN = "111111110",
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Bass Guitar").ID
                        },
                        new Musician
                        {
                            FName = "Thomas",
                            MName = "Baptist",
                            LName = "Morello",
                            Phone = 5555555511,
                            DOB = new DateTime(1964, 05, 30),
                            SIN = "111111111",
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Electric Guitar").ID
                        },
                        new Musician
                        {
                            FName = "Brad",
                            LName = "Wilk",
                            Phone = 5555555512,
                            DOB = new DateTime(1968, 09, 05),
                            SIN = "111111112",
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Drums").ID
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Plays.Any())
                {
                    context.Plays.AddRange(

                        new Plays
                        {
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Beat Machine").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111100").ID
                        },
                        new Plays
                        {
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Acoustic Guitar").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111102").ID
                        },
                        new Plays
                        {
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Electric Guitar").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111104").ID
                        },
                        new Plays
                        {
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Acoustic Guitar").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111105").ID
                        },
                        new Plays
                        {
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Electric Guitar").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111105").ID
                        },
                        new Plays
                        {
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Harmonica").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111105").ID
                        },
                        new Plays
                        {
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Acoustic Guitar").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111106").ID
                        },
                        new Plays
                        {
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Electric Guitar").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111106").ID
                        },
                        new Plays
                        {
                            InstrumentID = context.Instruments.FirstOrDefault(i => i.Name == "Harmonica").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111106").ID
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Performances.Any())
                {
                    context.Performances.AddRange(
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Lotta Years").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111100").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Lotta Years").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111101").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Lotta Years").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111103").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Kirby").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111100").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "TUFF").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111100").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "TUFF").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111102").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Hold On").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111104").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Hang Loose").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111104").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Maggie's Farm").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111105").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Mr. Tambourine Man").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111105").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Four Winds").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111106").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Four Winds").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111107").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "If The Brakeman Turns My Way").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111106").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "If The Brakeman Turns My Way").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111107").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Classic Cars").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111106").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Classic Cars").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111107").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Cape Canaveral").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111106").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Milk Thistle").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111106").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Calm Like a Bomb").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111109").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Calm Like a Bomb").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111110").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Calm Like a Bomb").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111111").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Calm Like a Bomb").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111112").ID
                        },
                         new Performance
                         {
                             SongID = context.Songs.FirstOrDefault(s => s.Title == "Sleep No In the Fire").ID,
                             MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111109").ID
                         },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Sleep No In the Fire").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111110").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Sleep No In the Fire").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111111").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Sleep No In the Fire").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111112").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Born of a Broken Man").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111109").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Born of a Broken Man").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111110").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Born of a Broken Man").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111111").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Born of a Broken Man").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111112").ID
                        },
                         new Performance
                         {
                             SongID = context.Songs.FirstOrDefault(s => s.Title == "Ashes In the Fall").ID,
                             MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111109").ID
                         },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Ashes In the Fall").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111110").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Ashes In the Fall").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111111").ID
                        },
                        new Performance
                        {
                            SongID = context.Songs.FirstOrDefault(s => s.Title == "Ashes In the Fall").ID,
                            MusicianID = context.Musicians.FirstOrDefault(m => m.SIN == "111111112").ID
                        }
                        );
                    context.SaveChanges();
                }
            }
        }
    }
}
