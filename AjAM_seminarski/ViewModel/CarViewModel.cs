using AjAM_seminarski.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AjAM_seminarski.ViewModel
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        [Range(0, 999.99)]
        public float Price { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public List<Picture> Pictures { get; set; }

        public CarViewModel()
        {
            Pictures = new List<Picture>();
        }
    }
}