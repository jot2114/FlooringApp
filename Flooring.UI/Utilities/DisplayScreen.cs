using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.BLL;
using Flooring.Models;

namespace Flooring.UI.Utilities
{
    public class DisplayScreen
    {
        public static void WorkflowErrorScreen(string message)
        {
            Console.WriteLine("An error occured. {0}", message);
            UserPrompts.PressKeyForContinue();
        }

        public static void PrintOrderDetails(Order order)
        {
            Console.WriteLine("\nOrder Information");
            Console.WriteLine("========================");
            Console.WriteLine($"Order Date {order.OrderDate:d}");
            Console.WriteLine($"Customer Name {order.CustomerName}");
            Console.WriteLine($"State Name: {order.State.StateAbbreviation}");
            Console.WriteLine($"Product Type: {order.ProductOrdered.ProductType}");
            Console.WriteLine($"Area: {order.Area}");
            Console.WriteLine($"CostPerSqFoot: {order.ProductOrdered.CostPerSquareFoot:c}");
            Console.WriteLine($"LaborCostPerSqFoot: {order.ProductOrdered.LaborCostPerSquareFoot:c}");
            Console.WriteLine($"MaterialCost: {order.MaterialCost:c}");
            Console.WriteLine($"LaborCost: {order.LaborCost:c}");        
            Console.WriteLine($"TaxCost: {order.TaxCost:c}");
            Console.WriteLine($"Total: {order.Total:c}");
            Console.WriteLine();
        }


        public static void PrintOrdersDetails(List<Order> orders)
        {
            foreach (var order in orders)
            {
                Console.WriteLine("Order Information");
                Console.WriteLine("==================");
                Console.WriteLine($"Order Number {order.OrderId:d}");
                Console.WriteLine($"Customer Name  {order.CustomerName}");
                Console.WriteLine($"State Name: {order.State.StateAbbreviation}");
                Console.WriteLine($"Product Type: {order.ProductOrdered.ProductType}");
                Console.WriteLine($"Area: {order.Area}");
                Console.WriteLine($"CostPerSqFoot: {order.ProductOrdered.CostPerSquareFoot:c}");
                Console.WriteLine($"LaborCostPerSqFoot: {order.ProductOrdered.LaborCostPerSquareFoot:c}");
                Console.WriteLine($"MaterialCost: {order.MaterialCost:c}");
                Console.WriteLine($"LaborCost: {order.LaborCost:c}");
                Console.WriteLine($"TaxCost: {order.TaxCost:c}");
                Console.WriteLine($"Total: {order.Total:c}");

                Console.WriteLine();
            }
        }
    }
}
