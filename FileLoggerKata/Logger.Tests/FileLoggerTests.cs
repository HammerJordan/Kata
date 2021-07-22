using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Xunit;
using FluentAssertions;

namespace FileLoggerKata.Logger.Tests
{
    public class FileLoggerTests
    {
        private readonly FileLogger logger;

        public FileLoggerTests()
        {
            logger = new FileLogger();
        }

        public string ReadLastLineInLogFile()
        {
            var line = File.ReadLines(logger.PathToCurrentLogFile).Last();
            if (line.Contains(Environment.NewLine))
                line = line.Replace(Environment.NewLine,"");
            return line;

        }

        [Fact]
        public void CanCallLog()
        {
            logger.Log("My Msg");
        }

        [Fact]
        public void LogLogsFileWithCurrentTimeStamp()
        {
            var time = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
            string timeStamp = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss");

            logger.Log(time);

            string lastLog = ReadLastLineInLogFile();
            string expected = $"{timeStamp} {time}";

            lastLog.Should().BeEquivalentTo(expected);
        }
    }
}