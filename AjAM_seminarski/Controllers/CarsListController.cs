using AjAM_seminarski.Data;
using AjAM_seminarski.Helper;
using AjAM_seminarski.Models;
using AjAM_seminarski.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjAM_seminarski.Controllers
{
    public class CarsListController : Controller
    {
        private readonly ApplicationDbContext _context;
    
        public CarsListController(ApplicationDbContext context)
        {
            _context = context;
 
        }

        public async Task<IActionResult> Index()
        {
            List<Car> data = await _context.Cars.ToListAsync();
            List<CarViewModel> model = new List<CarViewModel>();

            if (data == null)
                return View(model);

            foreach (var item in data)
            {
                CarViewModel tmp = new CarViewModel();

                tmp.Id = item.Id;
                tmp.Name = item.Name;
                tmp.Price = item.Price;
                tmp.Description = item.Description;
                tmp.Amount = item.Amount;
                tmp.CategoryID = item.CategoryID;

                tmp.Pictures = _context.Pictures.Select(x => new Picture
                {
                    Id = x.Id,
                    CarId = x.CarId,
                    Url = x.Url
                }).Where(s => s.CarId == tmp.Id).ToList();

                model.Add(tmp);
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            CarCommentViewModel cm = new CarCommentViewModel();

            cm.CarFeatures = new CarFeaturesDetailsViewModel()
            {
            Features = new List<Features_Details>(),
            CarId = car.Id,
            CarName = car.Name,
            Description = car.Description,
            Price = car.Price,
            Amount = car.Amount,
            Category = _context.Categories.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name
            }).Where(p => p.Id == car.CategoryID).FirstOrDefault(),
            Pictures = _context.Pictures.Select(x => new Picture
            {
                Id = x.Id,
                CarId = x.CarId,
                Url = x.Url
            }).Where(s => s.CarId == id).ToList()

        };

            cm.CarId = id.Value;
            cm.Title = car.Name;

            var Comments = _context.CarComments.Where(d => d.CarId.Equals(id.Value)).ToList();
            cm.ListOfComments = Comments;

            var ratings = _context.CarComments.Where(d => d.CarId.Equals(id.Value)).ToList();
            if(ratings.Count > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rating);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View(cm);
        }

        public async Task<IActionResult> SendEmail(string message, string ccMail)
        {

            MailHelper.SendMail("", message, ccMail);

            return RedirectToAction("Index", "CarsList");
        }

    }
}
