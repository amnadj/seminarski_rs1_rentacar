using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AjAM_seminarski.Data;
using AjAM_seminarski.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AjAM_seminarski.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["success"] = TempData["success"];
            CarsRentViewModel model = new CarsRentViewModel()
            {
                Rent = new RentViewmodel()
                {
                    Cars = _context.Cars.Select(s => new SelectListItem()
                    {
                        Text = s.Name,
                        Value = s.Id.ToString()
                    }).ToList()
                },
                Cars = _context.Cars.Select(c => new CarViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Amount = c.Amount,
                    Category = c.Category,
                    Description = c.Description,
                    Price = c.Price,
                    Pictures = _context.Pictures.Select(x => new Picture
                    {
                        Id = x.Id,
                        CarId = x.CarId,
                        Url = x.Url
                    }).Where(s => s.CarId == c.Id).ToList()
                })

            };
            
            return View(model);
        }

    }
}
