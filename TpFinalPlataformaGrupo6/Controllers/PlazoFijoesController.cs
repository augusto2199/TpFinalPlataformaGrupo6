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
    public class PlazoFijoesController : Controller
    {
        private readonly MyContext _context;

        public PlazoFijoesController(MyContext context)
        {
            _context = context;
        }

        // GET: PlazoFijoes
        public async Task<IActionResult> Index()
        {
            var myContext = _context.plazoFijo.Include(p => p.titular);
            return View(await myContext.ToListAsync());
        }

        // GET: PlazoFijoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.plazoFijo == null)
            {
                return NotFound();
            }

            var plazoFijo = await _context.plazoFijo
                .Include(p => p.titular)
                .FirstOrDefaultAsync(m => m.id == id);
            if (plazoFijo == null)
            {
                return NotFound();
            }

            return View(plazoFijo);
        }

        // GET: PlazoFijoes/Create
        public IActionResult Create()
        {
            ViewData["num_usr"] = new SelectList(_context.usuarios, "id_usuario", "apellido");
            return View();
        }

        // POST: PlazoFijoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,num_usr,monto,fechaIni,fechaFin,tasa,pagado")] PlazoFijo plazoFijo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plazoFijo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["num_usr"] = new SelectList(_context.usuarios, "id_usuario", "apellido", plazoFijo.num_usr);
            return View(plazoFijo);
        }

        // GET: PlazoFijoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.plazoFijo == null)
            {
                return NotFound();
            }

            var plazoFijo = await _context.plazoFijo.FindAsync(id);
            if (plazoFijo == null)
            {
                return NotFound();
            }
            ViewData["num_usr"] = new SelectList(_context.usuarios, "id_usuario", "apellido", plazoFijo.num_usr);
            return View(plazoFijo);
        }

        // POST: PlazoFijoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,num_usr,monto,fechaIni,fechaFin,tasa,pagado")] PlazoFijo plazoFijo)
        {
            if (id != plazoFijo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plazoFijo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlazoFijoExists(plazoFijo.id))
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
            ViewData["num_usr"] = new SelectList(_context.usuarios, "id_usuario", "apellido", plazoFijo.num_usr);
            return View(plazoFijo);
        }

        // GET: PlazoFijoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.plazoFijo == null)
            {
                return NotFound();
            }

            var plazoFijo = await _context.plazoFijo
                .Include(p => p.titular)
                .FirstOrDefaultAsync(m => m.id == id);
            if (plazoFijo == null)
            {
                return NotFound();
            }

            return View(plazoFijo);
        }

        // POST: PlazoFijoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.plazoFijo == null)
            {
                return Problem("Entity set 'MyContext.plazoFijo'  is null.");
            }
            var plazoFijo = await _context.plazoFijo.FindAsync(id);
            if (plazoFijo != null)
            {
                _context.plazoFijo.Remove(plazoFijo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlazoFijoExists(int id)
        {
            return (_context.plazoFijo?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
