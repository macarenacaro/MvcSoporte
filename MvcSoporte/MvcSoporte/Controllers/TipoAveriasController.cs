﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcSoporte.Data;
using MvcSoporte.Models;

namespace MvcSoporte.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TipoAveriasController : Controller
    {
        private readonly MvcSoporteContexto _context;

        public TipoAveriasController(MvcSoporteContexto context)
        {
            _context = context;
        }

        // GET: TipoAverias
        public async Task<IActionResult> Index()
        {
              return _context.TipoAverias != null ? 
                          View(await _context.TipoAverias.ToListAsync()) :
                          Problem("Entity set 'MvcSoporteContexto.TipoAverias'  is null.");
        }

        // GET: TipoAverias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoAverias == null)
            {
                return NotFound();
            }

            var tipoAveria = await _context.TipoAverias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoAveria == null)
            {
                return NotFound();
            }

            return View(tipoAveria);
        }

        // GET: TipoAverias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoAverias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] TipoAveria tipoAveria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoAveria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoAveria);
        }

        // GET: TipoAverias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoAverias == null)
            {
                return NotFound();
            }

            var tipoAveria = await _context.TipoAverias.FindAsync(id);
            if (tipoAveria == null)
            {
                return NotFound();
            }
            return View(tipoAveria);
        }

        // POST: TipoAverias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] TipoAveria tipoAveria)
        {
            if (id != tipoAveria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoAveria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoAveriaExists(tipoAveria.Id))
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
            return View(tipoAveria);
        }

        // GET: TipoAverias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoAverias == null)
            {
                return NotFound();
            }

            var tipoAveria = await _context.TipoAverias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoAveria == null)
            {
                return NotFound();
            }

            return View(tipoAveria);
        }

        // POST: TipoAverias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoAverias == null)
            {
                return Problem("Entity set 'MvcSoporteContexto.TipoAverias'  is null.");
            }
            var tipoAveria = await _context.TipoAverias.FindAsync(id);
            if (tipoAveria != null)
            {
                _context.TipoAverias.Remove(tipoAveria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoAveriaExists(int id)
        {
          return (_context.TipoAverias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
