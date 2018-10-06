using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kdubois1_MVC_Music.Data;
using kdubois1_MVC_Music.Models;

namespace kdubois1_MVC_Music.Controllers
{
    public class MusiciansController : Controller
    {
        private readonly kdubois1_MVC_MusicContext _context;

        public MusiciansController(kdubois1_MVC_MusicContext context)
        {
            _context = context;
        }

        // GET: Musicians
        public async Task<IActionResult> Index()
        {
            var kdubois1_MVC_MusicContext = _context.Musicians.Include(m => m.Instrument);
            return View(await kdubois1_MVC_MusicContext.ToListAsync());
        }

        // GET: Musicians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians
                .Include(m => m.Instrument)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (musician == null)
            {
                return NotFound();
            }

            return View(musician);
        }

        // GET: Musicians/Create
        public IActionResult Create()
        {
            PopulateInstrumentDropdown();

            return View();
        }

        // POST: Musicians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StageName,FName,MName,LName,Phone,DOB,SIN,InstrumentID")] Musician musician)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(musician);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {

                if (dex.InnerException.Message.Contains("IX_Musicians_SIN"))
                {
                    ModelState.AddModelError("SIN", "Please ensure SIN numbers are unique");
                }
                else
                {
                    ModelState.AddModelError("","Unable to save changes. Please try again later.");
                }
            }

            PopulateInstrumentDropdown(musician);

            return View(musician);
        }

        // GET: Musicians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians.FindAsync(id);
            if (musician == null)
            {
                return NotFound();
            }

            PopulateInstrumentDropdown(musician);

            return View(musician);
        }

        // POST: Musicians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
             var musicianToUpdate = await _context.Musicians.SingleOrDefaultAsync(m => m.ID == id);

            if (musicianToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Musician>(musicianToUpdate, "",
               m => m.StageName,m => m.FName, m => m.MName, m => m.LName, m => m.Phone, m => m.DOB, m => m.SIN, m => m.InstrumentID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicianExists(musicianToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dex)
                {
                    if (dex.InnerException.Message.Contains("IX_Musicians_SIN"))
                    {
                        ModelState.AddModelError("SIN", "SIN numbers must be unique.");
                    }
                    else
                    {
                        ModelState.AddModelError("", dex.Message.ToString());
                    }
                }
            }
            //Validaiton Error so give the user another chance.
            PopulateInstrumentDropdown(musicianToUpdate);
            return View(musicianToUpdate);
        }

        // GET: Musicians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians
                .Include(m => m.Instrument)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (musician == null)
            {
                return NotFound();
            }

            return View(musician);
        }

        // POST: Musicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musician = await _context.Musicians.FindAsync(id);

            try
            {
                _context.Musicians.Remove(musician);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (ex.InnerException.Message.Contains("FK_Performances_Musicians_MusicianID"))
                {
                    ModelState.AddModelError("", "You cannot delete a musician before deleting their performances");
                }
                else
                {
                    ModelState.AddModelError("",ex.Message.ToString());
                }

                return View(musician);
            }


        }

        public void PopulateInstrumentDropdown(Musician musician = null)
        {
            var iQuery = new SelectList(_context.Instruments
                .OrderBy(i => i.Name), "ID", "Name", musician?.InstrumentID);
            ViewData["InstrumentID"] = iQuery;
        }

        private bool MusicianExists(int id)
        {
            return _context.Musicians.Any(e => e.ID == id);
        }
    }
}
