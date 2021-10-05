using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjAM_seminarski.ViewModel
{
    public class GetAllRentsViewModel
    {
        public int RentId { get; set; }
        public string User{ get; set; }
        public string Car { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsVerified{ get; set; }
    }
}
