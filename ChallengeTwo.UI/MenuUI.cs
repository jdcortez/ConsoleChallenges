using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeTwo.Repository;
using Challenges.Interfaces;

namespace ChallengeTwo.UI
{
    class MenuUI
    {
        private readonly IConsole _console;
        private readonly ClaimsRepository _claimsRepository = new ClaimsRepository();

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
                _console.WriteLine("Enter the number of the option you'd like to select: \n" +
                    "1) See all Claims \n" +
                    "2) Take Care of Next Claim\n" +
                    "3) Enter a new claim \n" +
                    "4) Exit");

                string userInput = _console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        //Show All
                        ShowAll();
                        break;
                    case "2":
                        //take care of next claim
                        TakeCareOfNextClaim();
                        break;
                    case "3":
                        //enter a new claim
                        CreateNewClaim();
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


        private void DisplayItem(Claim claim)
        {
            _console.WriteLine($"Claim Id: {claim.Id} \n" +
                    $"Type: {claim.TypeOfClaim} \n" +
                    $"Description: {claim.Description} \n" +
                    $"Price: ${claim.ClaimAmount} \n" +
                    $"Date of Incident: {claim.DateOfIncident} \n" +
                    $"Date of Claim: {claim.DateOfClaim} \n" +
                    $"IsValid: {claim.IsValid} \n" +
                    $"-----------------------");
        }

        private void ShowAll()
        {
            _console.Clear();
            var claimList = _claimsRepository.GetAllClaims();

            foreach (var claim in claimList)
            {
                DisplayItem(claim);
            }

            if (_claimsRepository.GetCount() == 0)
                _console.WriteLine("There aren't any claims to be shown at this time");

            //pause program
            _console.WriteLine("Press any key to continue...");
            _console.ReadKey();
        }


        //method gives user option to view claim at head of queue, then dequeue the claim if they wish 
        private void TakeCareOfNextClaim()
        {
            //if the queue is empty
            if (_claimsRepository.GetCount() < 1)
            {
                _console.WriteLine("There are no claims to be dealt with at this time");
                //pause program
                _console.WriteLine("Press any key to continue...");
                _console.ReadKey();
            }
            else
            {
                _console.WriteLine("Here are the details for the next claim to be handled:");
                _console.WriteLine("");

                var nextClaim = _claimsRepository.GetNextClaim();
                DisplayItem(nextClaim);

                _console.WriteLine("Do you want to deal with this claim now(y/n)?");
                var input = _console.ReadLine();

                if (input == "y")
                {
                    _claimsRepository.DeleteNextClaim();
                    _console.WriteLine("Thank you for completing the claim, press any key to continue...");
                    _console.ReadKey();
                }
                else if (input == "n")
                {
                    _console.WriteLine("Claim saved for later, press any key to continue...");
                    _console.ReadKey();
                }
                else
                {
                    _console.WriteLine("Your response was not understood, press any key to continue...");
                    _console.ReadKey();
                }

            }//end else
        }//end TakeCareOfNextClaim()

        private void CreateNewClaim()
        {
            //bool will switch to false if user enters invalid input
            var createClaim = true;
            while (createClaim)
            {
                var claim = new Claim();

                //set id
                _console.Clear();
                _console.WriteLine("I need some information to complete this claim.");
                _console.WriteLine("Please enter a valid integer for Claim Id");

                string stringId = _console.ReadLine();
                int id;

                if (Int32.TryParse(stringId, out id))
                {
                    claim.Id = id;
                }
                else
                {
                    _console.WriteLine("Invalid format for Id was set, please try again later.");
                    createClaim = false;
                    break;
                }
                
                //set claim type
                _console.WriteLine("Enter one claim type... car, home, or theft");
                string claimType = _console.ReadLine();

                switch (claimType)
                {
                    case "car":
                        claim.TypeOfClaim = ClaimType.Car;
                        break;
                    case "home":
                        claim.TypeOfClaim = ClaimType.Home;
                        break;
                    case "theft":
                        claim.TypeOfClaim = ClaimType.Theft;
                        break;
                    default:
                        _console.WriteLine("Invalid format for claim type was set, please try again later.");
                        createClaim = false;
                        break;
                }
                //set description
                _console.WriteLine("Enter a description of the claim");
                claim.Description = _console.ReadLine();

                //set ClaimAmount
                _console.WriteLine("Please enter the price for the claim in valid decimal format, excluding $ sign or commas");
                string price = _console.ReadLine();
                double cost;

                if (Double.TryParse(price, out cost))
                {
                    claim.ClaimAmount = cost;
                }
                else
                {
                    _console.WriteLine("Invalid format for claim type was set, please try again later.");
                    createClaim = false;
                    break;
                }

                //set DateOfIncident
                _console.WriteLine("Please enter the date of incident in the format of mm/dd/yyyy");
                string incidentDate = _console.ReadLine();

                try
                {
                    var dateTime = DateTime.Parse(incidentDate);
                    claim.DateOfIncident = dateTime;
                }
                catch (FormatException)
                {
                    _console.WriteLine("Invalid format for date of incident was entered, please try again later.");
                    createClaim = false;
                    break;
                }

                //set DateOfClaim
                _console.WriteLine("Please enter the date of claim in the format of mm/dd/yyyy");
                string claimDate = _console.ReadLine();

                try
                {
                    var dateTime = DateTime.Parse(claimDate);
                    claim.DateOfClaim = dateTime;
                }
                catch (FormatException)
                {
                    _console.WriteLine("Invalid format for date of claim was entered, please try again later.");
                    createClaim = false;
                    break;
                }

                //set IsValid
                var timespan = claim.DateOfClaim - claim.DateOfIncident;
                var result = timespan.Days < 31 ? true : false;
                claim.IsValid = result;

                //claim was successful, add to queue
                _console.WriteLine("Claim was successfully created.");
                _claimsRepository.EnqueueClaim(claim);
                createClaim = false;
            }//end while

            _console.WriteLine("press any key to continue...");
            _console.ReadKey();
        
        }//end CreateNewClaimMethod

        private void Seed()
        {
            var dateTime = DateTime.Parse("01/20/1993");
            var dateTime2 = DateTime.Parse("03/20/1993");

            var firstItem = new Claim(1, ClaimType.Home, "burglary occurred", 3000.00, dateTime, dateTime2);
            var secondItem = new Claim(2, ClaimType.Theft, "person got pickpocketed", 123.00, dateTime, dateTime2);
            var thirdItem = new Claim(3, ClaimType.Car, "window smashed", 500.00, dateTime, dateTime2);

            _claimsRepository.EnqueueClaim(firstItem);
            _claimsRepository.EnqueueClaim(secondItem);
            _claimsRepository.EnqueueClaim(thirdItem);
        }





    }
}
