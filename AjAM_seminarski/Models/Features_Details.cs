using AjAM_seminarski.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AjAM_seminarski.Data
{
    public class Features_Details
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


    }
}
