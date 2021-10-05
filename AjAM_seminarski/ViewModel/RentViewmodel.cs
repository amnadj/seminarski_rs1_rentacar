using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjAM_seminarski.ViewModel
{
    public class RentViewmodel
    {
        public DateTime fromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CarId { get; set; }
        public List<SelectListItem> Cars { get; set; }
    }
}
