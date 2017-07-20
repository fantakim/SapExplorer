using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapExplorer.Models
{
    public class FieldModel
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int Length { get; set; }
        public int Decimals { get; set; }
        public string Documentation { get; set; }
    }
}
