using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_TheTravelingSalesperson
{
    /// <summary>
    /// MVC View class
    /// </summary>
    public class ConsoleView
    {
        #region FIELDS
        private int maxAttempts = 3;
        private int maxBuySellAmount;
        private int minBuySellAmount;
        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// default constructor to create the console view objects
        /// </summary>
        public ConsoleView()
        {
            InitializeConsole();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initialize all console settings
        /// </summary>
        private void InitializeConsole()
        {
            ConsoleUtil.WindowTitle = "Laughing Leaf Productions";
            ConsoleUtil.HeaderText = "The Traveling Salesperson Application";
        }
        /// <summary>
        /// display the Continue prompt
        /// </summary>

        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            ConsoleUtil.DisplayMessage("");

            Console.CursorVisible = true;
        }
        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>

        public void DisplayExitPrompt()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Thank you for using the application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }
        /// <summary>
        /// display the welcome screen
        /// </summary>

        public void DisplayWelcomeScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Written by Tyler Kirschke");
            ConsoleUtil.DisplayMessage("Northwestern Michigan College");
            ConsoleUtil.DisplayMessage("");

            sb.Clear();
            sb.AppendFormat("You are a traveling salesperson buying and selling widgets ");
            sb.AppendFormat("around the country. You will be prompted regarding which city ");
            sb.AppendFormat("you wish to travel to and will then be asked whether you wish to buy ");
            sb.AppendFormat("or sell widgets.");
            ConsoleUtil.DisplayMessage(sb.ToString());
            ConsoleUtil.DisplayMessage("");

            sb.Clear();
            sb.AppendFormat("Your first task will be to set up your account details.");
            ConsoleUtil.DisplayMessage(sb.ToString());

            DisplayContinuePrompt();
        }
        /// <summary>
        /// setup the new salesperson object with the initial data
        /// Note: To maintain the pattern of only the Controller changing the data this method should
        ///       return a Salesperson object with the initial data to the controller. For simplicity in 
        ///       this demo, the ConsoleView object is allowed to access the Salesperson object's properties.
        /// </summary>

        public Salesperson DisplaySetupAccount()
        {
            Salesperson _salesperson = new Salesperson();

            ConsoleUtil.HeaderText = "Account Setup";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayPromptMessage("First Name: ");
            _salesperson.FirstName = UppercaseFirst(Console.ReadLine());
            ConsoleUtil.DisplayPromptMessage("Last Name: ");
            _salesperson.LastName = UppercaseFirst(Console.ReadLine());
            ConsoleUtil.DisplayPromptMessage("Account ID: ");
            _salesperson.AccountID = Console.ReadLine();
            if (!ConsoleValidator.TryGetIntegerFromUser(18, 100, 3, "Age", out int userInteger))
            {
                Console.WriteLine("Looks like you are having trouble with your age. Clearly you are old as dirt, setting age to 100.");
                _salesperson.Age = 100;
            }
            _salesperson.Age = userInteger;
            string userResponse = ConsoleValidator.GetYesNoFromUser(3, "Are you a hockey fan?", out bool maxAttemptsExceeded);
            if (maxAttemptsExceeded)
            {
                Console.WriteLine("Not valid answer. You must not be a hockey fan.");
            }
            if (userResponse == "yes")
            {
                _salesperson.IsHockeyFan = true;
            }
            else
            {
                _salesperson.IsHockeyFan = false;
            }

            ConsoleUtil.DisplayMessage("Product Types");
            ConsoleUtil.DisplayMessage("");

            //
            // list all product types
            //
            foreach (string productTypeName in Enum.GetNames(typeof(Product.ProductType)))
            {
                //
                // do not display the "NONE" enum value
                //
                if (productTypeName != Product.ProductType.None.ToString())
                {
                    ConsoleUtil.DisplayMessage(productTypeName);
                }

            }

            //
            // get product type, if invalid entry, set type to "None"
            //
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayPromptMessage("Enter the product type: ");
            Product.ProductType productType;
            if (Enum.TryParse<Product.ProductType>(UppercaseFirst(Console.ReadLine()), out productType))
            {
                _salesperson.Inventory.Type = productType;
            }
            else
            {
                _salesperson.Inventory.Type = Product.ProductType.None;
            }


            if (ConsoleValidator.TryGetIntegerFromUser(0, 100, 3, "units", out int numberOfUnits))
            {
                _salesperson.Inventory.AddProducts(numberOfUnits);
            }
            else
            {
                ConsoleUtil.DisplayMessage("You did not enter a valid number of units.");
                ConsoleUtil.DisplayMessage("We will set your intentory to 0");
            }

            _salesperson.Inventory.NumberOfUnits = numberOfUnits;

            ConsoleUtil.DisplayReset();
            ConsoleUtil.DisplayMessage("Your account is now setup");

            return _salesperson;
        }
        /// <summary>
        /// display a closing screen when the user quits the application
        /// </summary>

        public void DisplayClosingScreen()
        {
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thank you for using The Traveling Salesperson Application.");

            DisplayContinuePrompt();
        }
        /// <summary>
        /// get the menu choice from the user
        /// </summary>

        public MenuOption DisplayGetUserMenuChoice()
        {
            MenuOption userMenuChoice = MenuOption.None;
            bool usingMenu = true;

            ConsoleUtil.HeaderText = "Main Menu";
            ConsoleUtil.DisplayReset();

            while (usingMenu)
            {
                //
                // set up display area
                //
                ConsoleUtil.DisplayReset();
                Console.CursorVisible = false;

                //
                // display the menu
                //
                ConsoleUtil.DisplayMessage("Please type the number of your menu choice.");
                ConsoleUtil.DisplayMessage("");
                Console.Write(
                    "\t" + "1. Setup Account" + Environment.NewLine +
                    "\t" + "2. Update Account Info" + Environment.NewLine +
                    "\t" + "3. Travel" + Environment.NewLine +
                    "\t" + "4. Display Cities" + Environment.NewLine +
                    "\t" + "5. Display Account Info" + Environment.NewLine +
                    "\t" + "6. Buy" + Environment.NewLine +
                    "\t" + "7. Sell" + Environment.NewLine +
                    "\t" + "8. Display Inventory" + Environment.NewLine +
                    "\t" + "9. Save Account Info" + Environment.NewLine +
                    "\t" + "0. Load Account Info" + Environment.NewLine +
                    "\t" + "E. Exit" + Environment.NewLine);


                //
                // get and process the user's response
                // note: ReadKey argument set to "true" disables the echoing of the key press
                //
                ConsoleKeyInfo userResponse = Console.ReadKey(true);
                switch (userResponse.KeyChar)
                {
                    case '1':
                        userMenuChoice = MenuOption.SetupAccount;
                        usingMenu = false;
                        break;
                    case '2':
                        userMenuChoice = MenuOption.UpdateAccountInfo;
                        usingMenu = false;
                        break;
                    case '3':
                        userMenuChoice = MenuOption.Travel;
                        usingMenu = false;
                        break;
                    case '4':
                        userMenuChoice = MenuOption.DisplayCities;
                        usingMenu = false;
                        break;
                    case '5':
                        userMenuChoice = MenuOption.DisplayAccountInfo;
                        usingMenu = false;
                        break;
                    case '6':
                        userMenuChoice = MenuOption.Buy;
                        usingMenu = false;
                        break;
                    case '7':
                        userMenuChoice = MenuOption.Sell;
                        usingMenu = false;
                        break;
                    case '8':
                        userMenuChoice = MenuOption.DisplayInventory;
                        usingMenu = false;
                        break;
                    case '9':
                        userMenuChoice = MenuOption.SaveAccountInfo;
                        usingMenu = false;
                        break;
                    case '0':
                        userMenuChoice = MenuOption.LoadAccountInfo;
                        usingMenu = false;
                        break;
                    case 'E':
                    case 'e':
                        userMenuChoice = MenuOption.Exit;
                        usingMenu = false;
                        break;
                    default:
                        ConsoleUtil.DisplayMessage(
                            "It appears you have selected an incorrect choice." + Environment.NewLine +
                            "Press any key to continue or the ESC key to quit the application.");

                        userResponse = Console.ReadKey(true);
                        if (userResponse.Key == ConsoleKey.Escape)
                        {
                            usingMenu = false;
                        }
                        break;
                }
            }
            Console.CursorVisible = true;

            return userMenuChoice;
        }

        public Salesperson DisplayUpdateAccountInfo(Salesperson _salesperson)
        {
            ConsoleUtil.HeaderText = "Updating Account Info";
            ConsoleUtil.DisplayReset();
            

            ConsoleUtil.DisplayMessage("Re-enter a value if you must update. If the info doesn't have to be changed," +
                "\nsimply press the Enter key.\n");

            ConsoleUtil.DisplayPromptMessage($"Current first name: {_salesperson.FirstName}." +
                $"\n New first name: ");

            ConsoleKeyInfo userResponse = Console.ReadKey(true);

            if (userResponse.Key != ConsoleKey.Enter)
            {
                _salesperson.FirstName = Console.ReadLine();
            }           

            ConsoleUtil.DisplayPromptMessage($"Current last name: {_salesperson.LastName}." +
                $"\n New last name: ");

            if (userResponse.Key != ConsoleKey.Enter)
            {
                _salesperson.LastName = Console.ReadLine();
            }

            ConsoleUtil.DisplayPromptMessage($"Current account ID: {_salesperson.AccountID}." +
                $"\n New account ID: ");

            if (userResponse.Key != ConsoleKey.Enter)
            {
                _salesperson.AccountID = Console.ReadLine();
            }

            ConsoleUtil.DisplayPromptMessage($"Current age: {_salesperson.Age}");

            if (userResponse.Key != ConsoleKey.Enter)
            {
                if (ConsoleValidator.TryGetIntegerFromUser(18, 100, 3, "your age", out int userInteger))
                {
                    Console.WriteLine("Incorrect answer, you must be old. Age set to 100.");
                    _salesperson.Age = 100;
                }
                _salesperson.Age = userInteger;
            }
                
            return _salesperson;
        }

        /// <summary>
        /// tells user of successful load of account and travel info
        /// </summary>
        /// <param name="_salesperson"></param>
        public void DisplayConfirmLoadAccountInfo(Salesperson _salesperson)
        {
            ConsoleUtil.HeaderText = "Load Account";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Account information has been successfully loaded\n");

            DisplayAccountDetails(_salesperson);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// tells user of successful save of account and travel info
        /// </summary>
        /// <param name="_salesperson"></param>
        public void DisplayConfirmSaveAccountInfo(Salesperson _salesperson)
        {
            ConsoleUtil.HeaderText = "Save Account";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Account information has been successfully saved\n");

            DisplayAccountDetails(_salesperson);

            DisplayContinuePrompt();
        }

        public bool DisplayLoadAccountInfo(out bool maxAttemptsExceeded)
        {
            maxAttemptsExceeded = false;
            string userResponse;

            ConsoleUtil.HeaderText = "Load Account";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("");
            userResponse = ConsoleValidator.GetYesNoFromUser(maxAttempts, "Load account info?", out maxAttemptsExceeded);

            if (maxAttemptsExceeded)
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficult, we will return you" +
                    "to the main menu");
            }
            else
            {
                maxAttemptsExceeded = true;
                return userResponse == "yes" ? true : false;
            }

            return maxAttemptsExceeded;

        }

        public bool DisplayLoadAccountInfo(Salesperson _salesperson, out bool maxAttemptsExceeded)
        {
            maxAttemptsExceeded = false;
            string userResponse;

            ConsoleUtil.HeaderText = "Load Account";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("");
            userResponse = ConsoleValidator.GetYesNoFromUser(maxAttempts, "Load account info?", out maxAttemptsExceeded);

            if (maxAttemptsExceeded)
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficult, we will return you" +
                    "to the main menu");
            }
            else
            {
                return userResponse == "yes" ? true : false;
            }

            return maxAttemptsExceeded;
        }

        public bool DisplaySaveAccountInfo(Salesperson _salesperson, out bool maxAttemptsExceeded)
        {
            maxAttemptsExceeded = false;
            string userResponse;

            ConsoleUtil.HeaderText = "Save Account";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("The current account info.");
            DisplayAccountInfo(_salesperson);

            ConsoleUtil.DisplayMessage("");
            userResponse = ConsoleValidator.GetYesNoFromUser(maxAttempts, "Save account info?", out maxAttemptsExceeded);

            if (maxAttemptsExceeded)
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficult, we will return you" +
                    "to the main menu");
                return false;
            }
            else
            {
                return userResponse == "yes" ? true : false;
            }


        }

        /// <summary>
        /// get the next city to travel to from the user
        /// </summary>
        /// <returns>string City</returns>

        public string DisplayGetNextCity()
        {
            string nextCity = "";

            ConsoleUtil.DisplayPromptMessage("Enter next city: ");
            nextCity = UppercaseFirst(Console.ReadLine());

            return nextCity;
        }
        /// <summary>
        /// display a list of the cities traveled
        /// </summary>

        public void DisplayCitiesTraveled(Salesperson salesperson)
        {
            ConsoleUtil.HeaderText = "Cities Traveled";
            ConsoleUtil.DisplayReset();

            foreach (string city in salesperson.CitiesVisited)
            {
                ConsoleUtil.DisplayMessage(city);
            }

            DisplayContinuePrompt();
        }
        /// <summary>
        /// display the current account information
        /// </summary>

        public void DisplayAccountInfo(Salesperson salesperson)
        {
            ConsoleUtil.HeaderText = "Account Info";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("First Name: " + salesperson.FirstName);
            ConsoleUtil.DisplayMessage("Last Name: " + salesperson.LastName);
            ConsoleUtil.DisplayMessage("Account ID: " + salesperson.AccountID);
            ConsoleUtil.DisplayMessage("Product Type: " + salesperson.Inventory.Type);
            ConsoleUtil.DisplayMessage("Current Inventory: " + salesperson.Inventory.NumberOfUnits);
            ConsoleUtil.DisplayMessage("Age: " + salesperson.Age);
            ConsoleUtil.DisplayMessage("Cities traveled:");

            foreach (string city in salesperson.CitiesVisited)
            {
                ConsoleUtil.DisplayMessage(city);
            }        

            if (salesperson.IsHockeyFan)
            {
                ConsoleUtil.DisplayMessage("Is a hockey fan, NICE.");
            }
            else
            {
                ConsoleUtil.DisplayMessage("Is not a hockey fan, LAME.");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display current account details w/out continue prompt
        /// </summary>
        /// <param name="salesperson"></param>
        public void DisplayAccountDetails(Salesperson salesperson)
        {
            ConsoleUtil.DisplayMessage("First Name: " + salesperson.FirstName);
            ConsoleUtil.DisplayMessage("Last Name: " + salesperson.LastName);
            ConsoleUtil.DisplayMessage("Account ID: " + salesperson.AccountID);
            ConsoleUtil.DisplayMessage("Product Type: " + salesperson.Inventory.Type);
            ConsoleUtil.DisplayMessage("Current Inventory: " + salesperson.Inventory.NumberOfUnits);
            ConsoleUtil.DisplayMessage("Age:" + salesperson.Age);
            ConsoleUtil.DisplayMessage("Cities traveled:");

            foreach (string city in salesperson.CitiesVisited)
            {
                ConsoleUtil.DisplayMessage(city);
            }

            if (salesperson.IsHockeyFan)
            {
                ConsoleUtil.DisplayMessage("Is a hockey fan, NICE.");
            }
            else
            {
                ConsoleUtil.DisplayMessage("Is not a hockey fan, LAME.");
            }
        }
        /// <summary>
        /// display backorder notification
        /// </summary>
        /// <param name="_salesperson"></param>
        /// <param name="Inventory"></param>

        public void DisplayBackOrderNotification(Salesperson _salesperson, int Inventory)
        {
            ConsoleUtil.DisplayMessage($"You have {Math.Abs(Inventory)} units on backorder. You should buy more.");
            ConsoleUtil.DisplayMessage("");
        }
        /// <summary>
        /// get the number of units to purchase from user
        /// </summary>
        /// <param name="_salesperson"></param>
        /// <returns></returns>

        public int DisplayGetNumberOfUnitsToBuy(Salesperson _salesperson)
        {
            ConsoleUtil.HeaderText = "Buy Inventory";
            ConsoleUtil.DisplayReset();

            if (!ConsoleValidator.TryGetIntegerFromUser(0, 100, 3, "products", out int numberOfUnitsToBuy))
            {
                ConsoleUtil.DisplayMessage("You are entering invalid numbers of products to purchase.");
                ConsoleUtil.DisplayMessage("We will buy 0 products.");
                numberOfUnitsToBuy = 0;
                DisplayContinuePrompt();
            }

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("You have purchased " + numberOfUnitsToBuy + " " + _salesperson.Inventory.Type.ToString() + " units");

            DisplayContinuePrompt();

            return numberOfUnitsToBuy;
        }
        /// <summary>
        /// get the number of units to sell from the user
        /// </summary>
        /// <param name="_salesperson"></param>
        /// <returns></returns>

        public int DisplayGetNumberOfUnitsToSell(Salesperson _salesperson)
        {
            ConsoleUtil.HeaderText = "Sell Inventory";
            ConsoleUtil.DisplayReset();

            if (!ConsoleValidator.TryGetIntegerFromUser(0, 100, 3, "products", out int numberOfUnitsToSell))
            {
                ConsoleUtil.DisplayMessage("You are entering invalid numbers of products to sell.");
                ConsoleUtil.DisplayMessage("We will sell 0 products.");
                numberOfUnitsToSell = 0;
                DisplayContinuePrompt();
            }

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("You have sold  " + numberOfUnitsToSell + " " + _salesperson.Inventory.Type.ToString() + " units");

            DisplayContinuePrompt();

            return numberOfUnitsToSell;
        }
        /// <summary>
        /// display the inventory information to the user
        /// </summary>
        /// <param name="_salesperson"></param>

        public void DisplayInventory(Salesperson _salesperson)
        {
            ConsoleUtil.HeaderText = "Current Inventory";
            ConsoleUtil.DisplayReset();

            if (_salesperson.Inventory.OnBackorder == true)
            {
                DisplayBackOrderNotification(_salesperson, _salesperson.Inventory.NumberOfUnits);
            }

            ConsoleUtil.DisplayMessage($"Current Inventory: {_salesperson.Inventory.NumberOfUnits} {_salesperson.Inventory.Type} units.");

            DisplayContinuePrompt();
        }
        /// <summary>
        /// method to take a string and make the first letter a capital letter
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>

        static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return s.First().ToString().ToUpper() + s.Substring(1);
        }
        #endregion
    }
}
