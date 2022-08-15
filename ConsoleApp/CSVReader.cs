using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class CSVReader
    {
        public List<string> ImportLines(string path)
        {
            var importedLines = new List<string>();
            try
            {
                using (var streamReader = new StreamReader(path))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var line = streamReader.ReadLine();
                        importedLines.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Problem with reading data from CSV file: {e.Message}");
                return null;
            }
            return importedLines;

        }
        public List<DatabaseObject> CreateListOfImportedObjects(List<string> importedLines)
        {
            var importedObjects = new List<DatabaseObject>();
            for (int i = 0; i < importedLines.Count; i++)
            {
                var importedLine = importedLines[i];
                if (importedLine.Length < 1)
                    continue;
                int semicolonFreq = importedLine.Count(ch => ch == ';');
                if (semicolonFreq < 6) importedLine += ';';
                var values = importedLine.Split(';');
                var importedObject = DatabaseObject.BuildObject(values);
                if (importedObject != null)
                    importedObjects.Add(importedObject);
            }
            return importedObjects;
        }
    }
}
