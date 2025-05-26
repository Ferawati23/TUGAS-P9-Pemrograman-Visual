using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeApp.Models;

namespace ShoeApp.Controllers
{
    public class ShoesController : Controller
    {
        private readonly AppDbContext _context;

        public ShoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Shoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shoes.ToListAsync());
        }

        // GET: Shoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shoe shoe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoe);
        }

        // GET: Shoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var shoe = await _context.Shoes.FindAsync(id);
            if (shoe == null)
                return NotFound();

            return View(shoe);
        }

        // POST: Shoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Shoe shoe)
        {
            if (id != shoe.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoeExists(shoe.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shoe);
        }

        // GET: Shoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var shoe = await _context.Shoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoe == null)
                return NotFound();

            return View(shoe);
        }

        // POST: Shoes/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoe = await _context.Shoes.FindAsync(id);
            if (shoe != null)
            {
                _context.Shoes.Remove(shoe);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ShoeExists(int id)
        {
            return _context.Shoes.Any(e => e.Id == id);
        }
    }
}
