using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerSystem1._1.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public System.DateTime Dob { get; set; }
        public string updatedBy { get; set; }
        public System.DateTime dateUpdated { get; set; }
        public Nullable<int> AddressId { get; set; }
        public string Photo { get; set; }
    }
}