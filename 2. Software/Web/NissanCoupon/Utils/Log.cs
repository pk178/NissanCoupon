using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Utils
{
    public class Log
    {
        private static string GetLogPath(string LogfileName)
        {
            return Path.Combine(Environment.CurrentDirectory,
                        string.Format("Log\\{0}-{1}.txt", LogfileName, DateTime.Now.ToString("yy.MM.dd")));
        }

        public static void LogError(string FunctionName, string Message, string Exception)
        {
            try
            {
                string MessageToLog = string.Empty;

                MessageToLog += String.Format("Time: {0}\r\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                MessageToLog += String.Format("FunctionName: {0}\r\n", FunctionName);
                MessageToLog += String.Format("Message: {0}\r\n", Message);
                MessageToLog += String.Format("Exeption: {0}\r\n", Exception);
                MessageToLog += "--------------------------------------------------------------------------------------------------------------\r\n";

                using (StreamWriter f = new StreamWriter(GetLogPath("LogError"), true))
                {
                    f.WriteLine(MessageToLog);
                    f.Close();
                }
            }
            catch
            {

            }
        }

        public static void LogEvent(string FunctionName, string EventInfo)
        {
            try
            {
                string MessageToLog = string.Empty;

                MessageToLog += String.Format("Time: {0}\r\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                MessageToLog += String.Format("FunctionName: {0}\r\n", FunctionName);
                MessageToLog += String.Format("Message: {0}\r\n", EventInfo);
                MessageToLog += "--------------------------------------------------------------------------------------------------------------\r\n";

                using (StreamWriter f = new StreamWriter(GetLogPath("LogEvent"), true))
                {
                    f.WriteLine(MessageToLog);
                    f.Close();
                }
            }
            catch
            {

            }
        }
    }
}
