using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerSystem1._1.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Nullable<int> CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}