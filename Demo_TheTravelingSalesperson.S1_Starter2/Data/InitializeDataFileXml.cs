using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_TheTravelingSalesperson
{
    class InitializeDataFileXml
    {
        private Salesperson InitializeSalesperson()
        {
            Salesperson _salesperson = new Salesperson()
            {
                FirstName = "Bonzo",
                LastName = "Regan",
                AccountID = "banana103",
                Inventory = new Product(Product.ProductType.Furry, 20, false),
                CitiesVisited = new List<string>()
                {
                    "Detroit",
                    "Grand Rapids",
                    "Ann Arbor"
                }
            };

            return _salesperson;
        }

        private void SeedDataFile()
        {
            //XmlServices xmlService = new XmlServices(DataSettings.dataFilePathXml);
            //xmlservice.WriteSalesPersonToDataFile(InitializeSalesperson());

        }
    }
}
