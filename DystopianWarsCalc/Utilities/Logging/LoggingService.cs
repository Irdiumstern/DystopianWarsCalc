using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Utilities.Logging
{
    public static class LoggingService
    {
        private static List<LogEntry> logEntries = new List<LogEntry>();
        public static IReadOnlyList<LogEntry> Logs
        {
            get
            {
                return logEntries;
            }
        }

        public static LogEntry AddException(Exception e, string logText)
        {
            var log = new LogEntry(e, logText);
            logEntries.Add(log);
            return log;
        }

        public static LogEntry AddLog(string logText)
        {
            var log = new LogEntry(logText);
            logEntries.Add(log);
            return log;

        }

    }
}
