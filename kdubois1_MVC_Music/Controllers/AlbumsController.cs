using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kdubois1_MVC_Music.Data;
using kdubois1_MVC_Music.Models;
using Microsoft.AspNetCore.Authorization;

namespace kdubois1_MVC_Music.Controllers
{
    [Authorize(Roles = "Staff, Supervisor, Admin")]
    public class AlbumsController : Controller
    {
        private readonly kdubois1_MVC_MusicContext _context;

        public AlbumsController(kdubois1_MVC_MusicContext context)
        {
            _context = context;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            var kdubois1_MVC_MusicContext = _context.Albums.Include(a => a.Genre);
            return View(await kdubois1_MVC_MusicContext.ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        [Authorize(Roles = "Supervisor, Admin")]
        public IActionResult Create()
        {
            //ViewData["GenreID"] = new SelectList(_context.Genres, "ID", "Name");
            PopulateGenreDropdown();
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor, Admin")]
        public async Task<IActionResult> Create([Bind("ID,Name,YearProduced,Price,GenreID")] Album album)
        {
            try
            {
                if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            }
            catch (Exception ex)
            {

                if (ex.InnerException.Message.Contains("IX_Albums_Name_YearProduced"))
                {
                    ModelState.AddModelError("YearProduced", "An album called " + album.Name + " already exists from " + album.YearProduced.ToString());
                }
                else
                {
                    ModelState.AddModelError("", ex.Message.ToString());
                }
            }

            PopulateGenreDropdown(album);
            return View(album);
        }

        // GET: Albums/Edit/5
        [Authorize(Roles = "Supervisor, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            PopulateGenreDropdown(album);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor, Admin")]
        public async Task<IActionResult> Edit(int id, Byte[] RowVersion)
        {
            var albumToUpdate = await _context.Albums.SingleOrDefaultAsync(a => a.ID == id);

            if(albumToUpdate == null)
            {
                return NotFound();
            }

            if(await TryUpdateModelAsync<Album>(albumToUpdate,"",
                a => a.Name, a => a.YearProduced, a => a.Price, a => a.GenreID))
            {
                try
                {
                    _context.Entry(albumToUpdate).Property("RowVersion").OriginalValue = RowVersion;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateConcurrencyException ex)
                {

                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Album)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                   
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("",
                            "Unable to save changes. The Album was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Album)databaseEntry.ToObject();
                        if (databaseValues.Name != clientValues.Name)
                        {
                            ModelState.AddModelError("Name", "Current value: " + databaseValues.Name);
                        }
                        if(databaseValues.YearProduced != clientValues.YearProduced)
                        {
                            ModelState.AddModelError("YearProduced", "Current value:" + databaseValues.YearProduced);
                        }
                        //Price
                        if (databaseValues.Price != clientValues.Price)
                        {
                            ModelState.AddModelError("Price", "Current value:" + databaseValues.Price);
                        }
                        //GenreID
                        if (databaseValues.GenreID != clientValues.GenreID)
                        {
                            Genre databaseGenre = await _context.Genres.SingleOrDefaultAsync(i => i.ID == databaseValues.GenreID);
                            ModelState.AddModelError("GenreID", $"Current value: {databaseGenre?.Name}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you are trying to edit" +
                           "has been modified by another user. Please review the changes and try again.");
                    }

                }
                catch(DbUpdateException dex)
                {
                    if (dex.InnerException.Message.Contains("IX_Albums_Name_YearProduced"))
                    {
                        ModelState.AddModelError("YearProduced", "An album called " + albumToUpdate.Name + " already exists from " + albumToUpdate.YearProduced.ToString());
                    }
                    else
                    {
                        ModelState.AddModelError("", dex.Message.ToString());
                    }
                }
            }

            PopulateGenreDropdown(albumToUpdate);
            return View(albumToUpdate);
        }

        // GET: Albums/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var album = await _context.Albums.FindAsync(id);

            try
            {
                _context.Albums.Remove(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (ex.InnerException.Message.Contains("FK_Songs_Albums_AlbumID"))
                {

                    ModelState.AddModelError("", "You cannot remove an album that contains songs.");
                }
                else
                {
                    ModelState.AddModelError("", ex.Message.ToString());
                }

                return View(album);
            }
            
        }

        public void PopulateGenreDropdown(Album album = null)
        {
            var gQuery = new SelectList(_context.Genres
                .OrderBy(g => g.Name),"ID","Name", album?.GenreID);
            ViewData["GenreID"] = gQuery;
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.ID == id);
        }
    }
}
