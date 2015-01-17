using System;
using System.IO;
using System.Runtime.Serialization;

namespace SelfUpdateApp.TypeUpdates
{
    public class MSIUpdate : ServerUpdateFile 
    {
        public MSIUpdate()
            : this(string.Empty)
        {
        }
        public MSIUpdate(string fullFileName)
            : base(fullFileName)
        {
        }

        public override DateTime FileCreationDateTime 
        {
            get { return File.GetCreationTime(FullFileName); }
            set { throw new NotImplementedException(); }
        }

        public override string Hash
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}