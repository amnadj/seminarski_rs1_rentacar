using AjAM_seminarski.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AjAM_seminarski.Data
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<CarComments> CarComments { get; set; }

    }
}
