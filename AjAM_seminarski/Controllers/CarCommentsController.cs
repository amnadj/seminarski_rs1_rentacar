using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AjAM_seminarski.Data;
using AjAM_seminarski.Models;
using AjAM_seminarski.ViewModel;

namespace AjAM_seminarski.Controllers
{
    public class CarCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarComments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CarComments.Include(c => c.Cars);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CarCommentViewModel vm)
        {
            var comments = vm.Comment;
            var carId = vm.CarId;
            var rating = vm.Rating;

            CarComments carComments = new CarComments()
            {
                CarId = carId,
                Comments = comments,
                Rating = rating,
                PublishedDate = DateTime.Now
            };

            _context.CarComments.Add(carComments);
            _context.SaveChanges();

            return RedirectToAction("Details", "CarsList", new { id = carId });
        }

        // GET: CarComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carComments = await _context.CarComments
                .Include(c => c.Cars)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carComments == null)
            {
                return NotFound();
            }

            return View(carComments);
        }

        // GET: CarComments/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id");
            return View();
        }

        // POST: CarComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comments,PublishedDate,CarId,Rating")] CarComments carComments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carComments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carComments.CarId);
            return View(carComments);
        }

        // GET: CarComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carComments = await _context.CarComments.FindAsync(id);
            if (carComments == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carComments.CarId);
            return View(carComments);
        }

        // POST: CarComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comments,PublishedDate,CarId,Rating")] CarComments carComments)
        {
            if (id != carComments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carComments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarCommentsExists(carComments.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carComments.CarId);
            return View(carComments);
        }

        // GET: CarComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carComments = await _context.CarComments
                .Include(c => c.Cars)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carComments == null)
            {
                return NotFound();
            }

            return View(carComments);
        }

        // POST: CarComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carComments = await _context.CarComments.FindAsync(id);
            _context.CarComments.Remove(carComments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarCommentsExists(int id)
        {
            return _context.CarComments.Any(e => e.Id == id);
        }
    }
}
