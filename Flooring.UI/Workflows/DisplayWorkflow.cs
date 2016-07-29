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
    public class DisplayWorkflow : IWorkflow
    {
        private List<Order> _currentOrders;
        private DateTime date;

        public void Execute()
        {
            DateTime date = UserPrompts.GetdateFromUser("Please provide order date: ");
            Console.WriteLine();

            DisplayOrderInformation(date);
        }

        private void DisplayOrderInformation(DateTime date)
        {
            var manager = new OrderManager();
            var result = manager.DisplayAllOrders(date);

            Console.WriteLine();
            if (result.Success)
            {
                _currentOrders = result.OrderList;
                DisplayScreen.PrintOrdersDetails(_currentOrders);
                UserPrompts.PressKeyToReturnToMainMenu();
                Console.ReadKey();
            }

            else
            {
                DisplayScreen.WorkflowErrorScreen(result.Message);
            }
        }
    }
}
