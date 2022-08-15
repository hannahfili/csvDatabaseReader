using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class DatabaseObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int NumberOfChildren { get; set; } // sprawdzic czy to nie powininen byc integer (ze wzgledu na calkowitosc)
        // ale moze double jest ze wzgledu na wieksza pojemnosc danych

        public DatabaseObject(string [] values)
        {
            Type = values[0];
            Name = values[1];
        }
        public override string ToString()
        {
            return string.Concat(Type, " | ", Name, " | " , NumberOfChildren);
        }
        public static DatabaseObject BuildObject(string[] values)
        {
            if (values[0].ToUpper() == "DATABASE") return new DatabaseObject(values);
            else if (values[0].ToUpper() == "TABLE") return new TableObject(values);
            else if (values[0].ToUpper() == "COLUMN") return new ColumnObject(values);
            return null;
        }
    }
}
