using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerSystem1._1.Models.ViewModel
{
    public class CustomerDetailsViewModel
    {
        public List<Customer> Customer { get; set; }
        public List<Address> Addresses { get; set; }
        public string PageTitle { get; set; }
    }
}