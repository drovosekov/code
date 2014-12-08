using System;
using System.Diagnostics.Contracts;

namespace CSVImportParser
{
    public class Fields : IEquatable<Fields> 
    {
        public string HeaderText { get; set; }
        public string RealDBName { get; set; }
        public bool Require { get; set; }
        public int ColumnIndex { get; set; }

        public static Fields CreateNewField(string Header, string Name, bool req, int index)
        {
            Contract.Requires<ArgumentException>(string.IsNullOrEmpty(Header), "Необходимо заполнить Header (class Fields)");
            Contract.Requires<ArgumentException>(string.IsNullOrEmpty(Name), "Необходимо заполнить Name (class Fields)");

            var c = new Fields
            {
                HeaderText = Header,
                RealDBName = Name,
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