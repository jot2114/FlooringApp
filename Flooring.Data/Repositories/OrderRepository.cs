using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public class OrderRepository : IOrderRepository
    {
        private string _filepath;

        public void SetFilePath(DateTime date)
        {
            _filepath = string.Format(@"DataFiles/Orders_"+ DateInRightFormat(date) + ".txt", date);
        }

        public string DateInRightFormat(DateTime date)
        {
            var result = "";
            if (date.Month < 10)
            {
                result = "0"+date.Month.ToString();
            }
            else
            {
                result= date.Month.ToString();
            }

            if (date.Day < 10)
            {
                result += "0"+date.Day.ToString();
            }
            else
            {
                result += date.Day.ToString();
            }
            result += date.Year.ToString();
            return result;
        }


        public List<Order> GetAllOrdersByDate(DateTime date)
        {
            SetFilePath(date);
            List<Order> results = new List<Order>();

            if (File.Exists(_filepath))
            {
                var rows = File.ReadAllLines(_filepath);

                for (int i = 1; i < rows.Length; i++)
                {

                    var order = new Order();
                    order.State = new State();
                    order.ProductOrdered = new Product();

                    var columns = rows[i].Split(',');        
                    order.OrderId = int.Parse(columns[0]);
                    order.CustomerName = columns[1];
                    order.State.StateAbbreviation = columns[2];
                    order.State.TaxRate = decimal.Parse(columns[3]);
                    order.ProductOrdered.ProductType = columns[4];
                    order.Area = decimal.Parse(columns[5]);
                    order.ProductOrdered.CostPerSquareFoot = decimal.Parse(columns[6]);
                    order.ProductOrdered.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                    order.MaterialCost = decimal.Parse(columns[8]);
                    order.LaborCost = decimal.Parse(columns[9]);
                    order.TaxCost = decimal.Parse(columns[10]);
                    order.Total = decimal.Parse(columns[11]);

                    results.Add(order);
                }
            }
            return results;
        }

        public Order GetOrderByID(int Onumber, DateTime date)
        {
            SetFilePath(date);
            List<Order> orders = GetAllOrdersByDate(date);
            return orders.FirstOrDefault(a => a.OrderId == Onumber);
        }


        public void AddOrder(Order order, DateTime date)
        {
            SetFilePath(date);
            var orders = GetAllOrdersByDate(date);
            orders.Add(order);

            OverwriteFile(orders);
        }

        private void OverwriteFile(List<Order> orders)
        {
            File.Delete(_filepath);
            using (var writer = File.CreateText(_filepath))
            {
                writer.WriteLine(
                    "Order Number, CustomerName, State, Tax Rate, ProductType, Area, Cost per sq. foot, labor cost per sq. foot, material cost, labor cost, tax, total");
                foreach (var order in orders)
                {
                    writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                        order.OrderId,
                        order.CustomerName,
                        order.State.StateAbbreviation,
                        order.State.TaxRate,
                        order.ProductOrdered.ProductType,
                        order.Area,
                        order.ProductOrdered.CostPerSquareFoot,
                        order.ProductOrdered.LaborCostPerSquareFoot,
                        order.MaterialCost,
                        order.LaborCost,
                        order.TaxCost,
                        order.Total);
                }
            }
        }

        public void UpdateOrder(int id, Order order, DateTime date)
        {
            var orders = GetAllOrdersByDate(date);
            var orderToEdit = orders.First(a => a.OrderId == order.OrderId);

            orderToEdit.CustomerName = order.CustomerName;
            orderToEdit.State.StateAbbreviation = order.State.StateAbbreviation;
            orderToEdit.State.TaxRate = order.State.TaxRate;
            orderToEdit.ProductOrdered.ProductType = order.ProductOrdered.ProductType;
            orderToEdit.Area = order.Area;
            orderToEdit.ProductOrdered.CostPerSquareFoot = order.ProductOrdered.CostPerSquareFoot;
            orderToEdit.ProductOrdered.LaborCostPerSquareFoot = order.ProductOrdered.LaborCostPerSquareFoot;
            orderToEdit.MaterialCost = order.MaterialCost;
            orderToEdit.LaborCost = order.LaborCost;
            orderToEdit.TaxCost = order.TaxCost;
            orderToEdit.Total = order.Total;

            OverwriteFile(orders);
        }

      
        public void DeleteOrder(int OrderNumber, DateTime date)
        {
            SetFilePath(date);
            var orders = GetAllOrdersByDate(date);
            var result = orders.Where(a => a.OrderId != OrderNumber);

            OverwriteFile(result.ToList());
        }





        //private List<Order> _orders;
        //private string FilePath;

        //public void SetFilePath(DateTime date)
        //{
        //    FilePath = string.Format(@"DataFiles\Orders_{0}.txt", date);
        //}

        //public void ReadFromFile(DateTime date)
        //{
        //    SetFilePath(date);

        //    _orders = new List<Order>();
        //    using (StreamReader sr = File.OpenText(FilePath))
        //    {
        //        string inputLine = "";
        //        while ((inputLine = sr.ReadLine()) != null)
        //        {
        //            string[] inputParts = inputLine.Split('|');
        //            Order thisOrder = new Order()
        //            {
        //                OrderId = int.Parse(inputParts[0]),
        //                CustomerName = (inputParts[1]),
        //                //  State = (inputParts[2]),

        //                Area = decimal.Parse(inputParts[2]),
        //                MaterialCost = decimal.Parse(inputParts[3]),
        //                TaxCost = decimal.Parse(inputParts[3]),
        //                //LaborCost

        //            };
        //            _orders.Add(thisOrder);
        //        }
        //    }
        //}

        //public void WriteToFile(DateTime date)
        //{
        //    SetFilePath(date);

        //    using (var writer = File.CreateText(FilePath))
        //    {
        //        writer.WriteLine(
        //            "OrderNumber|CustomerName|State|TaxRate|ProductType|Area|CostPerSqFoot|LaborCostPerSqFoot|MaterialCost|LaborCost|tax|Total");
        //        foreach (var order in _orders)
        //        {
        //            writer.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}", order.OrderId,
        //                order.CustomerName, order.State.StateAbbreviation, order.State.TaxRate,
        //                order.ProductOrdered.ProductType, order.Area, order.ProductOrdered.CostPerSquareFoot,
        //                order.ProductOrdered.LaborCostPerSquareFoot,
        //                order.MaterialCost, order.LaborCost, order.State.TaxRate, order.Total);
        //        }
        //    }
        //}


        //public void AddOrder(Order order, DateTime date)
        //{
        //    using (var writer = File.AppendText(FilePath))
        //    {
        //        writer.WriteLine("{0}|{1}|{2}", order.OrderId, order.CustomerName, order.Area);
        //    }
        //}

        //public Order GetOrderByID(int id, DateTime date)
        //{
        //    List<Order> orders = GetAllOrdersByDate(date);
        //    return orders.FirstOrDefault(a => a.OrderId == id);
        //}

        //public List<Order> GetAllOrdersByDate(DateTime date)
        //{
        //    ReadFromFile(date);
        //    return _orders;
        //}

        //public void UpdateOrder(int id, Order newOrder, DateTime date)
        //{
        //    newOrder.OrderId = id;
        //    LoadOrdersAndDeleteEntry(id, date);
        //    _orders.Add(newOrder);
        //    WriteToFile(date);
        //}

        //public void DeleteOrder(int id, DateTime date)
        //{
        //    LoadOrdersAndDeleteEntry(id, date);
        //    WriteToFile(date);
        //}

        //private void LoadOrdersAndDeleteEntry(int id, DateTime date)
        //{
        //    var orders = GetAllOrdersByDate(date);
        //    for (int i = 0; i < orders.Count; i++)
        //    {
        //        if (orders[i].OrderId == id)
        //        {
        //            orders.RemoveAt(i);
        //        }
        //    }
        //}

        #region
        //public bool IsDateandOrderExist(DateTime date, int OrderNumber)
        //{
        //    SetFilePath(date);

        //    if (File.Exists(_filepath))
        //    {
        //        if (GetAllOrdersByDate(date).FirstOrDefault(o => o.OrderId == OrderNumber) != null)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
#endregion
    }
}
    
