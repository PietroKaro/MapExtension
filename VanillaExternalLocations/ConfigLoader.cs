using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanillaExternalLocations
{
    class ConfigLoader
    {
        static ConfigLoader instance;

        private ConfigLoader() {}

        public ConfigLoader GetLoader()
        {
            if (instance == null)
            {
                instance = new ConfigLoader();
                return instance;
            }
            return null;
        }
    }
}
