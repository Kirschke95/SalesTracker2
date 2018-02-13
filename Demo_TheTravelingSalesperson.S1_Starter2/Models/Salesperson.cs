using System.Collections.Generic;

namespace Demo_TheTravelingSalesperson
{
    /// <summary>
    /// Salesperson MVC Model class
    /// </summary>
    public class Salesperson
    {
        #region FIELDS

        private string _firstName;
        private string _lastName;
        private string _accountID;
        private List<string> _citiesVisited;
        private Product _inventory;
        private int _age;
        private bool _isHockeyFan;

        public bool IsHockeyFan
        {
            get { return _isHockeyFan; }
            set { _isHockeyFan = value; }
        }


        #endregion

        #region PROPERTIES

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string AccountID
        {
            get { return _accountID; }
            set { _accountID = value; }
        }
      
        public List<string> CitiesVisited
        {
            get { return _citiesVisited; }
            set { _citiesVisited = value; }
        }

        public Product Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Salesperson()
        {
            _citiesVisited = new List<string>();
            _inventory = new Product();
       
        }

        public Salesperson(string firstName, string lastName, string acountID)
        {
            _firstName = firstName;
            _lastName = lastName;
            _accountID = acountID;

            _citiesVisited = new List<string>();
            _inventory = new Product();
            
        }

        #endregion
        
        #region METHODS



        #endregion
    }
}
