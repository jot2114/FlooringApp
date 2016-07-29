using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.UI.Workflows
{
    public class MainMenu
    {
        public void Execute()
        {
            string input = "";

            do
            {
                Console.Clear();
                Console.WriteLine("WELCOME TO Flooring!");
                Console.WriteLine("--------------------");
                Console.WriteLine("1. Display Order");
                Console.WriteLine("2. Add Order");
                Console.WriteLine("3. Edit Order");
                Console.WriteLine("4. Remove Order");

                Console.WriteLine();
                Console.WriteLine("(Q) to Quit");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Enter Choice: ");

                input = Console.ReadLine();

                if (input.ToUpper() != "Q")
                {
                    ProcessChoice(input);
                }

            } while (input.ToUpper() != "Q");
        }

        private void ProcessChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    DisplayWorkflow display = new DisplayWorkflow();
                    display.Execute();
                    break;
                case "2":
                    AddWorkflow add = new AddWorkflow();
                    add.Execute();
                    break;
                case "3":
                    EditWorkflow create = new EditWorkflow();
                    create.Execute();
                    break;
                case "4":
                    RemoveWorkflow remove = new RemoveWorkflow();
                    remove.Execute();
                    break;
                default:
                    Console.WriteLine("{0} is not valid!", choice);
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}

