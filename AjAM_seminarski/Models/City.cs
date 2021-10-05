using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AjAM_seminarski.Data
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostalCode { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
