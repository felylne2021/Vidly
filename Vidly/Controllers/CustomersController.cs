using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers {
    public class CustomersController : Controller {
        private ApplicationDbContext _context;

        public CustomersController() {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        public ActionResult New() {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        //[HttpPost]
        //public void Save(TestVM data) {
        //    var test = "";
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer) {
            if (!ModelState.IsValid) {
                var viewModel = new CustomerFormViewModel {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else {
                var CustInDB = _context.Customers.Single(c => c.Id == customer.Id);

                CustInDB.Name = customer.Name;
                CustInDB.Birthdate = customer.Birthdate;
                CustInDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                CustInDB.MembershipType = customer.MembershipType;
            }

            var newViewModel = new CustomerFormViewModel {
                Customer = new Customer(),
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            _context.SaveChanges();
            return View("CustomerForm", newViewModel);
        }

        // GET: Customer
        public ViewResult Index() {

            return View();
        }

        public ActionResult Details(int id) {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int id) {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}