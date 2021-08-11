using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace VanillaExternalLocations
{
    class ConfigLoader // Read settings from xml file.
    {
        static ConfigLoader instance;

        private ConfigLoader() {}

        public static ConfigLoader GetLoader()
        {
            if (instance == null)
            {
                instance = new ConfigLoader();
                return instance;
            }
            return null;
        }

        public (bool island, bool carrier, bool vespucci, bool paleto) GetActiveLocations(string xmlPath)
        {
            bool island, carrier, vespucci, paleto;
            XPathNavigator nav;
            try
            {
                nav = new XPathDocument(xmlPath).CreateNavigator();
            }
            catch
            {
                return (false, false, false, false);
            }
            island = ReadAttribute(nav, "CayoPerico");
            carrier = ReadAttribute(nav, "AircraftCarrier");
            vespucci = ReadAttribute(nav, "YachtVespucci");
            paleto = ReadAttribute(nav, "YachtPaleto");
            return (island, carrier, vespucci, paleto);
        }

        bool ReadAttribute(XPathNavigator navigator , string iplName)
        {
            bool attribute;
            try
            {
                navigator = navigator.SelectSingleNode("//" + iplName);
                attribute = Convert.ToBoolean(navigator.GetAttribute("active", string.Empty));
                return attribute;
            }
            catch
            {
                return false;
            }
        }
    }
}
