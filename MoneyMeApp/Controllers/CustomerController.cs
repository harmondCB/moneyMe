using Microsoft.AspNetCore.Mvc;
using MoneyMeApp.DTO;
using MoneyMeApp.Interfaces;
using MoneyMeApp.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoneyMeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ICustomerPaymentRepository customerPaymentRepository;

        public CustomerController(ICustomerRepository customerRepository, ICustomerPaymentRepository customerPaymentRepository)
        {
            this.customerRepository = customerRepository;
            this.customerPaymentRepository = customerPaymentRepository;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = this.customerRepository.GetCustomers();

            return Ok(customers);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult GetCustomersById(int id)
        {
            var customer = this.customerRepository.GetCustomers(id);

            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post([FromBody] CustomerDetails value)
        {
            Customer customer = MapCustomer(value);

            var result = this.customerRepository.AddCustomer(customer);

            CustomerPayment customerPayment = new()
            {
                Duration = value.Term,
                Amount = value.AmountRequired,
                CustomerId = result.Id
            };

            var resulCustomerPayment = this.customerPaymentRepository.AddCustomerPayment(customerPayment);
            CustomerDetailsResult customerDetailsResult = GetCustomerDetailsResult(result, resulCustomerPayment);

            return Ok(customerDetailsResult);
        }        

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int Id, [FromBody] CustomerDetails value)
        {
            var existingCustomerPayment = this.customerPaymentRepository.GetCustomerPayment(Id);
            existingCustomerPayment.Amount = value.AmountRequired;
            existingCustomerPayment.Duration = value.Term;
                
            var resultCustomerPayment = this.customerPaymentRepository.UpdateCustomerPayment(existingCustomerPayment);

            Customer customer = MapCustomer(value);
            customer.Id = resultCustomerPayment.CustomerId;

            var result = this.customerRepository.UpdateCustomer(customer);
                      
            CustomerDetailsResult customerDetailsResult = GetCustomerDetailsResult(result, resultCustomerPayment);

            return Ok(customerDetailsResult);
        }

        private static Customer MapCustomer(CustomerDetails value)
        {
            return new()
            {
                FirstName = value.FirstName,
                LastName = value.LastName,
                DateOfBirth = value.DateOfBirth,
                Mobile = value.Mobile,
                Email = value.Email,
                Title = value.Title
            };
        }

        private static CustomerDetailsResult GetCustomerDetailsResult(Customer result, CustomerPayment resulCustomerPayment)
        {
            return new()
            {
                Id = resulCustomerPayment.CustomerId,
                CustomerPaymentId = resulCustomerPayment.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                DateOfBirth = result.DateOfBirth,
                Email = result.Email,
                Mobile = result.Mobile,
                AmountRequired = resulCustomerPayment.Amount,
                Term = resulCustomerPayment.Duration
            };
        }
    }
}
