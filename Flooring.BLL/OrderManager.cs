using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Data;
using Flooring.Models;

namespace Flooring.BLL
{
    public class OrderManager
    {
        private readonly IOrderRepository _orderRepo;


        public OrderManager()
        {
            _orderRepo = FactoryRepository.GetOrderRepository();
        }

        public OrderResponse DisplayOrderByID(int id, DateTime date)
        {
            var response = new OrderResponse();
            try
            {
                var order = _orderRepo.GetOrderByID(id, date);
                
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order number not found";
                }
                else
                {
                    order.OrderDate = date;
                    response.Success = true;
                    List<Order> orders = new List<Order>();
                    orders.Add(order);
                    response.OrderList = orders;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                var log = new LogRepository();
                log.logError(ex);
            }
            return response;
        }

        public OrderResponse UpdateOrder(Order newOrder,int oldOrderNumber,DateTime olddate)
        {
            var response = new OrderResponse();
            try
            {
                if (olddate != newOrder.OrderDate)
                {
                    DeleteOrder(oldOrderNumber, olddate);
                    response = AddOrder(newOrder, newOrder.OrderDate);
                }
                else
                {
                    newOrder.OrderId = oldOrderNumber;
                    _orderRepo.UpdateOrder(newOrder.OrderId, newOrder, newOrder.OrderDate);
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                var log = new LogRepository();
                log.logError(ex);
            }

            return response;
        }

        public OrderResponse DisplayAllOrders(DateTime date)
        {
            List<Order> orders;
            var response = new OrderResponse();
            try
            {
                orders = _orderRepo.GetAllOrdersByDate(date);
                if (orders == null)
                {
                    response.Success = false;
                    response.Message = "Invalid";
                }
                else
                {
                    response.Success = true;
                    response.OrderList = orders;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                var log = new LogRepository();
                log.logError(ex);
            }
            return response;
        }

        public Order CalculateOrder(Order orderToCalculate)
        {
            Order newOrder = new Order();
            newOrder = orderToCalculate;

            newOrder.LaborCost = newOrder.ProductOrdered.LaborCostPerSquareFoot * newOrder.Area;
            newOrder.MaterialCost = newOrder.ProductOrdered.CostPerSquareFoot * newOrder.Area;
            newOrder.TaxCost = (((newOrder.LaborCost + newOrder.MaterialCost) * newOrder.State.TaxRate))/100;
            newOrder.Total = newOrder.TaxCost + newOrder.LaborCost + newOrder.MaterialCost;
            return newOrder;
        }

        public OrderResponse AddOrder(Order newOrder, DateTime date)
        {
            var response = new OrderResponse();

            try
            {
                var orderList = _orderRepo.GetAllOrdersByDate(date);
                if (orderList.Count != 0)
                {
                    newOrder.OrderId = orderList.Max(a=>a.OrderId) + 1;
                }
                else
                {
                    newOrder.OrderId = 1;
                }

                _orderRepo.AddOrder(newOrder, date);

                response.Success = true;
                response.Message = "Order has been generated";
                Console.WriteLine(response.Message);
            }
            catch (Exception ex)
            {
                response.Message = "There was an error. Please try again later.";
                Console.WriteLine(response.Message);
            }
            return response;
        }

        public OrderResponse DeleteOrder(int id, DateTime date)
        {
            var response = new OrderResponse();
            try
            {
                //object m = null;    //to check exception
                //string s = m.ToString();

                var order = _orderRepo.GetOrderByID(id, date);
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order doesnot exist";
                }
                else
                {
                    response.Success = true;
                    _orderRepo.DeleteOrder(id, date);
                    response.Message = "Order has been deleted";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                var log = new LogRepository();
                log.logError(ex);
            }
            return response;
        }
    }
}
