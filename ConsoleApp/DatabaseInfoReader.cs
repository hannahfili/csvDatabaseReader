namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class DatabaseInfoReader
    {
        IEnumerable<DatabaseObject> ImportedObjects;
        
        IEnumerable<DatabaseObject> Databases;
        IEnumerable<TableObject> Tables;
        IEnumerable<ColumnObject> Columns;

        private CSVReader csvReader;

        public DatabaseInfoReader()
        {
            csvReader = new CSVReader();
        }

        public void ImportData(string fileToImport)
        {
            var importedLines = csvReader.ImportLines(fileToImport);
            ImportedObjects = csvReader.CreateListOfImportedObjects(importedLines);
            CleanUpObjectsProperties();

            Databases = ImportedObjects.Where(obj => obj.Type == "DATABASE");
            Tables = ImportedObjects.Where(obj => obj.Type == "TABLE").Cast<TableObject>();
            Columns = ImportedObjects.Where(obj => obj.Type == "COLUMN").Cast<ColumnObject>();

            AssignNumberOfChildrenToObjects(Tables, Columns);
        }
        public void PrintData()
        {
            foreach (var database in Databases)
            {
                Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");

                var databaseTables = Tables.Where(obj => obj.ParentType == database.Type && obj.ParentName == database.Name);

                foreach (TableObject table in databaseTables)
                {
                    Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

                    var tableColumns = Columns.Where(obj => obj.ParentType == table.Type && obj.ParentName == table.Name);

                    foreach (var column in tableColumns)
                    {
                        Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
                    }

                }
            }

            Console.ReadLine();
        }
        
        
        private void CleanUpObjectsProperties()
        {
            foreach (var importedObject in ImportedObjects)
            {
                importedObject.Type = importedObject.Type.Trim().Replace(" ", "").ToUpper();
                importedObject.Name = importedObject.Name.Trim().Replace(" ", "");
                if (importedObject is TableObject)
                {
                    TableObject impO = (TableObject)importedObject;
                    impO.Schema = impO.Schema.Trim().Replace(" ", "");
                    impO.ParentName = impO.ParentName.Trim().Replace(" ", "");
                    impO.ParentType = impO.ParentType.Trim().Replace(" ", "").ToUpper();
                }
                if (importedObject is ColumnObject)
                {
                    ColumnObject impO = (ColumnObject)importedObject;
                    impO.DataType = impO.DataType.Trim().Replace(" ", "");
                    impO.IsNullable = impO.IsNullable.Trim().Replace(" ", "");
                }
            }
        }
        private void AssignNumberOfChildrenToObjects(IEnumerable<TableObject> tables, IEnumerable<ColumnObject> columns)
        {
            foreach (var elem in ImportedObjects)
            {
                if (elem.Type == "DATABASE")
                {
                    elem.NumberOfChildren = tables
                    .Where(
                    obj => obj.ParentType == elem.Type
                    && obj.ParentName == elem.Name
                    ).Count();
                }
                else if (elem.Type == "TABLE")
                {
                    elem.NumberOfChildren = columns
                    .Where(
                    obj => obj.ParentType == elem.Type
                    && obj.ParentName == elem.Name
                    ).Count();
                }
                else continue;
            }
        }
    }


}

