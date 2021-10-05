using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AjAM_seminarski.Data;
using AjAM_seminarski.ViewModel;
using AjAM_seminarski.Models;
using Microsoft.AspNetCore.Http.Features;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;
using System.Net;

namespace AjAM_seminarski.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public CarsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            ViewBag.Message = TempData["Message"];
            return View(await _context.Cars.ToListAsync());
        }

        // GET: Cars/Details/5
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

            var features = _context.Feature_Car_Details.Select(x => new Feature_Car_Details
            {
                ID = x.ID,
                CarId = x.CarId,
                FeatureId = x.FeatureId
            })
                       .Where(p => p.CarId == id)
                       .ToList();
            var featuresNames = _context.Features_Details.ToList();
            var model = new CarFeaturesDetailsViewModel();
            model.Features = new List<Features_Details>();
            model.CarId = car.Id;
            model.CarName = car.Name;
            model.Description = car.Description;
            model.Price = car.Price;
            model.Amount = car.Amount;
            model.Pictures = new List<Picture>();
            model.Category = _context.Categories.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name
            }).Where(p => p.Id == car.CategoryID).FirstOrDefault();
            model.Pictures = _context.Pictures.Select(x => new Picture
            {
                Id = x.Id,
                CarId = x.CarId,
                Url = x.Url
            }).Where(s => s.CarId == id).ToList();
            for (int i = 0; i < features.Count; i++)
            {
                for (int j = 0; +j < featuresNames.Count; j++)
                {
                    if(features[i].FeatureId == featuresNames[j].Id)
                    {
                       
                        var newFeature = new Features_Details()
                        {
                            Id = featuresNames[j].Id,
                            Name = featuresNames[j].Name
                        };
                        model.Features.Add(newFeature);
                    }
                }
            }

            ViewBag.Message = TempData["DelImgMessage"];
            return View(model);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Amount,Price")] Car car, IFormFile[] files)
        {
            Car newCar = new Car()
            {
                Amount = car.Amount,
                Description = car.Description,
                Name = car.Name,
                Price = car.Price,
                CategoryID = 1
                
            };

            _context.Add(newCar);
            await _context.SaveChangesAsync();

            if (files != null)
            {
                foreach(var item in files)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "CarsImages");
                    var uniqueName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    string filepath = Path.Combine(uploadsFolder, uniqueName);
                    string url = $"/CarsImages/{uniqueName}";
                    Picture newPic = new Picture()
                    {
                        CarId = newCar.Id,
                        Url = url
                    };

                    using (FileStream fs = new FileStream(filepath, FileMode.Create))
                    {
                        item.CopyTo(fs);
                        fs.Flush();
                    }

                    _context.Pictures.Add(newPic);
                }

            }

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Amount,Price,CategoryID")] Car car, IFormFile[] files)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (files != null)
            {
                foreach (var item in files)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "CarsImages");
                    var uniqueName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    string filepath = Path.Combine(uploadsFolder, uniqueName);
                    string url = $"/CarsImages/{uniqueName}";
                    Picture newPic = new Picture()
                    {
                        CarId = id,
                        Url = url
                    };

                    using (FileStream fs = new FileStream(filepath, FileMode.Create))
                    {
                        item.CopyTo(fs);
                        fs.Flush();
                    }

                    _context.Pictures.Add(newPic);
                }

            }
            await _context.SaveChangesAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
        [HttpGet]
        public IActionResult AddFeatures(int Id)
        {
            var model = new List<FeaturesViewModel>();
            var feature_car_details = _context.Feature_Car_Details.ToList();
            foreach (var feature in _context.Features_Details)
            {
                var featureViewModel = new FeaturesViewModel
                {
                    Id = feature.Id,
                    Name = feature.Name,
                    CarId = Id
                };
                foreach (var item in feature_car_details)
                {
                    if(item.CarId == Id && item.FeatureId == feature.Id)
                    {
                        featureViewModel.IsSelected = true;
                        break;
                    }
                    else { 
                        featureViewModel.IsSelected = false;
                    }
                }
                model.Add(featureViewModel);
            }


            return View(model);
        }
        public bool IsFeature(int featureId, int carId)
        {
            var features = _context.Feature_Car_Details.ToList();
            foreach (var item in features)
            {
                if (item.CarId == carId && item.FeatureId == featureId)
                    return true;
            }
            return false;
        }
        [HttpPost]
        public IActionResult InsertNewFeatures(List<FeaturesViewModel> model)
        {
            bool flag = false;
            for (int i = 0; i < model.Count; i++)
            {
                flag = false;
                if (model[i].IsSelected)
                {
                    var listdata = _context.Feature_Car_Details.ToList().Select(x => new Feature_Car_Details
                    {
                        ID = x.ID,
                        CarId = x.CarId,
                        FeatureId = x.FeatureId
                       })
                        .Where(p => p.FeatureId == model[i].Id)
                        .ToList();

                    for (int j = 0; j < listdata.Count; j++)
                    {
                        if (listdata[j].CarId == model[i].CarId && listdata[j].FeatureId == model[i].Id)
                        {
                            flag = true;
                            break;
                        }
                        
                    }

                    if (!flag)
                    {
                        var aaa = new Feature_Car_Details
                        {
                            CarId = model[i].CarId,
                            FeatureId = model[i].Id
                        };
                        TempData["Message"] = $"Features successfully added!";
                        _context.Feature_Car_Details.Add(aaa);

                    }
                    
                }else if(!model[i].IsSelected && IsFeature(model[i].Id, model[i].CarId))
                {
                    TempData["Message"] = $"Features successfully removed!";
                    _context.Feature_Car_Details.Remove(_context.Feature_Car_Details.Where(x => x.CarId == model[i].CarId).Where(x=>x.FeatureId == model[i].Id).FirstOrDefault());
                }
                else
                {
                    continue;
                }

            }
                    _context.SaveChanges();
            return RedirectToAction("Index", "Cars");
        }
        public IActionResult DeleteImage(int id)
        {
            var pic = _context.Pictures.Find(id);
                _context.Pictures.Remove(pic);
            _context.SaveChanges();
            TempData["DelImgMessage"] = "Image deleted!";
            return RedirectToAction("Details", new { id = pic.CarId });
           
        }

    }
}
