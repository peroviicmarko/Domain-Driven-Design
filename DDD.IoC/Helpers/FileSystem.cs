namespace DDD.IoC.Helpers
{
    public class FileSystem
    {
        public static async Task WriteSync(string message, string filePath)
        {
            using StreamWriter writer = new(filePath, true);
            await writer.WriteLineAsync(message);
            writer.Close();
        }

        public static void Write(string message, string filePath)
        {
            using StreamWriter writer = new(filePath, true);
            writer.WriteLine(message);
            writer.Close();
        }

        public static bool MkDir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }

            if (Directory.Exists(path))
            {
                return true;
            }

            return false;
        }
    }
}
