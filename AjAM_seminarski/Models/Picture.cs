using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AjAM_seminarski.Data
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
