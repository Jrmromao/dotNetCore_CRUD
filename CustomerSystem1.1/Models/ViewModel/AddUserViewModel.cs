using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CustomerSystem1._1.Models.ViewModel
{
    public class AddUserViewModel
    {
        public Customer customer { get; set; }
        public Address Address { get; set; }
        [Display(Name = "Country List")]
        public List<CountryList> cList { set; get; }
    }
}