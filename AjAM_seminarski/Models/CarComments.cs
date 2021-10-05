using AjAM_seminarski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjAM_seminarski.Models
{
    public class CarComments
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public DateTime PublishedDate { get; set; }
        public int CarId { get; set; }
        public Car Cars { get; set; }
        public int Rating { get; set; }
    }
}
