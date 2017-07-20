using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapExplorer.Models
{
    public class FunctionModel
    {
        /// <summary>
        /// Name of Function Module
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Function group, to which the function module belongs
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Application to which function module is assigned
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// Remote host (CPIC)
        /// </summary>
        public string RemoteHost { get; set; }

        /// <summary>
        /// Short text for function module
        /// </summary>
        public string ShortText { get;set; }
    }
}
