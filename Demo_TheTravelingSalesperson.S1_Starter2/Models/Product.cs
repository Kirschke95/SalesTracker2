using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_TheTravelingSalesperson
{
    public class Product
    {
        public enum ProductType
        {
            None,
            Furry,
            Spotted,
            Dancing
        }


        #region FIELDS

        private static int _numberOfUnits;
        private bool _onBackorder;
        public ProductType _type;

        #endregion

        #region PROPERTIES
        public int NumberOfUnits
        {
            get { return _numberOfUnits; }
            set { _numberOfUnits = value; }
        }

        public bool OnBackorder
        {
            get { return _onBackorder; }
            set { _onBackorder = value; }
        }

        public ProductType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        #endregion
        #region METHODS

        public void AddProducts(int unitsToAdd)
        {
            if (unitsToAdd > NumberOfUnits)
            {
                OnBackorder = false;
            }
            _numberOfUnits += unitsToAdd;
        }

        public void SubtractProducts(int unitsToSubtract)
        {
            if (unitsToSubtract > NumberOfUnits)
            {
                OnBackorder = true;
            }
            
            _numberOfUnits -= unitsToSubtract;
        }


        #endregion
        #region Constructors

        public Product()
        {

        }

        public Product(ProductType type, int numberOfUnits, bool onBackOrder)
        {

        }

        #endregion

    }
}
