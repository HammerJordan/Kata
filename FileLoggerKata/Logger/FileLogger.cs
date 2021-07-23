using System;
using System.IO;
using System.Net.Mime;

namespace FileLoggerKata.Logger
{
    public class FileLogger
    {
        private readonly string pathToLogFile = @"C:\Users\Hammer\Desktop\MyProjects\C#\Kata\FileLoggerKata\Logs";

        public string PathToCurrentLogFile => Path.Join(pathToLogFile, $"{GetDateStamp()}.txt");
        public string LogDirectory => pathToLogFile;
        
        
        public void Log(string message)
        {
            string loggedMessage = $"{GetDateTimeStamp()} {message}\n";
            
            File.AppendAllText(PathToCurrentLogFile,loggedMessage);
        }

        private string GetDateTimeStamp()
        {
            string timeStamp = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss");
            return timeStamp;
        }

        private string GetDateStamp()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
    }
}