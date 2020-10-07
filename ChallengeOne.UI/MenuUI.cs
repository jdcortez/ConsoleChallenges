using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeOne.Repository;
using Challenges.Console;
using Challenges.Interfaces;



namespace ChallengeOne.UI
{
    class MenuUI
    {
        private readonly IConsole _console;
        private readonly MenuRepository _menuRepository = new MenuRepository();
        public MenuUI(IConsole console)
        {
            _console = console;
        }
        public void Run()
        {
            SeedContent();
            RunMenu();
        }

        private void RunMenu()
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                _console.Clear();
                _console.WriteLine("Enter the number of the option you'd like to select: \n" +
                    "1) Show all items on menu \n" +
                    "2) Add new menu item \n" +
                    "3) Delete a menu item \n" +
                    "4) Exit");

                string userInput = _console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        //Show All
                        ShowAll();
                        break;
                    case "2":
                        //add a new menu item
                        AddMenuItem();
                        break;
                    case "3":
                        //delete a menu item
                        DeleteMenuItem();
                        break;
                    case "4":
                        //Exit
                        continueToRun = false;
                        break;
                    default:
                        //default
                        _console.WriteLine("Please enter a valid number between 1 and 4. \n" +
                            "Press any key to continue......");
                        _console.ReadKey();
                        break;
                }
            }

        }//end RunMenu()

        private void DisplayItem(MenuItem item)
        {
            _console.WriteLine($"{item.Id} \n" +
                    $"Title: {item.Name} \n" +
                    $"Description: {item.Description} \n" +
                    $"Price: ${item.Price} \n" +
                    $"The 3 main ingredients are : {item.Ingredients[0]}, {item.Ingredients[1]}, {item.Ingredients[2]} \n" +
                    $"-----------------------");
        }

        private void ShowAll()
        {
            _console.Clear();
            var menu = _menuRepository.GetAllMenuItems();

            foreach (var item in menu)
            {
                DisplayItem(item);
            }

            //pause program
            _console.WriteLine("Press any key to continue...");
            _console.ReadKey();
        }

        private void AddMenuItem()
        {
            var menuItem = new MenuItem();
            //sets id to count +1
            var count = _menuRepository.GetAllMenuItems().Count;
            menuItem.Id = count + 1;

            _console.WriteLine("Claim saved for later, press any key to continue...");
            _console.ReadKey();

            _console.WriteLine("Please enter the description for the menu item");
            string description = _console.ReadLine();
            menuItem.Description = description;

            _console.WriteLine("Please enter the price for the menu item in valid decimal format, excluding $ sign or commas");
            string price = _console.ReadLine();
            double cost;

            if (Double.TryParse(price, out cost))
            {
                menuItem.Price = cost;
            }
            else
            {
                Console.WriteLine("invalid format for cost was set");
            }

            _console.WriteLine("Please list the 3 main ingredients and press enter after each entry");
            string firstIngredient = _console.ReadLine();
            string secondIngredient = _console.ReadLine();
            string thirdIngredient = _console.ReadLine();

            menuItem.Ingredients.Add(firstIngredient);
            menuItem.Ingredients.Add(secondIngredient);
            menuItem.Ingredients.Add(thirdIngredient);

            _menuRepository.AddMenuItem(menuItem);
        }

        private void DeleteMenuItem()
        {
            _console.WriteLine("Which menu item would you like to delete, please enter the id");
            var menu = _menuRepository.GetAllMenuItems();

            //print menu
            int count = 0;
            foreach(var item in menu)
            {
                count++;
                _console.WriteLine($"{count}) {item.Name}");
            }

            //get and validate user input
            int id;
            bool wasParsed = int.TryParse(_console.ReadLine(), out id);
            if (wasParsed)
            {
                int correctIndex = id - 1;
                if(correctIndex >= 0 && correctIndex < menu.Count)
                {
                    var menuItem = menu[correctIndex];
                    _menuRepository.RemoveMenuItem(menuItem);
                }
            }
            else
            {
                _console.WriteLine("You selected an invalid option.");
            }
            _console.WriteLine("Please press any key to continue..");
            _console.ReadKey();
        }

        private void SeedContent()
        {
            List<string> ingredients = new List<string>{"water", "flour", "sugar"};
            var firstItem = new MenuItem(1, "Pepperoni Pizza", "New York Style Pizza", ingredients, 32.0);
            var secondItem = new MenuItem(2, "corn on the cob", "grown here in Indiana", ingredients, 2.34);
            var thirdItem = new MenuItem(3, "popcorn", "movie theatre", ingredients, 10);
            var fourthItem = new MenuItem(4, "tacos", "chicken tacos", ingredients, 23.43);
            var fifthItem = new MenuItem(5, "salad", "house", ingredients, 12.30);

            _menuRepository.AddMenuItem(firstItem);
            _menuRepository.AddMenuItem(secondItem);
            _menuRepository.AddMenuItem(thirdItem);
            _menuRepository.AddMenuItem(fourthItem);
            _menuRepository.AddMenuItem(fifthItem);

        }





    }//end class



}



