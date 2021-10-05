using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AjAM_seminarski.Data;
using AjAM_seminarski.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Mail;

namespace AjAM_seminarski.Controllers
{
    public class RentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["porka"] = TempData["nesto"];
            return View();
        }
        public bool IsAvailable(int carId, DateTime fromDate, DateTime toDate)
        {
            var cars = _context.Rents.Where(s => s.CarId == carId);
            foreach (var item in cars)
            {
                if(toDate > item.From && toDate <= item.To)
                {
                    return false;
                }
                if(fromDate >= item.From && fromDate<= item.To)
                {
                    return false;
                }
            }
            return true;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(RentViewmodel model)
        {
            if(model.ToDate > model.fromDate && model.fromDate >= DateTime.Now)
            {
                if(model.CarId != 0 && model.fromDate != new DateTime() && model.ToDate != new DateTime())
                {
                    if (IsAvailable(model.CarId, model.fromDate,model.ToDate))
                    {
                        Rent r = new Rent()
                        {
                            CarId = model.CarId,
                            ClientId = _userManager.GetUserId(HttpContext.User),
                            From =  model.fromDate,
                            To =  model.ToDate,
                            Verified = false
                        };
                        _context.Rents.Add(r);
                        await _context.SaveChangesAsync();
                        TempData["success"] = "success";
                    }
                    else
                    {
                        var s = _context.Rents.Where(s => s.CarId == model.CarId).OrderByDescending(r => r.To).FirstOrDefault();
                        var nextFree = s.To.AddDays(1);
                        TempData["success"] = "Car is not available in that period. Date that car is free is: " + nextFree.ToShortDateString();
                    }
                }
            }
            else
            {
                TempData["success"] = "failed";
            }
            return RedirectToAction("Index", "User");
        }
        [Authorize]
        public IActionResult GetAll()
        {

            ViewData["AlreadyVerified"] = TempData["Message"];
            if (User.IsInRole("Admin"))
            {
                List<GetAllRentsViewModel> model = _context.Rents.Select(s => new GetAllRentsViewModel()
                {
                    FromDate = s.From,
                    ToDate = s.To,
                    IsVerified = s.Verified,
                    RentId = s.Id,
                    User = _context.ApplicationUsers.FirstOrDefault(e=>e.Id == s.ClientId).UserName,
                    Car = _context.Cars.FirstOrDefault(r => r.Id == s.CarId).Name
                }).ToList();

                return View(model);
            }
            ViewData["Message"] = "You don't have a permission!";
            return View("AuthError");
        }

        public async Task<IActionResult> VerifyAsync(int Id)
        {
            
            var rent = _context.Rents.FirstOrDefault(s=>s.Id == Id);
            if (!rent.Verified)
            {
                var car = _context.Cars.FirstOrDefault(s => s.Id == rent.CarId);
                float totalPrice = (rent.To - rent.From).Days * car.Price;
                Contract newContract = new Contract()
                {
                    Date = DateTime.Now,
                    Description = "",
                    RentId = Id,
                    FinalPrice = totalPrice,
                };
                rent.Verified = true;
                _context.Rents.Update(rent);
                _context.Contracts.Add(newContract);
                _context.SaveChanges();
                string sendTo = _context.ApplicationUsers.FirstOrDefault(e => e.Id == rent.ClientId).Email;
                var message = new MailMessage();
                message.To.Add(new MailAddress(sendTo));  // replace with valid value 
                message.From = new MailAddress("frpunlock3rr@gmail.com");  // replace with valid value
                message.Subject = "::Rent confirmation::";
                message.Body = "<h1>Your car is waiting for you.</h1><p>Your reservation is approved. Your car is reserved at dates shown below.</p><h3>From: " + rent.From.ToShortDateString() + "</h3><h3>To: " + rent.To.ToShortDateString() + "</h3><h3>Car: " + car.Name + "</h3> <h3>Total price: " + totalPrice+ "BAM </h3>";
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("frpunlock3rr@gmail.com", "frpunlocker123");
                await smtp.SendMailAsync(message);
                return RedirectToAction("GetAll");
            }
            else
            {
                TempData["Message"] = "Already verified!";
                return RedirectToAction("GetAll");

            }
        }

    }
}
