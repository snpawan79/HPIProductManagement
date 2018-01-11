using HPI.DataAccessLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPI.NUnit.Tests.TestsHelper
{
    public class DataInitializer
    {
        public static List<Product> GetAllProducts()
        {
            var products = new List<Product>
            {
            new Product() {ProductCode = "A001",Price = 10},
            new Product() {ProductCode = "B001", Price=120},
            new Product() {ProductCode = "C001", Price= 1200}
            };
            return products;
        }
    }
}
