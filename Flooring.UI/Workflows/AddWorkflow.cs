using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.BLL;
using Flooring.Models;
using Flooring.UI.Utilities;

namespace Flooring.UI.Workflows
{
    public class AddWorkflow : IWorkflow
    {
        public void Execute()
        {
            Order newOrder = new Order();
            var manager = new OrderManager();

            DateTime dateByUser = UserPrompts.GetdateFromUser("Enter the date of order: ");
            newOrder.OrderDate = dateByUser;
            newOrder.CustomerName = UserPrompts.GetStringFromUser("\nEnter the name of the customer: ",false);
            newOrder.Area = UserPrompts.GetDecimalFromUser("\nEnter the area: ");
            newOrder.State = UserPrompts.GetStateFromUser("\nEnter the state Abbreviation (OH,PA,MI,IN): ",false ).StateList.FirstOrDefault();
            newOrder.ProductOrdered = UserPrompts.GetProductFromUser("\nEnter the product type (Carpet,Laminate,Tile,Wood): ",false).ProductList.FirstOrDefault();        
            newOrder = manager.CalculateOrder(newOrder);
            DisplayScreen.PrintOrderDetails(newOrder);
            UserPrompts.PressKeyForContinue();

            Console.WriteLine("\nAre you sure you want to commit(Y/N)");
            string commit = Console.ReadLine().ToUpper();
            if (commit == "Y")
            {
                manager.AddOrder(newOrder, dateByUser);
                UserPrompts.PressKeyToReturnToMainMenu();
                Console.ReadKey();
            }
            else
            {
                UserPrompts.PressKeyToReturnToMainMenu();
                Console.ReadKey();
            }
        }
    }
}
