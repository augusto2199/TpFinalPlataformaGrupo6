using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TpFinalPlataformaGrupo6.Date;

namespace TpFinalPlataformaGrupo6.Models
{
    public class CajaDeAhorroesController : Controller
    {
        private readonly MyContext _context;

        public CajaDeAhorroesController(MyContext context)
        {
            _context = context;
        }

        // GET: CajaDeAhorroes
        public async Task<IActionResult> Index()
        {
              return _context.cajaAhorro != null ? 
                          View(await _context.cajaAhorro.ToListAsync()) :
                          Problem("Entity set 'MyContext.cajaAhorro'  is null.");
        }

        // GET: CajaDeAhorroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.cajaAhorro == null)
            {
                return NotFound();
            }

            var cajaDeAhorro = await _context.cajaAhorro
                .FirstOrDefaultAsync(m => m.id == id);
            if (cajaDeAhorro == null)
            {
                return NotFound();
            }

            return View(cajaDeAhorro);
        }

        // GET: CajaDeAhorroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CajaDeAhorroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,cbu,saldo")] CajaDeAhorro cajaDeAhorro)
        {

                _context.Add(cajaDeAhorro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: CajaDeAhorroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.cajaAhorro == null)
            {
                return NotFound();
            }

            var cajaDeAhorro = await _context.cajaAhorro.FindAsync(id);
            if (cajaDeAhorro == null)
            {
                return NotFound();
            }
            return View(cajaDeAhorro);
        }

        // POST: CajaDeAhorroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,cbu,saldo")] CajaDeAhorro cajaDeAhorro)
        {
            if (id != cajaDeAhorro.id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(cajaDeAhorro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CajaDeAhorroExists(cajaDeAhorro.id))
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

        // GET: CajaDeAhorroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.cajaAhorro == null)
            {
                return NotFound();
            }

            var cajaDeAhorro = await _context.cajaAhorro
                .FirstOrDefaultAsync(m => m.id == id);
            if (cajaDeAhorro == null)
            {
                return NotFound();
            }

            return View(cajaDeAhorro);
        }

        // POST: CajaDeAhorroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.cajaAhorro == null)
            {
                return Problem("Entity set 'MyContext.cajaAhorro'  is null.");
            }
            var cajaDeAhorro = await _context.cajaAhorro.FindAsync(id);
            if (cajaDeAhorro != null)
            {
                _context.cajaAhorro.Remove(cajaDeAhorro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CajaDeAhorroExists(int id)
        {
          return (_context.cajaAhorro?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
