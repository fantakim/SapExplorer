using SapExplorer.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SapExplorer.Services
{
    public class DestinationService
    {
        public IList<DestinationModel> GetAllDestinations()
        {
            string config = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            var xmlElement = XElement.Load(config);
            var elements = xmlElement.XPathSelectElements("/SAP.Middleware.Connector/ClientSettings/DestinationConfiguration/destinations/add");
            var destinations = elements.Select(e => new DestinationModel
            {
                Name = (string)e.Attribute("NAME"),
                User = (string)e.Attribute("USER"),
                Password = (string)e.Attribute("PASSWD"),
                Client = (string)e.Attribute("CLIENT"),
                SystemNumber = (string)e.Attribute("SYSNR"),
                AppServerHost = (string)e.Attribute("ASHOST")
            });

            return destinations.ToList();
        }
    }
}