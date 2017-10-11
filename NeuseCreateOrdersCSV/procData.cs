using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQtoCSV;

namespace NeuseCreateOrdersCSV
{
    class ProcData
    {
        [CsvColumn(Name = "OrderID", FieldIndex = 1)]
        public string OrderID { get; set; }

        [CsvColumn(Name= "OrderName", FieldIndex = 2)]
        public string OrderName { get; set; }
    }
}
