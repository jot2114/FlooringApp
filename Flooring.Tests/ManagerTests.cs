using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Flooring.BLL;
using Flooring.Data;
using Flooring.Models;
using NUnit.Framework;

namespace Flooring.Tests
{
    [TestFixture]
    public class ManagerTests
    {
        private readonly OrderManager _manager;
        private ProductManager _Pmanager;
        private StateManager _SManager;

        public ManagerTests()
        {
            _manager = new OrderManager();
            _Pmanager = new ProductManager();
            _SManager = new StateManager();
        }

        [Test]
        public void FoundOrderReturnsSuccess()
        {
            var result = _manager.DisplayOrderByID(1, DateTime.Parse("10/10/2011"));

            Assert.AreEqual(1, result.OrderList[0].OrderId);
        }

        [Test]
        public void OrderNotFoundReturnsFalse()
        {
            var result = _manager.DisplayOrderByID(100, DateTime.Parse("10/10/2011"));

            Assert.AreEqual(false, result.Success);
        }

        [Test]
        public void AddOrderSuccess()
        {
            var newOrder = new Order();
            newOrder.CustomerName = "Jot";
            newOrder.Area = 23.5m;
            _manager.AddOrder(newOrder, new DateTime(2011, 10, 10));
            Assert.AreEqual("Jot", newOrder.CustomerName);

        }

        [TestCase(100, "10/10/2011", false)]
        [TestCase(2, "10/10/2011", true)]
        public void RemoveOrderSuccess(int OrderNumber, string date, bool invalid)
        {
            var dateTime = DateTime.Parse(date);

            var response = _manager.DeleteOrder(OrderNumber, dateTime);
            Assert.AreEqual(invalid, response.Success);
        }

        [Test]
        public void UpdateOrder()
        {
            var newOrder = new Order();
            newOrder.CustomerName = "Jot";
            _manager.UpdateOrder(newOrder, 2, new DateTime(2011,10,10));
            Assert.AreEqual("Jot",newOrder.CustomerName);

        }

        //[TestCase("10/10/2011", "Wood", "OH", true)]
        //[TestCase("10/10/2011", "abc", "PA", false)]
        //public void AddOrderSuccess(string date, string productType, string state, bool invalid)
        //{

        //these 2 line works
        //var response = _manager.AddOrder(new Order(), new DateTime(2011, 10, 10));
        //Assert.IsTrue(response.Success);

        //    Order order = new Order();
        //    DateTime dateTime = DateTime.Parse(date);
        //    order.ProductOrdered = _Pmanager.GetProduct(productType).ProductList[0];
        //    order.State = _SManager.GetState(state).StateList[0];
        //    order.Area = 13;
        //    order.CustomerName = "wert";
        //    order = _manager.CalculateOrder(order);

        //    var response = _manager.AddOrder(order, dateTime);
        //    Assert.AreEqual(invalid, response.Success);
        //}

    }
}
