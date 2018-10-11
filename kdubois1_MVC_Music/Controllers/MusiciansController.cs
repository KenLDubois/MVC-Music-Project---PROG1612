using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kdubois1_MVC_Music.Data;
using kdubois1_MVC_Music.Models;
using kdubois1_MVC_Music.ViewModels;

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
        public async Task<IActionResult> Index(int? InstrumentID, string SearchString, string SortDirection, string SortField, string ActionButton)
        {
            PopulateInstrumentDropdown();


            var musicians = from m in _context.Musicians
                            .Include(i => i.Instrument)
                            select m;

            if (InstrumentID.HasValue)
            {
                musicians = musicians.Where(m => m.InstrumentID == InstrumentID);
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                musicians = musicians.Where(m => m.DisplayName.ToUpper().Contains(SearchString.ToUpper()));
            }


            if (!string.IsNullOrEmpty(ActionButton))
            {
                if(ActionButton != "Filter")
                {
                    if(ActionButton == SortField)
                    {
                        SortDirection = string.IsNullOrEmpty(SortDirection) ? "desc" : "";
                    }

                    SortField = ActionButton;
                }
            }

            musicians = musicians.OrderBy(m => m.DisplayName);

            if (SortField == "DisplayName")
            {
                if (string.IsNullOrEmpty(SortDirection))
                {
                    musicians = musicians.OrderBy(m => m.DisplayName);
                }
                else
                {
                    musicians = musicians.OrderByDescending(m => m.DisplayName);
                }
            }

            if(SortField == "Age")
            {
                if (string.IsNullOrEmpty(SortDirection))
                {
                    musicians = musicians.OrderBy(m => m.Age);
                }
                else
                {
                    musicians = musicians.OrderByDescending(m => m.Age);
                }
            }

            if(SortField == "Phone")
            {
                if (string.IsNullOrEmpty(SortDirection))
                {
                    musicians = musicians.OrderBy(m => m.Phone);
                }
                else
                {
                    musicians = musicians.OrderByDescending(m => m.Phone);
                }
            }

            if(SortField == "Instrument")
            {
                if (string.IsNullOrEmpty(SortDirection))
                {
                    musicians = musicians.OrderBy(m => m.Instrument.Name);
                }
                else
                {
                    musicians = musicians.OrderByDescending(m => m.Instrument.Name);
                }
            }

            ViewData["SortField"] = SortField;
            ViewData["SortDirection"] = SortDirection;

            return View(await musicians.ToListAsync());
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
            var musician = new Musician();
            musician.Plays = new List<Plays>();
            PopulateInstrumentDropdown();
            PopulateAssignedInstrumentData(musician);
            return View();
        }

        // POST: Musicians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StageName,FName,MName,LName,Phone,DOB,SIN,InstrumentID")] Musician musician, string[] selectedInstruments)
        {
            try
            {
                if(selectedInstruments != null)
                {
                    musician.Plays = new List<Plays>();
                    foreach(var inst in selectedInstruments)
                    {
                        var instToAdd = new Plays { MusicianID = musician.ID, InstrumentID = int.Parse(inst) };
                        musician.Plays.Add(instToAdd);
                    }
                }
                

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

            PopulateAssignedInstrumentData(musician);
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

            var musician = await _context.Musicians
                .Include(m => m.Instrument)
                .Include(m => m.Plays).ThenInclude(m => m.Instrument)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (musician == null)
            {
                return NotFound();
            }

            PopulateInstrumentDropdown(musician);
            PopulateAssignedInstrumentData(musician);
            return View(musician);
        }

        // POST: Musicians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string[] selectedInstruments)
        {
             var musicianToUpdate = await _context.Musicians
                .Include(m => m.Instrument)
                .Include(m => m.Plays).ThenInclude(m => m.Instrument)
                .SingleOrDefaultAsync(m => m.ID == id);

            if (musicianToUpdate == null)
            {
                return NotFound();
            }

            UpdatePlays(selectedInstruments, musicianToUpdate);

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
            PopulateAssignedInstrumentData(musicianToUpdate);
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

        private void PopulateAssignedInstrumentData(Musician musician)
        {
            var allInstruments = _context.Instruments;
            var mInstruments = new HashSet<int>(musician.Plays.Select(i => i.InstrumentID));
            var viewModel = new List<PlaysVM>();
            foreach(var ins in allInstruments)
            {
                viewModel.Add(new PlaysVM
                {
                    InstrumentID = ins.ID,
                    InstrumentName = ins.Name,
                    Assigned = mInstruments.Contains(ins.ID)
                });
            }
            ViewData["Instruments"] = viewModel;
        }

        private void UpdatePlays(string[] selectedInstruments, Musician musicianToUpdate)
        {
            if(selectedInstruments == null)
            {
                musicianToUpdate.Plays = new List<Plays>();
                return;
            }

            var selectedInstrumentsHS = new HashSet<string>(selectedInstruments);
            var playsHS = new HashSet<int>(musicianToUpdate.Plays.Select(i => i.InstrumentID));
            foreach(var ins in _context.Instruments)
            {
                if (selectedInstrumentsHS.Contains(ins.ID.ToString()))
                {
                    if (!playsHS.Contains(ins.ID))
                    {
                        musicianToUpdate.Plays.Add(new Plays { MusicianID = musicianToUpdate.ID, InstrumentID = ins.ID });
                    }
                }
                else
                {
                    if (playsHS.Contains(ins.ID))
                    {
                        Plays playsToRemove = musicianToUpdate.Plays.SingleOrDefault(i => i.InstrumentID == ins.ID);
                        _context.Remove(playsToRemove);
                    }
                }
            }
        }

        private bool MusicianExists(int id)
        {
            return _context.Musicians.Any(e => e.ID == id);
        }
    }
}
