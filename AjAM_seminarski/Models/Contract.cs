using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AjAM_seminarski.Data
{
    public class Contract
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public float FinalPrice { get; set; }
        public int? RentId { get; set; }
        public int CarIdd { get; set; }
        public virtual Rent Rent { get; set; }
        public virtual Car Car { get; set; }

    }
}
