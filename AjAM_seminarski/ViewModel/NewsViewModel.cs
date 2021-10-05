using AjAM_seminarski.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjAM_seminarski.ViewModel
{
    public class NewsViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
    }
}
