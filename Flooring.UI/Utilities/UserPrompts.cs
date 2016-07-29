using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.BLL;
using Flooring.Models;
using Flooring.UI.Workflows;

namespace Flooring.UI.Utilities
{
    public class UserPrompts
    {

        public static DateTime GetdateFromUser(string message)
        {
            do
            {
                Console.Write($"{message} (MM/DD/YYYY): ");
                string input = Console.ReadLine();

                DateTime value;
                if (DateTime.TryParse(input, out value))
                    return value;

                Console.WriteLine("Invalid");
            } while (true);
        }

        public static DateTime? GetdateOrNullFromUser(string message)
        {
            do
            {
                Console.Write($"{message} (MM/DD/YYYY): ");
                string input = Console.ReadLine();

                DateTime value;
                if (input == "")
                {
                    return null;
                }

                if (DateTime.TryParse(input, out value))
                    return value;

                Console.WriteLine("Invalid");
            } while (true);
        }

        public static string GetStringFromUser(string message,bool acceptNull)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            do
            {
                if (input == "" && !acceptNull)
                {
                    Console.WriteLine("Please, enter something");
                }
                else
                {
                    return input;
                }
            } while (true);        
        }

        public static int GetIntFromUser(string message)
        {
            do
            {
                Console.Write(message);
                string input = Console.ReadLine();
             
                int value;
                if (int.TryParse(input, out value))
                    return value;

                Console.WriteLine("That was not a valid number");
            } while (true);
        }

        public static decimal GetDecimalFromUser(string message,bool acceptNull = false)
        {
            do
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (input == "" && acceptNull)
                {
                    return 0;
                }

                decimal value;
                if (decimal.TryParse(input, out value))
                {
                    if (value >= 0)
                    {
                        return value;
                    }
                }
                   
                Console.WriteLine("That was not a valid number");
            } while (true);
        }

        public static void PressKeyForContinue()
        {
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
        }

        public static void PressKeyToReturnToMainMenu()
        {
            Console.WriteLine("Press any key to return to main menu");
            Console.ReadKey();
        }

        public static StateResponse GetStateFromUser(string message,bool acceptNull)
        {
            StateManager manager = new StateManager();
            StateResponse response = new StateResponse();

            do
            {
                Console.Write(message);
                string input = Console.ReadLine().ToUpper();
                if (input == "" && acceptNull)
                {
                    response.Success = true;
                    return response;
                }
                response = manager.GetState(input);
                if (!response.Success)
                {
                    Console.WriteLine(response.Message);
                }
                else
                    break;
            } while (true);        
            return response;
        }

        public static ProductResponse GetProductFromUser(string message, bool acceptNull)
        {     
            ProductManager manager = new ProductManager();
            ProductResponse response = new ProductResponse();

            do
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (input == "" && acceptNull)
                {
                    response.Success = true;
                    return response;
                }
                response = manager.GetProduct(input);
                if (!response.Success)
                {
                    Console.WriteLine(response.Message);
                }
                else
                    break;
            } while (true);
                   
            return response;
        }
    }
}
