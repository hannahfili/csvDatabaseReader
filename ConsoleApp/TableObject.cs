using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class TableObject:DatabaseObject
    {

        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }


        public TableObject(string [] valuesFromLine):base(valuesFromLine)
        {
            Schema = valuesFromLine[2];
            ParentName = valuesFromLine[3];
            ParentType = valuesFromLine[4];
        }

        public override string ToString()
        {
            return string.Concat(base.ToString(), " | ", Schema, " | ", ParentName, " | ", ParentType);
        }
    }

    
}
