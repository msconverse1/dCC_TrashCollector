using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MSC_TrashCollector.Models;

namespace MSC_TrashCollector.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Address);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpGet]
        public ActionResult ExtraPickUpDay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewModel viewModel = new ViewModel()
            {
                Customer = new Customer(),
                Address = new Address(),
                SuspendedDay = new SuspendedDay(),
                ExtraPickupDate = new ExtraPickupDate(),
            };
            viewModel.Customer = db.Customers.Find(id);
            viewModel.Address = db.Addresses.Where(e => e.ID == viewModel.Customer.AddressID).SingleOrDefault();
            viewModel.SuspendedDay = db.SuspendedDays.Where(e => e.ID == viewModel.Customer.SuspendedDayId).SingleOrDefault();
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult ExtraPickUpDay(ViewModel viewModel)
        {
            viewModel.ExtraPickupDate.CustomerId = viewModel.Customer.ID;
            db.ExtraPickupDates.Add(viewModel.ExtraPickupDate);
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult PickupDays(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewModel viewModel = new ViewModel()
            {
                Customer = new Customer(),
                Address = new Address(),
                SuspendedDay = new SuspendedDay(),
               
            };
            viewModel.Customer = db.Customers.Find(id);
            viewModel.Address = db.Addresses.Where(e => e.ID == viewModel.Customer.AddressID).SingleOrDefault();
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult PickupDays(ViewModel viewModel)
        {

                db.SuspendedDays.Add(viewModel.SuspendedDay);
                db.Entry(viewModel.Customer).State = EntityState.Modified;
                db.SaveChanges();
            viewModel.Customer.SuspendedDayId = viewModel.SuspendedDay.ID;
            db.SaveChanges();
                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SuspendDays(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewModel viewModel = new ViewModel()
            {
                Customer =new Customer(),
                Address = new Address(),
                SuspendedDay = new SuspendedDay()
        };
            viewModel.Customer = db.Customers.Find(id);
            viewModel.Address  =db.Addresses.Where(e => e.ID == viewModel.Customer.AddressID).SingleOrDefault();
            viewModel.SuspendedDay = db.SuspendedDays.Where(e => e.ID == viewModel.Customer.SuspendedDayId).SingleOrDefault();
            if (viewModel == null)
            {
                return HttpNotFound();
            }
           
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SuspendDays(ViewModel viewModel)
        {
            db.Entry(viewModel.SuspendedDay).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
       // GET: Customers/Create
       [HttpGet]
        public ActionResult Create()
        {
            ViewModel viewModel = new ViewModel()
            {
                Customer = new Customer(),
                Address = new Address(),
                
            };
            
            return View(viewModel);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModel viewModel)
        {
            if (ModelState.IsValid)            
            {
                //viewModel.Customer.UserID= User.Identity.GetUserId();
                db.Customers.Add(viewModel.Customer);
                db.Addresses.Add(viewModel.Address);
                
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "StreetName", viewModel.Customer.AddressID);
            return View(viewModel);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "StreetName", customer.AddressID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "StreetName", customer.AddressID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
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
