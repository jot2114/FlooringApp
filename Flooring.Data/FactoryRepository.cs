using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Data
{
    public static class FactoryRepository
    {
        public static IOrderRepository GetOrderRepository()
        {
            string mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode.ToUpper())
            {
                case "TEST":
                    return new MockOrderRepository();

                case "PROD":
                    return new OrderRepository();   

                default:
                    throw new Exception("I don't know that mode!");
            }
        }

        public static IProductRepository GetProductRepository()
        {
            string mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode.ToUpper())
            {
                case "TEST":
                    return new MockProductRepository();

                case "PROD":
                    return new ProductRepository();

                default:
                    throw new Exception("I don't know that mode!");
            }
        }


        public static IStateRepository GetTaxRepository()
        {
            string mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode.ToUpper())
            {
                case "TEST":
                    return new MockStateRepository();

                case "PROD":
                    return new StateRepository();

                default:
                    throw new Exception("I don't know that mode!");
            }
        }
    }
}
