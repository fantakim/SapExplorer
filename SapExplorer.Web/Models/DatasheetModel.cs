using SapExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SapExplorer.Web.Models
{
    public class DatasheetModel
    {
        public string SelectedDestination { get; set; }
        public string SearchFunction { get; set; }
        public IList<DestinationModel> Destinations { get; set; }
        public IList<ParameterModel> Parameters { get; set; }
    }
}