using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjAM_seminarski.ViewModel
{
    public class CarsRentViewModel
    {
        public IEnumerable<CarViewModel> Cars { get; set; }
        public RentViewmodel Rent { get; set; }

        public CarsRentViewModel()
        {
            Cars = new List<CarViewModel>();
        }
    }
}
