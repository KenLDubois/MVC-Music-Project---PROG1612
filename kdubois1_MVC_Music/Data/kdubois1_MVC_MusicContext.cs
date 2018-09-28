using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using kdubois1_MVC_Music.Models;

namespace kdubois1_MVC_Music.Data
{
    public class kdubois1_MVC_MusicContext : DbContext
    {
        public kdubois1_MVC_MusicContext (DbContextOptions<kdubois1_MVC_MusicContext> options)
            : base(options)
        {
        }

        // Add properties for DB sets of:
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song>  Songs { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Plays> Plays { get; set; }
        
        // TODO: Add remaing DB sets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add custom schema for music DB
            modelBuilder.HasDefaultSchema("MU");      

            // Make sure SIN number is unique
            modelBuilder.Entity<Musician>()
                .HasIndex(m => m.SIN)
                .IsUnique();

            //Many to many Intersection keys
            modelBuilder.Entity<Performance>()
                .HasKey(p => new { p.SongID, p.MusicianID });

            modelBuilder.Entity<Plays>()
                .HasKey(p => new { p.InstrumentID, p.MusicianID });

            //Prevent Cascade Deletes
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Musician)
                .WithMany(m => m.Performances)
                .HasForeignKey(p => p.MusicianID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Plays>()
                .HasOne(p => p.Instrument)
                .WithMany(i => i.Plays)
                .HasForeignKey(p => p.InstrumentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Instrument>()
                .HasMany<Musician>(i => i.Musicians)
                .WithOne(m => m.Instrument)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Genre>()
                .HasMany<Album>(g => g.Albums)
                .WithOne(a => a.Genre)
                .HasForeignKey(a => a.GenreID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Genre>()
                .HasMany<Song>(g => g.Songs)
                .WithOne(s => s.Genre)
                .HasForeignKey(s => s.GenreID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Album>()
                .HasMany<Song>(a => a.Songs)
                .WithOne(s => s.Album)
                .HasForeignKey(s => s.AlbumID)
                .OnDelete(DeleteBehavior.Restrict);
            
            //NOTE: 
            //Musician cascade to Plays
            //Song cascades to Performances

        }
    }
}
