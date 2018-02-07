using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Demo_TheTravelingSalesperson
{
    class CsvServices
    {
        #region FIELDS

        private string _dataFilePath;

        #endregion

        #region METHODS

        public CsvServices(string dataFilePath)
        {
            _dataFilePath = dataFilePath;
        }

        public Salesperson ReadSalespersonFromDataFile()
        {
            Salesperson _salesperson = new Salesperson();
            string salespersonInfo;
            string[] salespersonInfoArray;
            string citiesTraveled;

            //initialize a FileStream object for writing
            FileStream rfileStream = File.OpenRead(DataSettings.dataFilePathCsv);

            //wrap the FieldStream objec in a using statement to ensure of the dispose
            using (rfileStream)
            {
                //wrap the FileStream object in a StreamWriter object to simpligy writing strings
                StreamReader sReader = new StreamReader(rfileStream);

                using (sReader)
                {
                    salespersonInfo = sReader.ReadLine();
                    citiesTraveled = sReader.ReadLine();
                }
            }
            //
            //convert and write data to salesperson object
            //
            salespersonInfoArray = salespersonInfo.Split(',');
            _salesperson.FirstName = salespersonInfoArray[0];
            _salesperson.LastName = salespersonInfoArray[1];
            _salesperson.AccountID = salespersonInfoArray[2];

            if (!Enum.TryParse<Product.ProductType>(salespersonInfoArray[3], out Product.ProductType productType))
            {
                productType = Product.ProductType.None;
            }
            _salesperson.Inventory.Type = productType;

            _salesperson.Inventory.AddProducts(Convert.ToInt32(salespersonInfoArray[4]));
            _salesperson.Inventory.OnBackorder = (Convert.ToBoolean(salespersonInfoArray[5]));

            _salesperson.CitiesVisited = citiesTraveled.Split(',').ToList();

            return _salesperson;

        }

        public void WriteSalespersonToDataFile(Salesperson _salesperson)
        {
            string salespersonData;
            char delineator = ',';

            StringBuilder sb = new StringBuilder();

            //
            //add salesperson and product info to string
            //
            sb.Clear();
            sb.Append(_salesperson.FirstName + delineator);
            sb.Append(_salesperson.LastName + delineator);
            sb.Append(_salesperson.AccountID + delineator);
            sb.Append(_salesperson.Inventory.Type.ToString() + delineator);
            sb.Append(_salesperson.Inventory.NumberOfUnits.ToString() + delineator);
            sb.Append(_salesperson.Inventory.OnBackorder.ToString() + delineator);
            sb.Append(Environment.NewLine);

            //add cities traveled to string
            foreach (string city in _salesperson.CitiesVisited)
            {
                sb.Append(city + delineator);
            }

            //remove the last delineator
            if (sb.Length !=0)
            {
                sb.Length--;
            }

            //convert stringbuilder object to a string
            salespersonData = sb.ToString();

            //initialize a Filestream object for writing
            FileStream wfileStream = File.OpenWrite(DataSettings.dataFilePathCsv);

            //wrap the fieldstream object in a using statemtnt to ensure of the dispose
            using (wfileStream)
            {
                //wrap the filestream objext in a streamwriter object to simplify writing strings
                StreamWriter sWriter = new StreamWriter(wfileStream);

                using (sWriter)
                {
                    sWriter.Write(salespersonData);
                }
            }

        }


        #endregion
    }
}
