using System;

namespace ExcelToOutlook.Model
{
    public class GridData
    {
        public int Row;
        public string Subject { get; set; }
        public DateTime RawDate { get; set; } = new DateTime();
        public string Date { get { return RawDate.ToLongDateString(); } }
    }
}
