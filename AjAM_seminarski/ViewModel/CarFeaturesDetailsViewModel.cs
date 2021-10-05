using AjAM_seminarski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjAM_seminarski.ViewModel
{
    public class CarFeaturesDetailsViewModel
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public List<Features_Details> Features{ get; set; }
        public List<Picture> Pictures { get; set; }
        public Category Category { get; set; }

    }
}
