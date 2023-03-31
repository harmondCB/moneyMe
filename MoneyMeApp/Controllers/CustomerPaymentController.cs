using Microsoft.AspNetCore.Mvc;
using MoneyMeApp.DTO;
using MoneyMeApp.Interfaces;
using MoneyMeApp.Models;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoneyMeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPaymentController : ControllerBase
    {
        private readonly ICustomerPaymentRepository customerPaymentRepository;
        private readonly ICustomerPaymentProductRepository customerPaymentProductRepository;
        private readonly IProductRepository productRepository;

        public CustomerPaymentController(ICustomerPaymentRepository customerPaymentRepository, 
            ICustomerPaymentProductRepository customerPaymentProductRepository,
            IProductRepository productRepository)
        {
            this.customerPaymentRepository = customerPaymentRepository;
            this.customerPaymentProductRepository = customerPaymentProductRepository;
            this.productRepository = productRepository;
        }
        // GET: api/<CustomerPaymentController>
        [HttpGet]
        public IActionResult GetCustomerPayments()
        {
            return Ok(this.customerPaymentRepository.GetCustomerPayments());
        }


        // GET api/<CustomerPaymentController>/5
        [HttpGet("{id}")]
        public IActionResult GetCustomersById(int id)
        {
            return Ok(this.customerPaymentRepository.GetCustomerPayment(id));
        }

        // GET api/<CustomerPaymentController>/5
        [HttpGet("Product/{id}")]
        public IActionResult GetCustomerPaymentProductById(int id)
        {
            return Ok(this.customerPaymentProductRepository.GetCustomerPaymentProductByPaymentId(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CustomerPaymentDetails value)
        {
            var interestRate = 0.0M;
            var totalAmount = 0.0M;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddMonths(value.Duration);
            var customerPayment = this.customerPaymentRepository.GetCustomerPayment(value.CustomerPaymentId);

            var product = this.productRepository.GetProduct(value.ProductId);
            totalAmount = customerPayment.Amount;

            if (product.InterestRate > 0)
            {
                interestRate = customerPayment.Amount * product.InterestRate;
                totalAmount = +interestRate;
            }

            if (product.InterestFreeMonth > 0)
            {
                startDate = startDate.AddMonths(product.InterestFreeMonth);
            }

            Models.CustomerPaymentProduct customerPaymentProduct = new()
            {
                CustomerPaymentId = value.CustomerPaymentId,
                Interest = interestRate,
                TotalAmount = totalAmount,
                StartDate = startDate,
                EndDate = endDate,
                ProductId = value.ProductId
            };

            var productpaymentResult = this.customerPaymentProductRepository.AddCustomerPaymentProduct(customerPaymentProduct);

            return Ok(this.customerPaymentProductRepository.GetCustomerPaymentProductFullDetails(productpaymentResult.Id));
        }

        //PUT api/<CustomerPaymentController>/5
        [HttpPut("{customerProductPaymentId}")]
        public IActionResult Put(int customerProductPaymentId, [FromBody] CustomerPaymentDetails value)
        {
            var interestRate = 0.0M;
            var totalAmount = 0.0M;
            var product = this.productRepository.GetProduct(value.ProductId);
            var customerProductPaymentResult = this.customerPaymentProductRepository.GetCustomerPaymentProduct(customerProductPaymentId);
            var customerPaymentResult = this.customerPaymentRepository.GetCustomerPayment(value.CustomerPaymentId);

            totalAmount = customerPaymentResult.Amount;
            var startDate = customerProductPaymentResult.StartDate;
            var endDate = customerProductPaymentResult.StartDate.AddMonths(value.Duration);

            if (product.InterestRate > 0)
            {
                interestRate = customerPaymentResult.Amount * product.InterestRate;
                totalAmount = +interestRate;
            }

            if (product.InterestFreeMonth > 0)
            {
                startDate = startDate.AddMonths(product.InterestFreeMonth);
            }


            Models.CustomerPaymentProduct customerPaymentProduct = new()
            {
                Id = customerProductPaymentId,
                CustomerPaymentId = value.CustomerPaymentId,
                Interest = interestRate,
                TotalAmount = totalAmount,
                StartDate = startDate,
                EndDate = endDate,
                ProductId = value.ProductId
            };

            var productpaymentResult = this.customerPaymentProductRepository.UpdateCustomerPaymentProduct(customerPaymentProduct);

            return Ok(this.customerPaymentProductRepository.GetCustomerPaymentProductFullDetails(productpaymentResult.Id));
            
        }
    }
}
