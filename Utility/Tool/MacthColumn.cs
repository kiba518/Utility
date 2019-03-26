using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class MacthColumn
    {
        public string ColumnName { get; set; }
        public string ColumnNameCn { get; set; }
        public string ColumnType { get; set; }
        public int RequiredFields { get; set; }
    }
}
