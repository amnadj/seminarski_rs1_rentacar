using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AjAM_seminarski.Data;
using AjAM_seminarski.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AjAM_seminarski.Controllers
{
    public class ContractController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ContractController> _logger;


        public ContractController(ApplicationDbContext context, ILogger<ContractController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Contract> data = await _context.Contracts.OrderByDescending(c => c.Date).ToListAsync();
            List<ContractViewModel> model = new List<ContractViewModel>();

            if (data == null)
                return View(model);

            foreach (var item in data)
            {
                ContractViewModel tmp = new ContractViewModel();

                tmp.Id = item.Id;
                tmp.Date = item.Date;
                tmp.FinalPrice = item.FinalPrice;
                tmp.Description = item.Description;

                model.Add(tmp);
            }

            return View(model);
        }

        public IActionResult Create()
        {
            ContractViewModel model = new ContractViewModel();

            model.Cars = _context.Cars.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            model.Date = DateTime.Now;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ContractViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Contract data = new Contract();

            data.Id = model.Id;
            data.Date = model.Date;
            data.Description = model.Description;
            data.CarIdd = model.CarId;
            data.FinalPrice = model.FinalPrice;

            _context.Contracts.Add(data);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Error, ID is not supplied.";
                return RedirectToAction(nameof(Index));
            }

            Contract data = await _context.Contracts.FindAsync(id);

            if (data == null)
            {
                TempData["Error"] = "Provided ID doesn't exist.";
                return RedirectToAction(nameof(Index));
            }

            ContractViewModel model = new ContractViewModel();

            model.Id = data.Id;
            model.Description = data.Description;
            model.Date = data.Date;
            model.FinalPrice = data.FinalPrice;
            model.CarId = data.CarIdd;

            model.Cars = _context.Cars.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ContractViewModel model)
        {
            if (id != model.Id)
            {
                TempData["Error"] = "Error, ID is not supplied.";
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Contract data = await _context.Contracts.FindAsync(id);

                    if (data == null)
                    {
                        TempData["Error"] = "Provided ID doesn't exist.";
                        return RedirectToAction(nameof(Index));
                    }

                    data.Id = model.Id;
                    data.Date = model.Date;
                    data.Description = model.Description;
                    data.FinalPrice = model.FinalPrice;
                    data.CarIdd = model.CarId;

                    model.Cars = _context.Cars.Select(s => new SelectListItem()
                    {
                        Text = s.Name,
                        Value = s.Id.ToString()
                    }).ToList();

                    _context.Update(data);

                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Successfully edited Termin.";

                    return RedirectToAction(nameof(Edit), new { id = data.Id });
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Internal Server Error");
                    ModelState.AddModelError("error", "Changes not saved");
                    TempData["Error"] = "Update failed, please check errors.";
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Error, ID is not supplied.";
                return RedirectToAction(nameof(Index));
            }

            Contract data = await _context.Contracts
                .FirstOrDefaultAsync(t => t.Id == id);

            if (data == null)
            {
                TempData["Error"] = "Provided ID doesn't exist.";
                return RedirectToAction(nameof(Index));
            }

            ContractViewModel model = new ContractViewModel();

            model.Id = data.Id;
            model.FinalPrice = data.FinalPrice;
            model.Description = data.Description;
            model.Date = data.Date;
            model.CarId = data.CarIdd;

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Contract data = await _context.Contracts.FindAsync(id);

            _context.Contracts.Remove(data);

            await _context.SaveChangesAsync();

            TempData["Success"] = "Successfully deleted Termins.";

            return RedirectToAction(nameof(Index));
        }
    }
}
