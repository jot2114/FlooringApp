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
    public class RemoveWorkflow
    {
        public void Execute()
        {
            var manager = new OrderManager();
            
            DateTime dateByUser = UserPrompts.GetdateFromUser("Enter the date of order ");
            int orderToRemove = UserPrompts.GetIntFromUser("\nEnter the order number you want to delete ");

            var result = manager.DisplayOrderByID(orderToRemove, dateByUser);

            DisplayScreen.PrintOrderDetails(result.OrderList[0]);

            Console.Write("Are you sure you want to commit (Y/N)");
            string commit = Console.ReadLine().ToUpper();

            if (commit == "Y")
            {
                 result = manager.DeleteOrder(orderToRemove, dateByUser);

                Console.WriteLine(result.Message);
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

















