using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using kdubois1_MVC_Music.Models;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace kdubois1_MVC_Music.Data
{
    public class kdubois1_MVC_MusicContext : DbContext
    {
        //To give access to IHttpContextAccessor for Audit Data with IAuditable
        private readonly IHttpContextAccessor _httpContextAccessor;

        //Property to hold the UserName value
        public string UserName
        {
            get; private set;
        }

        public kdubois1_MVC_MusicContext (DbContextOptions<kdubois1_MVC_MusicContext> options)
            : base(options)
        {
            UserName = "SeedData";
        }

        public kdubois1_MVC_MusicContext(DbContextOptions<kdubois1_MVC_MusicContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            UserName = _httpContextAccessor.HttpContext?.User.Identity.Name;
            //UserName = (UserName == null) ? "Unknown" : UserName;
            UserName = UserName ?? "Unknown";
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

            // Make sure Album Title + Year is Unique
            modelBuilder.Entity<Album>()
                .HasIndex(a => new { a.Name, a.YearProduced })
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

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditable trackable)
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;

                        case EntityState.Added:
                            trackable.CreatedOn = now;
                            trackable.CreatedBy = UserName;
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;
                    }
                }
            }
        }

    }
}
