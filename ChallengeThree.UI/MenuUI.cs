using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenges.Console;
using Challenges.Interfaces;
using ChallengeThree.Repository;

namespace ChallengeThree.UI
{
    class MenuUI
    {
        private readonly IConsole _console;
        private readonly BadgesRepository _badgesRepository = new BadgesRepository();

        public MenuUI(IConsole console)
        {
            _console = console;
        }
        public void Run()
        {
            Seed();
            RunMenu();
        }

        private void RunMenu()
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                _console.Clear();
                _console.WriteLine(" Hello, Security Admin..Please enter the number of the option you'd like to select: \n" +
                    "1) Show all badges and access levels \n" +
                    "2) Update doors on an existing badge\n" +
                    "3) Create a new badge \n" +
                    "4) Exit");

                string userInput = _console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        //Show All
                        ShowAll();
                        break;
                    case "2":
                        //update doors on an existing badge
                        UpdateDoors();
                        break;
                    case "3":
                        //create a new badge
                        CreateNewBadge();
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

        private string PrintDoors(Badge badge)
        {
            string newString = "";
            foreach (var str in badge.Doors)
            {
                newString += str;
                newString += "   ";
            }
            return newString;
        }

        private void DisplayItem(Badge badge)
        {
            _console.WriteLine($"Badge Id: {badge.Id} \n" +
                    $"Door Access: {PrintDoors(badge)} \n" +
                     $"-----------------------");
        }

        private void ShowAll()
        {
            _console.Clear();
            //gets all values in dictionary
            var badgeDictionary = _badgesRepository.GetAllBadges();
            Dictionary<Badge, Badge>.ValueCollection badgeCollection = badgeDictionary.Values;

            foreach (var badge in badgeCollection)
            {
                DisplayItem(badge);
            }

            if (badgeDictionary.Count == 0)
                _console.WriteLine("There aren't any badges to be shown at this time");

            //pause program
            _console.WriteLine("Press any key to continue...");
            _console.ReadKey();
        }

        private void UpdateDoors()
        {
            var badge = new Badge();

            //set id
            _console.Clear();
            _console.WriteLine("I need some information to update badge");
            _console.WriteLine("Please enter a valid integer for badge Id");

            string stringId = _console.ReadLine();
            int id;

            if (Int32.TryParse(stringId, out id))
            {
                badge.Id = id;
            }
            else
            {
                _console.WriteLine("Invalid format for Id");
            }
            //end of casting input to id

            //check if key exists
            if (_badgesRepository.KeyExists(badge))
            {
                var badgeValue = _badgesRepository.ReturnValueWithKey(badge);
                _console.WriteLine($"{badge.Id} has access to doors {PrintDoors(badgeValue)}");
                _console.WriteLine(" What would you like to do?  Please enter the number of the option you'd like to select: \n" +
                    "1) Remove a door \n" +
                    "2) Add a door\n" +
                    "3) Remove all doors \n");

                string userInput = _console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        //Remove a door
                        _console.WriteLine("Which door would you like to remove?");
                        string doorToRemove = _console.ReadLine();

                        //returns index of badge.Door of occurence of string, -1 if not found
                        int index = badgeValue.Doors.IndexOf(doorToRemove);

                        if (index > -1)
                        {
                            //create new updated list
                            var newList = badgeValue.Doors;
                            newList.RemoveAt(index);

                            //updates value with new list
                            _badgesRepository.UpdateDoorsOnExistingBadge(badge, newList);
                            _console.WriteLine("Door was removed");
                        }
                        else
                        {
                            _console.WriteLine("You did not enter a valid door to remove");
                        }
                        break;

                    case "2":
                        //add a door
                        _console.WriteLine("Which door would you like to add?");
                        string doorToAdd = _console.ReadLine();

                        //create new updated list
                        var newListToAdd = badge.Doors;
                        newListToAdd.Add(doorToAdd);

                        //updates value with new list
                        _badgesRepository.UpdateDoorsOnExistingBadge(badge, newListToAdd);
                        _console.WriteLine("Door was added");
                        break;

                    case "3":
                        //Remove all doors
                        _badgesRepository.DeleteAllDoorsOnExistingBadge(badge);
                        _console.WriteLine("All doors were deleted");
                        break;
                    default:
                        _console.WriteLine("Invalid format for options, please try again later.");
                        break;
                }//end switch

            }
            //no match found for input
            else
            {
                _console.WriteLine("There was no badge found that corresponds to that Id, please try again later.");
            }

            _console.WriteLine("press any key to continue...");
            _console.ReadKey();

        }//end UpdateDoors();

        private void CreateNewBadge()
        {
            bool validParse = false;
            var badge = new Badge();

            //set id
            _console.Clear();
            _console.WriteLine("I need some information to create badge");
            _console.WriteLine("Please enter a valid integer for badge Id");

            string stringId = _console.ReadLine();
            int id;

            //try to parse input to valid id
            if (Int32.TryParse(stringId, out id))
            {
                badge.Id = id;
                validParse = true;
            }
            else
            {
                _console.WriteLine("Invalid format for Id");
            }

            //check to see if badge exists
            if (_badgesRepository.KeyExists(badge))
            {
                _console.WriteLine("That badge already exists, please try modifying badge instead");

            }
            //create badge 
            else if (!_badgesRepository.KeyExists(badge) && validParse == true)
            {

                //gets all of doors to be added
                bool moreDoors = true;
                List<String> doors = new List<String>();
                while (moreDoors)
                {

                    _console.WriteLine("Please enter a door that it needs access to: ");
                    string door = _console.ReadLine();
                    doors.Add(door);

                    _console.WriteLine("Any other doors it needs access to(y/n)? ");
                    string input = _console.ReadLine();

                    if (input == "y")
                    {
                        moreDoors = true;
                    }
                    else if (input == "n")
                    {
                        moreDoors = false;
                        break;
                    }
                    else
                    {
                        _console.WriteLine("Input wasn't recognized");
                        moreDoors = false;
                        break;
                    }
                }

                //set list to badge field
                badge.Doors = doors;
                //add badge to repository
                _badgesRepository.AddBadge(badge);
                _console.WriteLine("Badge was created");

            }
            else
            {
                //there was no match found, but value for Id couldn't be parsed
                _console.WriteLine("");
            }

            _console.WriteLine("press any key to continue...");
            _console.ReadKey();
        }//end CreateBadge();

        private void Seed()
        {

            var firstItem = new Badge(1, new List<string>() { "a7", "b5" });
            var secondItem = new Badge(2, new List<string>() { "c7", "d5" });

            _badgesRepository.AddBadge(firstItem);
            _badgesRepository.AddBadge(secondItem);
        }
    }
}
