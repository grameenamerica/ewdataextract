using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EastWestDataExtract
{
    public class LogFile
    {
        public void writeLog(string source, string message)

        {
            string logTime, messageText,logFilePath;
            string mn, yy;

            logFilePath = @"C:\LogFile\EastWestDataExtract\";
            logTime = System.DateTime.UtcNow.ToString();

            mn = System.DateTime.UtcNow.Month.ToString();
            yy = System.DateTime.UtcNow.Year.ToString();

            logFilePath = logFilePath + "log_" + mn + yy + ".txt";
            messageText = logTime + " (UTC): " + source + ": " + message;
            System.IO.File.AppendAllText(logFilePath, messageText+"\n");

        }
    }
}