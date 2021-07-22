using System;
using System.IO;
using System.Net.Mime;

namespace FileLoggerKata.Logger
{
    public class FileLogger
    {
        private readonly string pathToLogFile = @"C:\Users\Hammer\Desktop\MyProjects\C#\Kata\FileLoggerKata\Logs";
        private readonly string logFile = "log.txt";

        public string PathToCurrentLogFile => Path.Join(pathToLogFile, logFile);
        
        public void Log(string message)
        {
            string loggedMessage = $"{GetTimeStamp()} {message}\n";
            
            File.AppendAllText(PathToCurrentLogFile,loggedMessage);
        }

        private string GetTimeStamp()
        {
            string timeStamp = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss");
            return timeStamp;
        }
    }
}