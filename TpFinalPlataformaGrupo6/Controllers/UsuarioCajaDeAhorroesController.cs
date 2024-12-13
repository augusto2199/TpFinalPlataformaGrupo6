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
    public class UsuarioCajaDeAhorroesController : Controller
    {
        private readonly MyContext _context;

        public UsuarioCajaDeAhorroesController(MyContext context)
        {
            _context = context;
        }

        // GET: UsuarioCajaDeAhorroes
        public async Task<IActionResult> Index()
        {
            var myContext = _context.UsuarioCajaDeAhorro.Include(u => u.cajaAhorro).Include(u => u.usuario);
            return View(await myContext.ToListAsync());
        }

        // GET: UsuarioCajaDeAhorroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuarioCajaDeAhorro == null)
            {
                return NotFound();
            }

            var usuarioCajaDeAhorro = await _context.UsuarioCajaDeAhorro
                .Include(u => u.cajaAhorro)
                .Include(u => u.usuario)
                .FirstOrDefaultAsync(m => m.id_UsuarioCaja == id);
            if (usuarioCajaDeAhorro == null)
            {
                return NotFound();
            }

            return View(usuarioCajaDeAhorro);
        }

        // GET: UsuarioCajaDeAhorroes/Create
        public IActionResult Create()
        {
            ViewData["fk_cajaAhorro"] = new SelectList(_context.cajaAhorro, "id", "id");
            ViewData["fk_usuario"] = new SelectList(_context.usuarios, "id_usuario", "apellido");
            return View();
        }

        // POST: UsuarioCajaDeAhorroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_UsuarioCaja,fk_usuario,fk_cajaAhorro")] UsuarioCajaDeAhorro usuarioCajaDeAhorro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioCajaDeAhorro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["fk_cajaAhorro"] = new SelectList(_context.cajaAhorro, "id", "id", usuarioCajaDeAhorro.fk_cajaAhorro);
            ViewData["fk_usuario"] = new SelectList(_context.usuarios, "id_usuario", "apellido", usuarioCajaDeAhorro.fk_usuario);
            return View(usuarioCajaDeAhorro);
        }

        // GET: UsuarioCajaDeAhorroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuarioCajaDeAhorro == null)
            {
                return NotFound();
            }

            var usuarioCajaDeAhorro = await _context.UsuarioCajaDeAhorro.FindAsync(id);
            if (usuarioCajaDeAhorro == null)
            {
                return NotFound();
            }
            ViewData["fk_cajaAhorro"] = new SelectList(_context.cajaAhorro, "id", "id", usuarioCajaDeAhorro.fk_cajaAhorro);
            ViewData["fk_usuario"] = new SelectList(_context.usuarios, "id_usuario", "apellido", usuarioCajaDeAhorro.fk_usuario);
            return View(usuarioCajaDeAhorro);
        }

        // POST: UsuarioCajaDeAhorroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_UsuarioCaja,fk_usuario,fk_cajaAhorro")] UsuarioCajaDeAhorro usuarioCajaDeAhorro)
        {
            if (id != usuarioCajaDeAhorro.id_UsuarioCaja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioCajaDeAhorro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioCajaDeAhorroExists(usuarioCajaDeAhorro.id_UsuarioCaja))
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
            ViewData["fk_cajaAhorro"] = new SelectList(_context.cajaAhorro, "id", "id", usuarioCajaDeAhorro.fk_cajaAhorro);
            ViewData["fk_usuario"] = new SelectList(_context.usuarios, "id_usuario", "apellido", usuarioCajaDeAhorro.fk_usuario);
            return View(usuarioCajaDeAhorro);
        }

        // GET: UsuarioCajaDeAhorroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuarioCajaDeAhorro == null)
            {
                return NotFound();
            }

            var usuarioCajaDeAhorro = await _context.UsuarioCajaDeAhorro
                .Include(u => u.cajaAhorro)
                .Include(u => u.usuario)
                .FirstOrDefaultAsync(m => m.id_UsuarioCaja == id);
            if (usuarioCajaDeAhorro == null)
            {
                return NotFound();
            }

            return View(usuarioCajaDeAhorro);
        }

        // POST: UsuarioCajaDeAhorroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuarioCajaDeAhorro == null)
            {
                return Problem("Entity set 'MyContext.UsuarioCajaDeAhorro'  is null.");
            }
            var usuarioCajaDeAhorro = await _context.UsuarioCajaDeAhorro.FindAsync(id);
            if (usuarioCajaDeAhorro != null)
            {
                _context.UsuarioCajaDeAhorro.Remove(usuarioCajaDeAhorro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioCajaDeAhorroExists(int id)
        {
            return (_context.UsuarioCajaDeAhorro?.Any(e => e.id_UsuarioCaja == id)).GetValueOrDefault();
        }
    }
}
