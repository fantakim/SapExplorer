using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapExplorer.Models
{
    public class StructureModel
    {
        public string Name { get; set; }
        public IEnumerable<FieldModel> Fields { get; private set; }

        public StructureModel(string name, IEnumerable<FieldModel> fields)
        {
            this.Name = name;
            this.Fields = fields;
        }
    }
}
