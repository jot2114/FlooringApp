using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Flooring.Data;
using Flooring.Models;
using NUnit.Framework;

namespace Flooring.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        private readonly IOrderRepository repo;

        public RepositoryTests()
        {
            repo= FactoryRepository.GetOrderRepository();
        }

        [Test]
        public void AddOrder()
        {
            var order = new Order();
            repo.AddOrder(order, DateTime.Parse("10/10/2011"));
            //repo.AddOrder(order, DateTime.Parse("09/13/2011"));

            var result = repo.GetAllOrdersByDate(DateTime.Parse("10/10/2011"));

            Assert.AreEqual(3, result.Count);
        }


        [Test]
        public void CanLoadAllOrders()
        {
            var orders = repo.GetAllOrdersByDate(DateTime.Parse("10/10/2011"));

            Assert.AreEqual(2, orders.Count);
        }


        [TestCase(2,"10/10/2011", "Bob")]
        public void CanLoadOneOrder(int OrderId, string date, string expected)
        { 
            var dateTime = DateTime.Parse(date);
            var order = repo.GetOrderByID(OrderId, dateTime);

            Assert.AreEqual(expected, order.CustomerName);
        }


        [Test]
        public void UpdateAccountSucceeds()
        {
            var orderToUpdate = repo.GetOrderByID(1, DateTime.Parse("10/10/2011"));
            orderToUpdate.Area = 2.3m;
            repo.UpdateOrder(1,orderToUpdate, DateTime.Parse("10/10/2011"));

            var result = repo.GetOrderByID(1, DateTime.Parse("10/10/2011"));
            Assert.AreEqual(2.3m, result.Area);
        }


        [Test]
        public void DeleteOrderSucceeds()
        {
            var orders = repo.GetAllOrdersByDate(DateTime.Parse("10/10/2011"));
            repo.DeleteOrder(1, DateTime.Parse("10/10/2011"));

            Assert.AreEqual(1, orders.Count);
        }
    }
}
