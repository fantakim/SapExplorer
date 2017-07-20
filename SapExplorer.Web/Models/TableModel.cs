using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SapExplorer.Web.Models
{
    public class TableModel
    {
        public string Name { get; set; }
        public DataTable Table { get; set; }
    }
}