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
    public class PagoesController : Controller
    {
        private readonly MyContext _context;

        public PagoesController(MyContext context)
        {
            _context = context;
        }

        // GET: Pagoes
        public async Task<IActionResult> Index()
        {
            var myContext = _context.pago.Include(p => p.usuario);
            return View(await myContext.ToListAsync());
        }

        // GET: Pagoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.pago == null)
            {
                return NotFound();
            }

            var pago = await _context.pago
                .Include(p => p.usuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (pago == null)
            {
                return NotFound();
            }

            return View(pago);
        }

        // GET: Pagoes/Create
        public IActionResult Create()
        {
            ViewData["num_usr"] = new SelectList(_context.usuarios, "id_usuario", "apellido");
            return View();
        }

        // POST: Pagoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,num_usr,nombre,monto,pagado,metodo")] Pago pago)
        {

                _context.Add(pago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Pagoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.pago == null)
            {
                return NotFound();
            }

            var pago = await _context.pago.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }
            ViewData["num_usr"] = new SelectList(_context.usuarios, "id_usuario", "apellido", pago.num_usr);
            return View(pago);
        }

        // POST: Pagoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,num_usr,nombre,monto,pagado,metodo")] Pago pago)
        {
            if (id != pago.id)
            {
                return NotFound();
            }


                try
                {
                    _context.Update(pago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagoExists(pago.id))
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

        // GET: Pagoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.pago == null)
            {
                return NotFound();
            }

            var pago = await _context.pago
                .Include(p => p.usuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (pago == null)
            {
                return NotFound();
            }

            return View(pago);
        }

        // POST: Pagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.pago == null)
            {
                return Problem("Entity set 'MyContext.pago'  is null.");
            }
            var pago = await _context.pago.FindAsync(id);
            if (pago != null)
            {
                _context.pago.Remove(pago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagoExists(int id)
        {
            return (_context.pago?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
