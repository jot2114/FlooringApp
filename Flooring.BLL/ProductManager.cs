using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Data;
using Flooring.Models;

namespace Flooring.BLL
{
    public class ProductManager
    {
        private readonly IProductRepository _productRepo;

        public ProductManager()
        {
            _productRepo = FactoryRepository.GetProductRepository();
        }

        public ProductResponse GetProduct(string productType)
        {
            var result = new ProductResponse();

            try
            {
                var product = _productRepo.GetProductByName(productType);

                if (product == null)
                {
                    result.Success = false;
                    result.Message = "Product was not found.";
                }
                else if (product.ProductType != "Carpet" && product.ProductType != "Laminate" &&
                         product.ProductType != "Tile" && product.ProductType != "Wood")
                {
                    result.Success = false;
                    result.Message = "Invalid";
                }
                else
                {
                    result.Success = true;
                    List<Product> products = new List<Product>();
                    products.Add(product);
                    result.ProductList = products;
                }
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "There was an error. Please try again later.";
            }
            return result;
        }

        public ProductResponse GetAllProducts(string productType)
        {
            List<Product> products;
            var response = new ProductResponse();

            try
            {
                products = _productRepo.GetAllProducts();
                if (products == null)
                {
                    response.Success = false;
                    response.Message = "Invalid";
                }
                else
                {
                    response.Success = true;
                    response.ProductList = products;
                }
            }
            catch
            {
                response.Success = false;
                response.Message = "there was an error, please try again";
            }
            return response;
        }
    }
}
