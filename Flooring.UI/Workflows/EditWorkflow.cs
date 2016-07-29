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
    public class EditWorkflow : IWorkflow
    {
        public void Execute()
        {
            var manager = new OrderManager();
            Order newOrder = new Order();

            DateTime date = UserPrompts.GetdateFromUser("Enter the date of the order which you want to edit ");
            int editOrderNumber = UserPrompts.GetIntFromUser("\nEnter the order number you want to edit ");
            var response = manager.DisplayOrderByID(editOrderNumber, date);

            if (response.Success)
            {
                DisplayScreen.PrintOrderDetails(response.OrderList[0]);

                DateTime? newDate = UserPrompts.GetdateOrNullFromUser("\nEnter the date, or nothing to keep original(" + date +  ") ");
                if (newDate == null)
                {
                    newOrder.OrderDate = response.OrderList[0].OrderDate;
                }
                else
                {
                    newOrder.OrderDate = newDate.Value;
                }

                string newName = UserPrompts.GetStringFromUser("\nEnter new name of the customer, or nothing to keep original(" +
                                                                                  response.OrderList[0].CustomerName + ") ",true);
                if (string.IsNullOrEmpty(newName))
                {
                    newOrder.CustomerName = response.OrderList[0].CustomerName;
                }
                else
                {
                    newOrder.CustomerName = newName;
                }

                ProductResponse productResponse = UserPrompts.GetProductFromUser("\nEnter new  product type, or nothing to keep actual value(" +
                                                                  response.OrderList[0].ProductOrdered.ProductType + ") ",true);
                if (productResponse.Success)
                {
                    if (productResponse.ProductList == null)
                    {
                        newOrder.ProductOrdered = response.OrderList[0].ProductOrdered;
                    }
                    else
                    {
                        newOrder.ProductOrdered = productResponse.ProductList[0];
                    }         
                }

                else
                {
                    Console.WriteLine(productResponse.Message);
                }

                StateResponse stateResponse = UserPrompts.GetStateFromUser("\nEnter new  state abbreviation, or nothing to keep actual value(" +
                                                   response.OrderList[0].State.StateAbbreviation + ") ",true);
                if (stateResponse.Success)
                {
                    if (stateResponse.StateList == null)
                    {
                        newOrder.State = response.OrderList[0].State;
                    }
                    else
                    {
                        newOrder.State = stateResponse.StateList[0];
                    }    
                }
                else
                {
                    Console.WriteLine(stateResponse.Message);
                }

                decimal area = UserPrompts.GetDecimalFromUser("\nEnter new Area, or nothing to keep actual value (" + response.OrderList[0].Area + ") ",true);
            
                if (area == 0)
                {
                    newOrder.Area = response.OrderList[0].Area;
                }
                else
                {
                    newOrder.Area = area;
                }

                newOrder = manager.CalculateOrder(newOrder);

                DisplayScreen.PrintOrderDetails(newOrder);

                Console.WriteLine("\nAre you sure you want to commit(Y/N)");
                string commit = Console.ReadLine().ToUpper();
                if (commit == "Y")
                {           
                    manager.UpdateOrder(newOrder,editOrderNumber,date);
                                     
                    Console.Clear();
                    Console.WriteLine("Order has been updated");
                }
                else
                {
                    Console.WriteLine("No changes in original order");
                }
                UserPrompts.PressKeyForContinue();
            }
            else
                DisplayScreen.WorkflowErrorScreen(response.Message);

            Console.WriteLine();
        }
    }
}
