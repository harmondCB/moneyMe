using MoneyMeApp.Data;
using MoneyMeApp.Interfaces;
using MoneyMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyMeApp.Repository
{
    public class CustomerPaymentProductRepository : ICustomerPaymentProductRepository
    {
        private readonly DataContext context;

        public CustomerPaymentProductRepository(DataContext context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }
        public CustomerPaymentProduct GetCustomerPaymentProduct(int customersPaymentProductId)
        {
            return this.context.CustomerPaymentProducts.FirstOrDefault(cpp => cpp.Id == customersPaymentProductId);
        }

        public ICollection<CustomerPaymentProduct> GetCustomerPaymentProducts()
        {
            return this.context.CustomerPaymentProducts.OrderBy(cpp => cpp.Id).ToList();
        }

        public CustomerPaymentProduct GetCustomerPaymentProductByPaymentId(int customersPaymentId)
        {
            return this.context.CustomerPaymentProducts.FirstOrDefault(cpp => cpp.CustomerPaymentId == customersPaymentId);
        }

        public CustomerPaymentProduct AddCustomerPaymentProduct(CustomerPaymentProduct customersPaymentProduct)
        {
            this.context.CustomerPaymentProducts.Add(customersPaymentProduct);
            this.context.SaveChanges();

            return customersPaymentProduct;
        }

        public CustomerPaymentProduct DeleteCustomerPaymentProduct(int customersPaymentProductId)
        {
            var result = this.context.CustomerPaymentProducts.FirstOrDefault(cpp => cpp.Id == customersPaymentProductId);
            
            if (result != null)
            {
                this.context.CustomerPaymentProducts.Remove(result);
                this.context.SaveChanges();
            }

            return result;
        }


        public CustomerPaymentProduct UpdateCustomerPaymentProduct(CustomerPaymentProduct customersPaymentProduct)
        {
            this.context.CustomerPaymentProducts.Update(customersPaymentProduct);
            this.context.SaveChanges();

            return customersPaymentProduct;
        }

        public DTO.CustomerPaymentProduct GetCustomerPaymentProductFullDetails(int customerPaymentProductId)
        {
            var data = this.context.CustomerPaymentProducts.FirstOrDefault(x => x.Id == customerPaymentProductId);
            var paymentProductResult = (from cp in this.context.CustomerPayments
                                        join cpp in this.context.CustomerPaymentProducts on cp.Id equals cpp.CustomerPaymentId
                                        join p in this.context.Products on cpp.ProductId equals p.Id
                                        select new DTO.CustomerPaymentProduct
                                        {
                                            Amount = cp.Amount,
                                            Terms = cp.Duration,
                                            Interest = cpp.Interest,
                                            StartDate = cpp.StartDate,
                                            EndDate = cpp.EndDate,
                                            TotalAmount = cpp.TotalAmount,
                                            ProductName = p.Name,
                                            CustomerPaymentId = cp.Id
                                        }).FirstOrDefault(x => x.CustomerPaymentId == data.CustomerPaymentId);

            return paymentProductResult;
        }        
    }
}
