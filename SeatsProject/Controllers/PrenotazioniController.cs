﻿namespace SeatsProject.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query.Internal;
    using SeatsProject.Models;

    public class PrenotazioniController : Controller
    {
        private readonly SeatsProjectContext _context;
        private string[] caratteri = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public PrenotazioniController(SeatsProjectContext context)
        {
            _context = context;
        }

        // GET: Prenotazioni

        public IActionResult GetDayReservations(DateTime date)
        {
            var lstDayReservations = _context.Prenotazioni.Where(p => p.Data == date);
            var lstBookableSeats = _context.Sedi.Where(p => p.Prenotabile == true);
            //var lstUser = _context.Login.Where();

            return Json(new { data = lstDayReservations });
        }

        public void Reserve(Prenotazioni prenotazione)
        {
            var alreadyReservedASeat = OneReservationForDayCheck(prenotazione.Utente, prenotazione.Data);
            var available = AvailabilityCheck(prenotazione);
            if (available && !alreadyReservedASeat) // here we check if the seat is available and you haven't done any reservation for this day yet.
            {
                GetIdPrenotazione(prenotazione);

                // devo aggiungere la data e l'utente
                _context.Prenotazioni.Add(prenotazione);
                _context.SaveChanges();
            }
            else
            {
                if (!available && alreadyReservedASeat) // here we check if the seat is not available and you have already done a reservation for this day.
                {
                    CheckAndDiscardReservation(prenotazione);
                }
                else
                {
                    if (!available || alreadyReservedASeat)
                    {
                        ViewBag.ErrorMessage = "questo posto è già stato prenotato per questa data oppure hai già prenotato un posto per la data di oggi.";
                        Console.Error.WriteLine("questo posto è già stato prenotato per questa data oppure hai già prenotato un posto per la data di oggi.");

                    }
                }

                // dovrei fare in modo di mandare un messaggio di errore per dire che questo posto è già prenotato per la data selezionata.
            }
        }

        public void CheckAndDiscardReservation(Prenotazioni prenotazione) // here we discard the reservation from the DB if the seat clicked by the user is not available that day and has been booked from the user himself.
        {
            var MiaPrenotazione = _context.Prenotazioni.FirstOrDefault(p => p.Data == prenotazione.Data && p.Utente == prenotazione.Utente && p.CodicePostazione == prenotazione.CodicePostazione);
            if (MiaPrenotazione != null)
            {
                _context.Prenotazioni.Remove(MiaPrenotazione);
                _context.SaveChanges();
            }
        }

        public bool OneReservationForDayCheck(string user, DateTime date) // here we check that the user hasn't already made a reservation for that day
        {
            var reservation = _context.Prenotazioni.FirstOrDefault(p => p.Data == date && p.Utente == user);
            var alreadyReserved = true;
            if (reservation == null)
            {
                return !alreadyReserved;
            }
            else
            {
                return alreadyReserved;
            }

        }

        public bool AvailabilityCheck(Prenotazioni prenotazione)// this method is useful to check if already exists an book of a specific seat in a specific date.
        {
            var postoOccupato = _context.Prenotazioni.FirstOrDefault(m => m.Data == prenotazione.Data && m.CodicePostazione == prenotazione.CodicePostazione);
            var SeatAvailable = true;
            if (postoOccupato == null)
            {
                return SeatAvailable;
            }
            else
            {
                return !SeatAvailable;
            }
        }

        public void GetIdPrenotazione(Prenotazioni prenotazione) // here we create a unique IdPrenotazione for every book and we add it at teh list to control all. 
        {
            string IdPrenotazione = string.Empty;
            for (int i = 0; i < 16; i++)
            {
                Random rnd = new Random(); int indice = rnd.Next(0, caratteri.Length);
                IdPrenotazione += caratteri[indice];
            }

            var uniqueId = _context.Prenotazioni.FirstOrDefault(Id => Id.CodicePrenotazione == IdPrenotazione);
            if (uniqueId == null)
            {
                prenotazione.CodicePrenotazione = IdPrenotazione;
            }
            else
            {
                GetIdPrenotazione(prenotazione);
            }
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