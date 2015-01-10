using System;
using System.Diagnostics.Contracts;

namespace CSVImportParser
{
    public class Fields : IEquatable<Fields> 
    {
        public string HeaderText { get; set; }
        public string FieldNameOfClassForSerelization { get; set; }
        public bool Require { get; set; }
        public int ColumnIndex { get; set; }

        public static Fields CreateNewField(string header, string name, bool req, int index)
        {
            Contract.Requires<ArgumentException>(string.IsNullOrEmpty(header), "Необходимо заполнить Header (class Fields)");
            Contract.Requires<ArgumentException>(string.IsNullOrEmpty(name), "Необходимо заполнить Name (class Fields)");

            var c = new Fields
            {
                HeaderText = header,
                FieldNameOfClassForSerelization = name,
                Require = req,
                ColumnIndex = index
            };

            return c;
        }

        public override String ToString()
        {
            return HeaderText;
        }

        public bool Equals(Fields other)
        {
            return HeaderText == other.HeaderText;
        }

        
    }
} 