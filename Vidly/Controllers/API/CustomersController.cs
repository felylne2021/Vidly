using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.API {
    public class CustomersController : ApiController {
        private ApplicationDbContext _context;

        public CustomersController() {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IHttpActionResult GetCustomers() {
            var customer = _context.Customers
                .Include(c => c.MembershipType)
                .ToList();
            return Ok(customer);
        }

        // GET /api/customers/id
        public IHttpActionResult GetCustomer(int id) {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Ok(customer);
        }

        // POST /api/customers
        [HttpPost]
        public void CreateCustomer(Customer customer) {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Customers.Add(customer);
            _context.SaveChanges();

        }

        // PUT /api/customers/id
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto) {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var custInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (custInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, custInDb);
            _context.SaveChanges();

        }

        // DELETE /api/customers/id
        [HttpDelete]
        public void DeleteCustomer(int id) {
            var custInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (custInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(custInDb);
            _context.SaveChanges();
        }
    }
}
