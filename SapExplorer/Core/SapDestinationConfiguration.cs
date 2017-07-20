using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapExplorer.Core
{
    public class SapDestinationConfiguration : IDestinationConfiguration
    {
        #pragma warning disable 0067
        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;
        #pragma warning restore 0067

        public bool ChangeEventsSupported()
        {
            throw new NotImplementedException();
        }

        public RfcConfigParameters GetParameters(string destinationName)
        {
            throw new NotImplementedException();
        }
    }
}
