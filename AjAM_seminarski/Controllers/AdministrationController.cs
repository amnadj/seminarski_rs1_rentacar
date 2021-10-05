using AjAM_seminarski.Data;
using AjAM_seminarski.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AjAM_seminarski.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class AdministrationController : Controller
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;
        private readonly ApplicationDbContext _context;

       
        DbContextOptionsBuilder<ApplicationDbContext> _optionsBuilder;

        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly UserManager<ApplicationUser> userManager;
        [HttpGet]
        public IActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 4;
           var cars = _context.Cars.ToPagedList(pageNumber,pageSize);
            return View(cars);
        }
        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManagerr, IConfiguration configuration, ApplicationDbContext context)
        {
            this.RoleManager = roleManager;
            userManager = userManagerr;
            _context = context;
            _configuration = configuration;
            _optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _optionsBuilder.UseSqlServer(_connectionString);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        public async Task<IActionResult> RemoveRole(IdentityRole role)
        {
            var selectedRole = await RoleManager.FindByIdAsync(role.Id);
            await RoleManager.DeleteAsync(selectedRole);
            ViewBag.Message = $"Succesfully deleted {selectedRole.Name} role";
            return View("Success");
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await RoleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = RoleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await RoleManager.FindByIdAsync(Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {Id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in await userManager.GetUsersInRoleAsync(role.Name))
            {

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }


            }
            //aoko je u ovoj roli izbacice ga
            ViewBag.Message = TempData["UserAdded"];
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel modell)
        {
            var role = await RoleManager.FindByIdAsync(modell.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {modell.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = modell.RoleName;
                var result = await RoleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    ViewBag.Message = $"Successfully updated role";
                    //return RedirectToAction("EditRole");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(modell);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await RoleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} can not be found";
                return View("Not Found");
            }

            var model = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await RoleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} can not be found.";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    TempData["UserAdded"] = $"Successfully added users to {role.Name} role!";
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    TempData["UserAdded"] = $"Successfully removed users from {role.Name} role!";
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);

                }
                else
                {
                    continue;
                }

            }
            return RedirectToAction("EditRole", new { Id = roleId });

        }

        
    }
}
