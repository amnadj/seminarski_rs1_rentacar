using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AjAM_seminarski.Data
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }
        public int? CardNumber { get; set; }
        public int? Other { get; set; }
        public int ClientId { get; set; }

    }
}
