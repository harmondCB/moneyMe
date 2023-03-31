using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MoneyMeWebApplication.Objects;
using MoneyMeWebApplication.ViewModel;
using System.Net.Http.Json;

namespace MoneyMeWebApplication.Controllers
{
    public class HomeController : Controller
    {
        string BaseURL = "https://localhost:5001/";

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {

            var result = await GetCustomer(id);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Summary(int id)
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CustomerFullDetailsViewModel customerFullDetailsViewModel)
        {           
            if (ModelState.IsValid)
            {
                int routeId = 0;

                CustomerDetails customerDetails = new()
                {
                    Id = customerFullDetailsViewModel.Customer.Id,
                    FirstName = customerFullDetailsViewModel.Customer.FirstName,
                    LastName = customerFullDetailsViewModel.Customer.LastName,
                    Mobile = customerFullDetailsViewModel.Customer.Mobile,
                    DateOfBirth = customerFullDetailsViewModel.Customer.DateOfBirth,
                    Email = customerFullDetailsViewModel.Customer.Email,
                    Term = customerFullDetailsViewModel.Term,
                    Title = customerFullDetailsViewModel.Customer.Title,
                    AmountRequired = customerFullDetailsViewModel.Amount
                };

                CustomerPaymentDetails customerPaymentDetails = new()
                {
                    ProductId = customerFullDetailsViewModel.SelectedProduct,
                    CustomerPaymentId = customerFullDetailsViewModel.CustomerPayment.Id,
                    Duration = customerFullDetailsViewModel.Term,
                    Amount = customerFullDetailsViewModel.Amount
                };

                if(customerFullDetailsViewModel.Customer.Id != 0)
                {
                    routeId = customerFullDetailsViewModel.CustomerPayment.Id;
                    CustomerPaymentProduct customerPaymentProduct = new();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseURL);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage Response = await client.GetAsync("api/CustomerPayment/Product/" + customerFullDetailsViewModel.CustomerPayment.Id);

                        if (Response.IsSuccessStatusCode)
                        {
                            var result = Response.Content.ReadAsStringAsync().Result;
                            customerPaymentProduct = JsonConvert.DeserializeObject<CustomerPaymentProduct>(result);                           
                        }
                    }

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseURL);
                        client.DefaultRequestHeaders.Clear();

                        var putTask = await client.PutAsJsonAsync<CustomerDetails>("api/Customer/" + customerFullDetailsViewModel.CustomerPayment.Id, customerDetails);
                        var response = putTask;
                    }

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseURL);
                        client.DefaultRequestHeaders.Clear();

                        var putTask = await client.PutAsJsonAsync<CustomerPaymentDetails>("api/CustomerPayment/" + customerPaymentProduct.Id, customerPaymentDetails);
                        var response = putTask;
                    }

                } else
                {
                    CustomerDetailsResult customerDetailsNew = new();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseURL);
                        client.DefaultRequestHeaders.Clear();
                        
                        var postTask = await client.PostAsJsonAsync<CustomerDetails>("api/Customer", customerDetails);                      
                        var response = postTask;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().Result;
                            customerDetailsNew = JsonConvert.DeserializeObject<CustomerDetailsResult>(result);
                        }
                    }
                    routeId = customerDetailsNew.CustomerPaymentId;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseURL);
                        client.DefaultRequestHeaders.Clear();
                        customerPaymentDetails.CustomerPaymentId = customerDetailsNew.CustomerPaymentId;

                        var postTask = await client.PostAsJsonAsync<CustomerPaymentDetails>("api/CustomerPayment/", customerPaymentDetails);
                        var response = postTask;
                    }
                }

                return RedirectToAction("Summary", routeId);
            }
            
            return View(customerFullDetailsViewModel);
        }



        public async Task<CustomerFullDetailsViewModel> GetCustomer(int customerPaymentId)
        {
            Customer customer = new();
            List<Product> product = new();
            CustomerPayment customerPayment = new();
            CustomerPaymentProduct customerPaymentProduct = new();

            if (customerPaymentId != 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseURL);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Response = await client.GetAsync("api/CustomerPayment/Product/" + customerPaymentId);

                    if (Response.IsSuccessStatusCode)
                    {
                        var result = Response.Content.ReadAsStringAsync().Result;
                        customerPaymentProduct = JsonConvert.DeserializeObject<CustomerPaymentProduct>(result);
                    }
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseURL);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Response = await client.GetAsync("api/CustomerPayment/" + customerPaymentId);

                    if (Response.IsSuccessStatusCode)
                    {
                        var result = Response.Content.ReadAsStringAsync().Result;
                        customerPayment = JsonConvert.DeserializeObject<CustomerPayment>(result);                     
                    }
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseURL);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Response = await client.GetAsync("api/Customer/" + customerPayment.CustomerId);

                    if (Response.IsSuccessStatusCode)
                    {
                        var result = Response.Content.ReadAsStringAsync().Result;
                        customer = JsonConvert.DeserializeObject<Customer>(result);
                    }
                }

            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Response = await client.GetAsync("api/Product/");

                if (Response.IsSuccessStatusCode)
                {
                    var result = Response.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<List<Product>>(result);
                }
            }

            CustomerFullDetailsViewModel customerFullDetails = new()
            {
                Term = customerPayment.Duration,
                Amount = customerPayment.Amount,
                CustomerPayment = customerPayment,
                Customer = customer,
                Products = product,
                SelectedProduct = customerPaymentProduct.ProductId
            };


            return customerFullDetails;
        }
    }
}
