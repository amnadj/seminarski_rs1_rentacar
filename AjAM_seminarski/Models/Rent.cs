using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AjAM_seminarski.Data
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool Verified { get; set; }
        public string ClientId { get; set; }
        public int CarId { get; set; }
    }
}
