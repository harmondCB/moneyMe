using Microsoft.AspNetCore.Mvc;
using MoneyMeApp.DTO;
using MoneyMeApp.Interfaces;
using MoneyMeApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoneyMeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            var products = this.productRepository.GetProducts();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var products = this.productRepository.GetProduct(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(products);
        }

        [HttpGet("Name/{name}")]
        public IActionResult GetByName(string name)
        {
            var product = this.productRepository.GetProduct(name);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductDetails value)
        {
            Product product = MapProduct(value);

            return Ok(this.productRepository.AddProduct(product));
        }

        private static Product MapProduct(ProductDetails value)
        {
            return new()
            {
                Name = value.Name,
                InterestRate = value.InterestRate,
                InterestFreeMonth = value.InterestFreeMonth
            };
        }
    }
}
