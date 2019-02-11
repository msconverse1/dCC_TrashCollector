using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using MSC_TrashCollector.Models;
using Newtonsoft.Json;

namespace MSC_TrashCollector.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        // GET: Employees
        public ActionResult Index()
        {
            
            var userLoggedin = User.Identity.GetUserId();

            var employees = db.Employees.Where(c => c.ANUserID == userLoggedin).Include(c => c.Address);
            var test = employees.Single();
            var customerdb = db.Customers.Where(c=>c.Address.ZipCode == test.Address.ZipCode).ToList();

            return View(customerdb);
        }
        [HttpPost]
        
        public ActionResult Index(string DayofWeek)
        {
            //GetUser
            var userLoggedin = User.Identity.GetUserId();
            var employees = db.Employees.Where(c => c.ANUserID == userLoggedin).Include(c => c.Address);
            var test = employees.Single();

            //Get Todays Day of week
            var todaysday = DateTime.Now.DayOfWeek.ToString();

            //Create list of Customers based off of the Users ZipCode
            var customerdb = db.Customers.Where(c => c.Address.ZipCode == test.Address.ZipCode).ToList();
            foreach (var item in customerdb)
            {
                item.SuspendedDay = db.SuspendedDays.Where(s => s.ID == item.SuspendedDayId).Single();
            }

            //Create list for Customers for selected date
            var people = customerdb.Where(c => c.SuspendedDay.PickUPDay.Equals(DayofWeek,StringComparison.OrdinalIgnoreCase)).ToList();
            var prumdays = db.ExtraPickupDates.Where(c => c.ExtraDay == DayofWeek).ToList();
            foreach (var item in prumdays)
            {
                item.Customer = db.Customers.Where(s => s.ID == item.CustomerId).Single();
            }

            //Assign all Customers for selected days from both tables to One
            var returnpeople = new List<Customer>();
            foreach (var item in people)
            {
                if (item.SuspendedDay.StartDate.AsDateTime().Date.Day >= DateTime.Now.Date.Day && item.SuspendedDay.EndDate.AsDateTime().Date.Day <= DateTime.Now.Date.Day)
                {

                }
                else
                    returnpeople.Add(item);
            }
            foreach (var item in prumdays)
            {
                if (!returnpeople.Contains(item.Customer))
                {
                    returnpeople.Add(item.Customer);
                }
            }

            return View(returnpeople);
        }
        [HttpGet]
        public ActionResult PickupStatus(int? id)
        {
            Customer customer = db.Customers.Where(c => c.ID == id).FirstOrDefault();
            if (customer.TrashPickup)
            {
                customer.TrashPickup = false;
                customer.PickupCost = 0;
            }
            else
            {
                customer.TrashPickup = true;
                customer.PickupCost += 3.5 * 2;

            }
            db.SaveChanges();

            return RedirectToAction("Index");

          
        }
        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            var zip = db.Addresses.Select(e => e.ZipCode).ToList();
            ViewModelEmployee viewModelEmployee = new ViewModelEmployee()
            {
                Employee = new Employee(),
                Address = new Address(),

            };
            viewModelEmployee.Employee.ZipCodeSelect = zip;
            

            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "StreetName");
            return View(viewModelEmployee);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelEmployee viewModelEmployee)
        {
            if (ModelState.IsValid)
            {
                var zipcode = db.Addresses.Where(e=>e.ZipCode == viewModelEmployee.Address.ZipCode).First();
                viewModelEmployee.Employee.AddressID = zipcode.ID;
                var userlogedin = User.Identity.GetUserId();
                viewModelEmployee.Employee.ANUserID = userlogedin;
                db.Employees.Add(viewModelEmployee.Employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "StreetName", viewModelEmployee.Employee.AddressID);
            return View(viewModelEmployee.Employee);
        }

        [HttpGet]
        public ActionResult MappAddress(int? id)
        {
            Customer customer = db.Customers.Where(c => c.ID == id).FirstOrDefault();
            customer.Address = db.Addresses.Where(c => c.ID == customer.AddressID).FirstOrDefault();
            APIKeys aPIKeys = new APIKeys();
            customer.key = aPIKeys.api;

            return View("MappAddress", customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
