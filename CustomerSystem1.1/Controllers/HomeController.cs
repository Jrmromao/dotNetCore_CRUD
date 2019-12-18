using CustomerSystem1._1.Models;
using CustomerSystem1._1.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerSystem1._1.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// O que e que eu queri fazer?
        /// 
        /// Quero iniciar o programa com uma lista.
        /// Ora, para fazer isto tenho de ter uma
        /// class ppara fazer o trabalho de base de dado. Depois tenho de 
        /// fazer os updates com essa class. 
        /// posso criar outrra para para a instace dessa class entre methos.
        /// 
        /// -------------------------------------------------
        /// 
        /// penso que isto esta a falhar para abrir porque tenho a base de dados desligada. vow 
        /// ligar o sql server para ver se isto ficaa a trabalhar. Vamos la....
        /// 
        /// afinal isto ja deu 
        /// 
        /// 
        /// </summary>
        /// 
        public ActionResult Index()
        {

            //var db = new CustomerRepositoryEntities1();

            var db = new MyDbContext();



            var customerDetailsVM = new CustomerDetailsViewModel()
            {
                Customer = db.Customers.ToList(),
                PageTitle = "Customer Details"
            };



            return View(customerDetailsVM);
        }
        [HttpGet] // decorated 
        public ActionResult AddCustomer()
        {

            var cList = new List<CountryList>()
            {
                new CountryList(){CountryId = 1, CountryName = "Portugal"},
                new CountryList(){CountryId = 2, CountryName = "India"},
                new CountryList(){CountryId = 3, CountryName = "Ireland"},
                new CountryList(){CountryId = 4, CountryName = "Spain"}
            };


            var viewModel = new AddUserViewModel();
            viewModel.cList = cList;

            ViewBag.Country = new SelectList(cList, "CountryName", "CountryName");

            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(FormCollection formCollection)
        {
            //var db = new CustomerRepositoryEntities1();
            var db = new MyDbContext();
            var c = new Customer();
            var ca = new Address();


            c.CustomerName = formCollection["Name"];
            c.Email = formCollection["Email"];
            c.dateUpdated = DateTime.Now;
            c.updatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            c.Dob = Convert.ToDateTime(formCollection["Dob"]);

            string Line1 = formCollection["Line1"];
            string Line2 = formCollection["Line2"];
            string State = formCollection["State"];
            string City = formCollection["City"];
            string PostalCode = formCollection["PostalCode"];
            string Country = formCollection["Country"];

            if (Request.Files.Count >= 1)
            {
                var file = Request.Files[0];
                var imgByte = new Byte[file.ContentLength];
                file.InputStream.Read(imgByte, 0, file.ContentLength);
                var bas64String = Convert.ToBase64String(imgByte, 0, imgByte.Length);
                c.Photo = bas64String;


            }

            ca.Customer = c;
            ca.Line1 = Line1;
            ca.Line2 = Line2;
            ca.State = State;
            ca.City = City;
            ca.PostalCode = PostalCode;
            ca.Country = Country;


            db.Customers.Add(c);
            db.Addresses.Add(ca);

            db.SaveChanges();
            // get the last customer in the list. 
            // now, we need to get the lo

            var cList = db.Customers.OrderBy(o => o.CustomerId).ToList();

            var customerDetailsViewmodel = new CustomerDetailsViewModel()
            {
                Customer = cList,
                PageTitle = "Customer Details"
            };

            return RedirectToAction("Index", customerDetailsViewmodel);
        }

        public ActionResult Edit(int customerId)
        {
            //var db = new CustomerRepositoryEntities1();
            var db = new MyDbContext();
            var cust = db.Customers.ToList().
                Where(o => o.CustomerId == customerId)
                .FirstOrDefault();
            return View(cust);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            //var db = new CustomerRepositoryEntities1();
            var db = new MyDbContext();
            var c = db.Customers.Where(oc => oc.CustomerId == customer.CustomerId).FirstOrDefault();

            c.CustomerId = customer.CustomerId;
            c.CustomerName = customer.CustomerName;
            c.Email = customer.Email;
            c.Dob = customer.Dob;
            c.dateUpdated = DateTime.Now;
            c.updatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            db.SaveChanges();


            var customerDetailsVM = new CustomerDetailsViewModel()
            {
                Customer = db.Customers.ToList(),
                PageTitle = "| " + c.CustomerName
            };

            return View("index", customerDetailsVM);
        }
        [HttpGet]
        public ActionResult Delete(int customerId)
        {
            // var db = new CustomerRepositoryEntities1();
            var db = new MyDbContext();
            var cust = db.Customers
                .Where(o => o.CustomerId == customerId)
                .FirstOrDefault();


            var address = db.Addresses.Where(a => a.CustomerId == customerId).ToList();



            foreach (var a in address)
            {
                db.Addresses.Remove(a);
            }

            db.Customers.Remove(cust);

            db.SaveChanges();

            return RedirectToAction("index");
        }

        public ActionResult ViewDetails(int id)
        {
            // var db = new CustomerRepositoryEntities1();
            var db = new MyDbContext();
            var cust = db.Customers.ToList().Where(o => o.CustomerId == id).FirstOrDefault();
            var addresses = db.Addresses.Where(a => a.CustomerId == id).ToList();

            var customerDetails = new CustomerDemailModelViewModel()
            {
                customer = cust,
                addresses = addresses,//db.Addresses.ToList().Where(o => o.CustomerId == id).ToList()

            };
            //ViewBag.cList = db.customers;
            return PartialView(customerDetails);
        }

        [HttpPost]
        public ActionResult Edit_address(Address ca)
        {
            //var db = new CustomerRepositoryEntities1();
            var db = new MyDbContext();
            var toUpdated = db.Addresses.
                Where(a => a.AddressId == ca.AddressId)
                .FirstOrDefault();

            toUpdated.Line1 = ca.Line1;
            toUpdated.Line2 = ca.Line2;
            toUpdated.State = ca.State;
            toUpdated.Country = ca.Country;
            toUpdated.City = ca.City;


            toUpdated.PostalCode = ca.PostalCode;
            db.SaveChanges();
            return RedirectToAction("index");
        }


        [HttpPost]
        public ActionResult Add_address(Address ca)
        {
            //CustomerRepositoryEntities1 db = NewMethod(ca);
            var db = new MyDbContext();
            db.Addresses.Add(ca);
            db.SaveChanges();

            return RedirectToAction("index");
        }

        //private static CustomerRepositoryEntities1 NewMethod(Address ca)
        //{
        //    // add a new address to the address list
        //    //System.Windows.Forms.MessageBox.Show(ca.CustomerId+"");
        //    // var db = new CustomerRepositoryEntities1();
        //    var db = new MyDbContext();
        //    var caa = db.Customers.Where(ce => ce.CustomerId == ca.CustomerId).FirstOrDefault();

        //    ca.Customer = db.Customers.Where(c => c.CustomerId == ca.CustomerId).FirstOrDefault();
        //    return db;
        //}

        public ActionResult Delete_address(int id)
        {

            // add a new address to the address list
            //System.Windows.Forms.MessageBox.Show(ca.CustomerId+"");
            //var db = new CustomerRepositoryEntities1();
            //var db = new CustomerRepositoryEntities1();

            var db = new MyDbContext();
            var address = db.Addresses.Where(a => a.AddressId == id).FirstOrDefault();
            var cust_id = address.CustomerId;
            db.Addresses.Remove(address);
            db.SaveChanges();


            var cust = db.Customers.ToList().Where(o => o.CustomerId == cust_id).FirstOrDefault();
            var addresses = db.Addresses.Where(a => a.CustomerId == cust_id).ToList();

            var customerDetails = new CustomerDemailModelViewModel()
            {
                customer = cust,
                addresses = addresses//db.Addresses.ToList().Where(o => o.CustomerId == id).ToList()
            };
            //ViewBag.cList = db.customers;
            return RedirectToAction("index");
        }
    }
}


