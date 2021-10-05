using Microsoft.AspNetCore.Identity;
using AjAM_seminarski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjAM_seminarski.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string ProfilePhotoUrl { get; set; }
       


    }
}
