using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerSystem1._1.Models.ViewModel
{
    public class CustomerDemailModelViewModel
    {
        public Customer customer { get; set; }

        public List<Address> addresses { get; set; }
    }
}