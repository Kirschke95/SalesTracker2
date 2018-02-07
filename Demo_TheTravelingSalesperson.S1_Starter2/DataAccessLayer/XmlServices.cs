using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Demo_TheTravelingSalesperson
{
    class XmlServices
    {
        #region FIELDS

        string _dataFilePath;

        #endregion
        #region METHODS
        public XmlServices(string dataFilePath)
        {
            _dataFilePath = dataFilePath;
        }

        public Salesperson ReadSalespersonFromDataFile()
        {
            Salesperson _salesperson = new Salesperson();

            //initialize a FileStream object for reading
            StreamReader sReader = new StreamReader(_dataFilePath);

            //intialize an XML seriailzier object
            XmlSerializer deserializer = new XmlSerializer(typeof(Salesperson)); 

            using (sReader)
            {
                object xmlObject = deserializer.Deserialize(sReader);
                Console.WriteLine(xmlObject);
                _salesperson = (Salesperson)xmlObject;
            }

            return _salesperson;
        }

        public void WriteSalespersonToDataFile(Salesperson _salesperson)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Salesperson), new XmlRootAttribute("Salesperson"));

            StreamWriter sWriter = new StreamWriter(_dataFilePath);

            using (sWriter)
            {
                serializer.Serialize(sWriter, _salesperson);
            }
        }

        
        #endregion
    }
}
