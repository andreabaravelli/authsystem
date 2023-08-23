namespace SeatsProject.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using SeatsProject.Models;

    public class SedeController : Controller
    {
        private readonly SeatsProjectContext _context;

        public SedeController(SeatsProjectContext context)
        {
            _context = context;
        }

        // GET: Sede
        public IActionResult Index(int? IdPadre = null)
        {

            if (IdPadre == null)
            {
                var lstSedi = GetSedi();

                return View(lstSedi);
            }
            else
            {
                var lstSedi = GetSedi();

                return View(lstSedi.Where(l => l.IdPadre == IdPadre).ToList());
            }
        }

        public IActionResult GetJson()
        {
            List<Sede> sedi = GetSedi();
            return Json(new { data = sedi });
        }

        public List<Sede> GetSedi(int? IdPadre = null)
        {
            if (IdPadre == null)
            {
                var lstSedi = _context.Sedi.ToList();

                return lstSedi;
            }
            else
            {
                var lstSedi = _context.Sedi.Where(l => l.IdPadre == IdPadre).ToList();

                return lstSedi;
            }
        }

        [HttpPost]
        public Sede GetSeat(Sede item)
        {

            var seat = _context.Sedi.Where(c => c.CodiceArea == item.CodiceArea);

            return (Sede)seat;
        }

        // GET: Sede/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sedi == null)
            {
                return NotFound();
            }

            var sede = await _context.Sedi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sede == null)
            {
                return NotFound();
            }

            return View(sede);
        }

        // GET: Sede/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sede/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descrizione,CodiceArea,IdPadre,Prenotabile")] Sede sede)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sede);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sede);
        }

        // GET: Sede/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sedi == null)
            {
                return NotFound();
            }

            var sede = await _context.Sedi.FindAsync(id);
            if (sede == null)
            {
                return NotFound();
            }

            return View(sede);
        }

        // POST: Sede/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descrizione,CodiceArea,IdPadre,Prenotabile")] Sede sede)
        {
            if (id != sede.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sede);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SedeExists(sede.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(sede);
        }

        // GET: Sede/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sedi == null)
            {
                return NotFound();
            }

            var sede = await _context.Sedi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sede == null)
            {
                return NotFound();
            }

            return View(sede);
        }

        // POST: Sede/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sedi == null)
            {
                return Problem("Entity set 'SeatsProjectContext.Sedi'  is null.");
            }

            var sede = await _context.Sedi.FindAsync(id);
            if (sede != null)
            {
                _context.Sedi.Remove(sede);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SedeExists(int id)
        {
            return (_context.Sedi?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
