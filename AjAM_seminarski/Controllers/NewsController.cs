using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AjAM_seminarski.Data;
using AjAM_seminarski.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AjAM_seminarski.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public NewsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;

        }
        public IActionResult Index()
        {
            ViewData["deleteStatus"] = TempData["deleteStatus"];
            var model = _context.News.OrderByDescending(d => d.CreatedAt).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(NewsViewModel model)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(w => w.Id == _userManager.GetUserId(HttpContext.User));
            News newNews = new News()
            {
                Content = model.Content,
                CreatedAt = DateTime.Now,
                CreatedBy = user.UserName.Substring(0, user.UserName.IndexOf("@")),
                Summary = (model.Content.Length > 80 ? model.Content.Substring(0, 80) : model.Content)+ "...",
                Title = model.Title,
            };


            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                var uniqueName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filepath = Path.Combine(uploadsFolder, uniqueName);
                string url = $"/images/{uniqueName}";
                newNews.ImagePath = url;

                using (FileStream fs = new FileStream(filepath, FileMode.Create))
                {
                    model.Image.CopyTo(fs);
                    fs.Flush();
                }

            }
                await _context.News.AddAsync(newNews);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            NewsViewModel model = _context.News.Where(w => w.Id == id).Select(s=> new NewsViewModel() { 
                Content = s.Content,
                CreatedAt = s.CreatedAt,
                ImagePath = s.ImagePath,
                Title = s.Title
            }).Single();
            return View(model);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id != 0)
            {
                var news = _context.News.Find(id);
                if(news != null)
                {
                    FileInfo file = new FileInfo(_hostingEnvironment.WebRootPath + news.ImagePath);
                    file.Delete();
                    _context.News.Remove(news);
                    await _context.SaveChangesAsync();
                    TempData["deleteStatus"] = "Successfully deleted";
                }
                else
                {
                    TempData["deleteStatus"] = "Failed!";
                }
            }
            return RedirectToAction("Index");
        }
    }
}
