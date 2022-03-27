using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _220318_OS_RestaurantMVC.Models;

namespace _220318_OS_RestaurantMVC.Controllers
{
    public class UrunMalzemelerController : Controller
    {
        private readonly RestaurantContext _context;

        public UrunMalzemelerController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: UrunMalzemeler
        public async Task<IActionResult> Index()
        {
            var restaurantContext = _context.UrunlerMalzemeler.Include(u => u.Malzeme).Include(u => u.Urun);
            return View(await restaurantContext.ToListAsync());
        }

        // GET: UrunMalzemeler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urunMalzeme = await _context.UrunlerMalzemeler
                .Include(u => u.Malzeme)
                .Include(u => u.Urun)
                .FirstOrDefaultAsync(m => m.UrunId == id);
            if (urunMalzeme == null)
            {
                return NotFound();
            }

            return View(urunMalzeme);
        }

        // GET: UrunMalzemeler/Create
        public IActionResult Create()
        {
            ViewData["MalzemeId"] = new SelectList(_context.Malzemeler, "MalzemeId", "MalzemeAdi");
            ViewData["UrunId"] = new SelectList(_context.Urunler, "UrunId", "UrunAdi");
            return View();
        }

        // POST: UrunMalzemeler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MalzemeId,UrunId")] UrunMalzeme urunMalzeme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urunMalzeme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MalzemeId"] = new SelectList(_context.Malzemeler, "MalzemeId", "MalzemeAdi", urunMalzeme.MalzemeId);
            ViewData["UrunId"] = new SelectList(_context.Urunler, "UrunId", "UrunAdi", urunMalzeme.UrunId);
            return View(urunMalzeme);
        }

        // GET: UrunMalzemeler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urunMalzeme = await _context.UrunlerMalzemeler.FindAsync(id);
            if (urunMalzeme == null)
            {
                return NotFound();
            }
            ViewData["MalzemeId"] = new SelectList(_context.Malzemeler, "MalzemeId", "MalzemeAdi", urunMalzeme.MalzemeId);
            ViewData["UrunId"] = new SelectList(_context.Urunler, "UrunId", "UrunAdi", urunMalzeme.UrunId);
            return View(urunMalzeme);
        }

        // POST: UrunMalzemeler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MalzemeId,UrunId")] UrunMalzeme urunMalzeme)
        {
            if (id != urunMalzeme.UrunId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urunMalzeme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunMalzemeExists(urunMalzeme.UrunId))
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
            ViewData["MalzemeId"] = new SelectList(_context.Malzemeler, "MalzemeId", "MalzemeAdi", urunMalzeme.MalzemeId);
            ViewData["UrunId"] = new SelectList(_context.Urunler, "UrunId", "UrunAdi", urunMalzeme.UrunId);
            return View(urunMalzeme);
        }

        // GET: UrunMalzemeler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urunMalzeme = await _context.UrunlerMalzemeler
                .Include(u => u.Malzeme)
                .Include(u => u.Urun)
                .FirstOrDefaultAsync(m => m.UrunId == id);
            if (urunMalzeme == null)
            {
                return NotFound();
            }

            return View(urunMalzeme);
        }

        // POST: UrunMalzemeler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var urunMalzeme = await _context.UrunlerMalzemeler.FindAsync(id);
            _context.UrunlerMalzemeler.Remove(urunMalzeme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunMalzemeExists(int id)
        {
            return _context.UrunlerMalzemeler.Any(e => e.UrunId == id);
        }
    }
}
