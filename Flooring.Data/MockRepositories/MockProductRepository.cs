using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public class MockProductRepository : IProductRepository
    {
        private List<Product> products;

        public MockProductRepository()
        {
            products = new List<Product>()
            {
                new Product() {ProductType = "wood", CostPerSquareFoot = 4.00m, LaborCostPerSquareFoot = 4.5m},
                new Product() {ProductType = "tile", CostPerSquareFoot = 5.00m, LaborCostPerSquareFoot = 5.5m},
                new Product() {ProductType = "brick", CostPerSquareFoot = 3.00m, LaborCostPerSquareFoot = 6.5m}
            };
        }

        public Product GetProductByName(string name)
        {
            return products.FirstOrDefault(o => o.ProductType == name.ToLower());
        }

        public List<Product> GetAllProducts()
        {
            return products; 
        }

    }
}
