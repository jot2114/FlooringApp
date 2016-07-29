using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public interface IOrderRepository
    {
        void AddOrder(Order order,DateTime date);
        Order GetOrderByID(int id,DateTime date);
        List<Order> GetAllOrdersByDate(DateTime date);
        void UpdateOrder(int id, Order order, DateTime date);
        void DeleteOrder(int id, DateTime date);
    }
}
    