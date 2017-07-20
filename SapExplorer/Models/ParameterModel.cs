using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapExplorer.Models
{
    public class ParameterModel
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public string Direction { get; set; }
        public string DefaultValue { get; set; }
        public bool Optional { get; set; }
        public string Documentation { get; set; }
        public StructureModel Structure { get; set; }
    }
}
