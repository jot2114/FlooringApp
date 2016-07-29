using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public class MockOrderRepository : IOrderRepository
    {
        //private static Dictionary<DateTime, List<Order>> _orders = new Dictionary<DateTime, List<Order>>();
        private static Dictionary<DateTime, List<Order>> _orders ;
        public MockOrderRepository()
        {
            if (_orders == null)
            {
               _orders = new Dictionary<DateTime, List<Order>>();

                DateTime mockDate = new DateTime(2011, 10, 10);

                _orders.Add(mockDate, new List<Order>() { new Order()
                {
                    OrderId = 1,
                    CustomerName = "David",
                    Area = 10.12m,
                    ProductOrdered = new Product() { CostPerSquareFoot = 20, LaborCostPerSquareFoot = 25, ProductType = "Wood" },
                    State = new State() { StateName = "Ohio", StateAbbreviation = "OH", TaxRate = 0.09m },
                    LaborCost = 201,
                    MaterialCost = 400,
                    TaxCost = 66.15m,
                    Total = 661.15m
                },
                
                new Order()
                {
                    OrderId = 2,
                    CustomerName = "Bob",
                    Area = 20.12m,
                    ProductOrdered = new Product() { CostPerSquareFoot = 20, LaborCostPerSquareFoot = 15, ProductType = "Tile" },
                    State = new State() { StateName = "California", StateAbbreviation = "CA", TaxRate = 0.09m },
                    LaborCost = 300,
                    MaterialCost = 520,
                    TaxCost = 76.15m,
                    Total = 901.15m
                }
                });


            }
        }

        public void AddOrder(Order newOrder, DateTime date)
        {
            if (_orders.ContainsKey(date))
            {
                _orders[date].Add(newOrder);
            }
            else
            {
                _orders.Add(date,new List<Order>() {newOrder});
            }
           
        }

        public Order GetOrderByID(int id, DateTime date)
        {
            return GetAllOrdersByDate(date).FirstOrDefault(o => o.OrderId == id);
        }

        public List<Order> GetAllOrdersByDate(DateTime date)
        {
            if (_orders.ContainsKey(date))
            {
                return _orders[date];
            }

            return null;
        }

        public void UpdateOrder(int id, Order order, DateTime date)
        {
            var orderToUpdate = GetAllOrdersByDate(date).FirstOrDefault(o => o.OrderId == id);
            if (orderToUpdate != null)
            {
                orderToUpdate.CustomerName = order.CustomerName;
                orderToUpdate.Area = order.Area;
                orderToUpdate.ProductOrdered.ProductType = order.ProductOrdered.ProductType;
                orderToUpdate.LaborCost = order.LaborCost;
                orderToUpdate.MaterialCost = order.MaterialCost;
                orderToUpdate.State.StateAbbreviation = order.State.StateAbbreviation;
                orderToUpdate.TaxCost = order.TaxCost;
                orderToUpdate.Total = order.Total;
            }
        }

        public void DeleteOrder(int id, DateTime date)
        {
            var orderToRemove = GetAllOrdersByDate(date).FirstOrDefault(o => o.OrderId == id);
            GetAllOrdersByDate(date).Remove(orderToRemove);
        }
    }
}
