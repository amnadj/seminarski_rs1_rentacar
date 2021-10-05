using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AjAM_seminarski.Models
{
    public class Feature_Car_Details
    {
        [Key]
        public int ID { get; set; }

        public int CarId { get; set; }
        public int FeatureId { get; set; }
    }
}
