namespace Mx.Hyperway.Sml
{
    using System.Collections.Generic;

    public class SmlDirectory
    {
        public SmlDirectory()
        {
            this.SmpRecords = new List<SmpRecord>();
        }
        public List<SmpRecord> SmpRecords { get; }
    }
}