using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ColumnObject:TableObject
    {
        public string DataType { get; set; }
        public string IsNullable { get; set; }

        public ColumnObject(string [] valuesFromLine):base(valuesFromLine)
        {
            DataType = valuesFromLine[5];
            IsNullable = valuesFromLine[6];
        }
        public override string ToString()
        {
            return string.Concat(Type, " | ", Name, " | ", Schema, " | ", ParentName, " | ", ParentType, " | ", DataType, " | ", IsNullable);
        }
    }
}
