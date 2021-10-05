using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AjAM_seminarski.Data
{
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }
        public int CarId { get; set; }
        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }
        public virtual Car Car { get; set; }
    }
}
