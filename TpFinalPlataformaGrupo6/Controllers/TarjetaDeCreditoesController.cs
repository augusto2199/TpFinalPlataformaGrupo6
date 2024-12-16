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
    public class TarjetaDeCreditoesController : Controller
    {
        private readonly MyContext _context;

        public TarjetaDeCreditoesController(MyContext context)
        {
            _context = context;
        }

        // GET: TarjetaDeCreditoes
        public async Task<IActionResult> Index()
        {
            var myContext = _context.tarjetaCredito.Include(t => t.titular);
            return View(await myContext.ToListAsync());
        }

        // GET: TarjetaDeCreditoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tarjetaCredito == null)
            {
                return NotFound();
            }

            var tarjetaDeCredito = await _context.tarjetaCredito
                .Include(t => t.titular)
                .FirstOrDefaultAsync(m => m.id == id);
            if (tarjetaDeCredito == null)
            {
                return NotFound();
            }

            return View(tarjetaDeCredito);
        }

        // GET: TarjetaDeCreditoes/Create
        public IActionResult Create()
        {
            ViewData["num_usr"] = new SelectList(_context.usuarios, "id_usuario", "apellido");
            return View();
        }

        // POST: TarjetaDeCreditoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,num_usr,numero,codigoV,limite,consumos")] TarjetaDeCredito tarjetaDeCredito)
        {

                _context.Add(tarjetaDeCredito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: TarjetaDeCreditoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tarjetaCredito == null)
            {
                return NotFound();
            }

            var tarjetaDeCredito = await _context.tarjetaCredito.FindAsync(id);
            if (tarjetaDeCredito == null)
            {
                return NotFound();
            }
            ViewData["num_usr"] = new SelectList(_context.usuarios, "id_usuario", "apellido", tarjetaDeCredito.num_usr);
            return View(tarjetaDeCredito);
        }

        // POST: TarjetaDeCreditoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,num_usr,numero,codigoV,limite,consumos")] TarjetaDeCredito tarjetaDeCredito)
        {
            if (id != tarjetaDeCredito.id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(tarjetaDeCredito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarjetaDeCreditoExists(tarjetaDeCredito.id))
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

        // GET: TarjetaDeCreditoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tarjetaCredito == null)
            {
                return NotFound();
            }

            var tarjetaDeCredito = await _context.tarjetaCredito
                .Include(t => t.titular)
                .FirstOrDefaultAsync(m => m.id == id);
            if (tarjetaDeCredito == null)
            {
                return NotFound();
            }

            return View(tarjetaDeCredito);
        }

        // POST: TarjetaDeCreditoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.tarjetaCredito == null)
            {
                return Problem("Entity set 'MyContext.tarjetaCredito'  is null.");
            }
            var tarjetaDeCredito = await _context.tarjetaCredito.FindAsync(id);
            if (tarjetaDeCredito != null)
            {
                _context.tarjetaCredito.Remove(tarjetaDeCredito);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarjetaDeCreditoExists(int id)
        {
            return (_context.tarjetaCredito?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
