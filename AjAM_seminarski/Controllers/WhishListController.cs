using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AjAM_seminarski.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AjAM_seminarski.Data;
using Microsoft.EntityFrameworkCore;

namespace AjAM_seminarski.Controllers
{

    public class WhishListController : Controller
    {
        private UserManager<AjAM_seminarski.Data.ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public WhishListController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var list = _context.Wishlists.Where(x => x.ClientId.Equals(user.Id)).Include(c => c.Car).Include(u => u.Client).ToList();

                return View(list);
            }
          
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AddItem(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var imamUBazi = _context.Wishlists.Where(x => x.ClientId.Equals(user.Id) && x.CarId == id).FirstOrDefault();

            if (user != null && imamUBazi == null)
            {
                Wishlist whishList = new Wishlist(); 
            
                whishList.CarId = id;
                whishList.ClientId = user.Id;
            

                _context.Wishlists.Add(whishList);
                _context.SaveChanges();
            
                return Json(true);              
            }
            
            return Json(false);
        }

    }
}
