using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public class ProductRepository : IProductRepository
    {
        //private static List<Product> AllProducts;
        private const string _FilePath = @"DataFiles/Products.txt";

        public ProductRepository()
        {
            GetAllProducts();
        }

        List<Product> results = new List<Product>();
        public List<Product> GetAllProducts()
        {   
            var rows = File.ReadAllLines(_FilePath);

            for (int i = 1; i < rows.Length; i++)
            {
                var columns = rows[i].Split('|');

                var product = new Product();
                product.ProductType = columns[0];
                product.CostPerSquareFoot = decimal.Parse(columns[1]);
                product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);
                results.Add(product);
            }
            return results;
        }

        public Product GetProductByName(string name)
        {
            return results.FirstOrDefault(a => a.ProductType == name);
        }
    }
}
