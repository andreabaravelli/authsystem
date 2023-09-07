namespace SeatsProject.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using SeatsProject.Models;

    public class PrenotazioniController : Controller
    {
        private readonly SeatsProjectContext _context;
        private string[] caratteri = new string[]{"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","0","1","2","3","4","5","6","7","8","9" };

        public PrenotazioniController(SeatsProjectContext context)
        {
            _context = context;
        }

        // GET: Prenotazioni
        public void Reserve(Prenotazioni prenotazione)
        {
            // qui bisogna aggiungere al database la prenotazione. si può fare passando al metodo una struct in javascript

            GetIdPrenotazione(prenotazione);

            // devo aggiungere la data e l'utente
            _context.Add(prenotazione);
            _context.SaveChanges(); 
        }

        public void GetIdPrenotazione(Prenotazioni prenotazione)
        {
            string IdPrenotazione = string.Empty;
            for (int i=0; i<16; i++)
            {
                Random rnd = new Random(); int indice = rnd.Next(0,caratteri.Length);
                IdPrenotazione += caratteri[indice];
            }
            prenotazione.CodicePrenotazione = IdPrenotazione;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Prenotazioni != null ?
                        View(await _context.Prenotazioni.ToListAsync()) :
                        Problem("Entity set 'SeatsProjectContext.Prenotazioni'  is null.");
        }

        // GET: Prenotazioni/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prenotazioni == null)
            {
                return NotFound();
            }

            var prenotazioni = await _context.Prenotazioni
                .FirstOrDefaultAsync(m => m.IdPrenotazine == id);
            if (prenotazioni == null)
            {
                return NotFound();
            }

            return View(prenotazioni);
        }

        // GET: Prenotazioni/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prenotazioni/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPrenotazine,Data,Utente,CodicePostazione")] Prenotazioni prenotazioni)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prenotazioni);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(prenotazioni);
        }

        // GET: Prenotazioni/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prenotazioni == null)
            {
                return NotFound();
            }

            var prenotazioni = await _context.Prenotazioni.FindAsync(id);
            if (prenotazioni == null)
            {
                return NotFound();
            }
            return View(prenotazioni);
        }

        // POST: Prenotazioni/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPrenotazine,Data,Utente,CodicePostazione")] Prenotazioni prenotazioni)
        {
            if (id != prenotazioni.IdPrenotazine)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prenotazioni);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrenotazioniExists(prenotazioni.IdPrenotazine))
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
            return View(prenotazioni);
        }

        // GET: Prenotazioni/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prenotazioni == null)
            {
                return NotFound();
            }

            var prenotazioni = await _context.Prenotazioni
                .FirstOrDefaultAsync(m => m.IdPrenotazine == id);
            if (prenotazioni == null)
            {
                return NotFound();
            }

            return View(prenotazioni);
        }

        // POST: Prenotazioni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prenotazioni == null)
            {
                return Problem("Entity set 'SeatsProjectContext.Prenotazioni'  is null.");
            }
            var prenotazioni = await _context.Prenotazioni.FindAsync(id);
            if (prenotazioni != null)
            {
                _context.Prenotazioni.Remove(prenotazioni);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrenotazioniExists(int id)
        {
            return (_context.Prenotazioni?.Any(e => e.IdPrenotazine == id)).GetValueOrDefault();
        }
    }
}
