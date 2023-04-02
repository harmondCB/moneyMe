using Microsoft.AspNetCore.Mvc;
using MoneyMeWebApplication.Objects;
using MoneyMeWebApplication.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
            var result = await GetCustomerSummaryDetails(id);

            return View(result);
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

                return RedirectToAction("Summary", new {id = routeId});
            }
            
            return View(customerFullDetailsViewModel);
        }

        public async Task<CustomerSummaryDetailsViewModel> GetCustomerSummaryDetails(int customerPaymentId)
        {
            Customer customer = new();
            CustomerPayment customerPayment = new();
            CustomerPaymentProduct customerPaymentProduct = new();

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

            CustomerSummaryDetailsViewModel customerSummaryDetails = new()
            {
                Customer = customer,
                CustomerPayment = customerPayment,
                CustomerPaymentProduct = customerPaymentProduct
            };

            return customerSummaryDetails;
        }
        public async Task<CustomerFullDetailsViewModel> GetCustomer(int customerPaymentId)
        {          
            List<Product> product = new();
            CustomerSummaryDetailsViewModel customerSummaryDetails = new() { 
                Customer = new(),
                CustomerPayment = new(),
                CustomerPaymentProduct = new()
            };

            if (customerPaymentId != 0)
            {
                customerSummaryDetails = await GetCustomerSummaryDetails(customerPaymentId);
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
                Term = customerSummaryDetails.CustomerPayment.Duration, 
                Amount = customerSummaryDetails.CustomerPayment.Amount,
                CustomerPayment =  customerSummaryDetails.CustomerPayment,
                Customer = customerSummaryDetails.Customer,
                Products = product,
                SelectedProduct = customerSummaryDetails.CustomerPaymentProduct.ProductId
            };

            return customerFullDetails;
        }
    }
}
