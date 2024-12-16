using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TpFinalPlataformaGrupo6.Date;
using TpFinalPlataformaGrupo6.Models;

namespace TpFinalPlataformaGrupo6.Controllers
{
    public class MovimientoesController : Controller
    {
        private readonly MyContext _context;

        public MovimientoesController(MyContext context)
        {
            _context = context;
        }

        // GET: Movimientoes
        public async Task<IActionResult> Index()
        {
            var myContext = _context.movimiento.Include(m => m.caja);
            return View(await myContext.ToListAsync());
        }

        // GET: Movimientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.movimiento == null)
            {
                return NotFound();
            }

            var movimiento = await _context.movimiento
                .Include(m => m.caja)
                .FirstOrDefaultAsync(m => m.id_movimiento == id);
            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // GET: Movimientoes/Create
        public IActionResult Create()
        {
            ViewData["num_caja"] = new SelectList(_context.cajaAhorro, "id", "id");
            return View();
        }

        // POST: Movimientoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_movimiento,num_caja,detalle,monto,fecha")] Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["num_caja"] = new SelectList(_context.cajaAhorro, "id", "id", movimiento.num_caja);
            return View(movimiento);
        }

        // GET: Movimientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.movimiento == null)
            {
                return NotFound();
            }

            var movimiento = await _context.movimiento.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            ViewData["num_caja"] = new SelectList(_context.cajaAhorro, "id", "id", movimiento.num_caja);
            return View(movimiento);
        }

        // POST: Movimientoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_movimiento,num_caja,detalle,monto,fecha")] Movimiento movimiento)
        {
            if (id != movimiento.id_movimiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimientoExists(movimiento.id_movimiento))
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
            ViewData["num_caja"] = new SelectList(_context.cajaAhorro, "id", "id", movimiento.num_caja);
            return View(movimiento);
        }

        // GET: Movimientoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.movimiento == null)
            {
                return NotFound();
            }

            var movimiento = await _context.movimiento
                .Include(m => m.caja)
                .FirstOrDefaultAsync(m => m.id_movimiento == id);
            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // POST: Movimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.movimiento == null)
            {
                return Problem("Entity set 'MyContext.movimiento'  is null.");
            }
            var movimiento = await _context.movimiento.FindAsync(id);
            if (movimiento != null)
            {
                _context.movimiento.Remove(movimiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimientoExists(int id)
        {
            return (_context.movimiento?.Any(e => e.id_movimiento == id)).GetValueOrDefault();
        }
    }
}
