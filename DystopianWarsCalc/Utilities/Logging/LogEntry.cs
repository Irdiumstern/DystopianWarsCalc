using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Utilities.Logging
{
    public class LogEntry
    {
        public DateTime Timestamp { get; } = DateTime.Now;

        public Exception? Exception { get; private set; }

        public string LogText { get; private set; }

        public IList<Object> Items { get; private set; } = new List<Object>();

        public LogEntry(Exception e, string text)
        {
            this.Exception = e;
            this.LogText = text;    
        }

        public LogEntry(string text)
        {
            this.LogText = text;
        }


    }
}
