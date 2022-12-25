using DDD.IoC.Helpers;

namespace DDD.IoC.Core
{
    public class Logger
    {

        public static void Info(string message)
        {
            Task.Run(async () => await WriteLogMessage(message, "info"));
        }

        public static void Warn(string message)
        {
            Task.Run(async () => await WriteLogMessage(message, "warn"));
        }

        public static void Error(string message)
        {
            Task.Run(async () => await WriteLogMessage(message, "error"));
        }

        public static void Error(string message, string stack)
        {

            var errorWithStack = message + "\n" + stack;
            Task.Run(async () => await WriteLogMessage(errorWithStack, "error", true));
        }

        public static void Http(string message)
        {
            Task.Run(async () => await WriteLogMessage(message, "http"));
        }

        public static void Custom(string message, string level)
        {
            Task.Run(async () => await WriteLogMessage(message, level));
        }

        private static async Task WriteLogMessage(string message, string logLevel, bool containsErrorStack = false)
        {
            var logTime = DateHelper.CurrentLogDate;
            var log = $"{logTime} {logLevel} - {message}";

            var color = logLevel switch
            {
                "info" => ConsoleColor.DarkGreen,
                "debug" => ConsoleColor.White,
                "warn" => ConsoleColor.Yellow,
                "http" => ConsoleColor.Magenta,
                "error" => ConsoleColor.Red,
                _ => ConsoleColor.White,
            };

            Console.ForegroundColor = color;
            Console.WriteLine(log);

            try
            {
                var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                var directoryPath = Path.Combine(logDirectory, logLevel);

                if (FileSystem.MkDir(directoryPath))
                {
                    var fileName = $"log_{DateHelper.CurrentLogDateFS}.txt";
                    var filePath = Path.Combine(directoryPath, fileName);

                    var formatLog = $"{logTime} - {message}";

                    await FileSystem.WriteSync(formatLog, filePath);
                }
            }
            catch { }
        }
    }
}
