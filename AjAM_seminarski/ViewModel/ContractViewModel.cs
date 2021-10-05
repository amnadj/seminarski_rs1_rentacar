using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AjAM_seminarski.ViewModel
{
    public class ContractViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        [DisplayName("Final Price")]
        public float FinalPrice { get; set; }
        public int RentId { get; set; }
        public int CarId { get; set; }
        public List<CarsRentViewModel> CarsRent { get; set; }
        public List<SelectListItem> Cars { get; set; }
        public List<CarViewModel> Car { get; set; }


        public ContractViewModel()
        {
            CarsRent = new List<CarsRentViewModel>();
            Car = new List<CarViewModel>();
        }
    }
}
