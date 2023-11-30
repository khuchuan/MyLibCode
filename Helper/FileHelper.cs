using DTO;
using Serilog;

namespace Helper
{
    public class FileHelper
    {
        public static async Task<bool> CleanWriteToLocalFileAsync(string content, string fileName)
        {
            try
            {
                string path = $"{AppDomain.CurrentDomain.BaseDirectory}{fileName}";
                //check directory existence first
                var directoryName = Path.GetDirectoryName(path);
                if (string.IsNullOrEmpty(directoryName))
                    return false;
                Directory.CreateDirectory(directoryName);

                using StreamWriter w = File.CreateText(path);
                await w.WriteAsync(content);
                return true;
            }
            catch
            {
                throw;
            }
        }
        private static async Task<string> ReadFileAsync(string path)
        {
            try
            {
                return await File.ReadAllTextAsync(path);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static async Task<string> ReadLocalFileAsync(string fileName)
        {
            try
            {
                return await ReadFileAsync($"{AppDomain.CurrentDomain.BaseDirectory}{fileName}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string ReadFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string ReadLocalFile(string fileName)
        {
            try
            {
                return ReadFile($"{AppDomain.CurrentDomain.BaseDirectory}{fileName}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> WriteFileAsync(Stream data, string filePath)
        {
            try
            {
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                await data.CopyToAsync(fileStream);
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "WriteFileAsync");
            }
            return false;
        }

        public static async Task<bool> WriteFileAsync(string data, string filePath)
        {
            try
            {
                using StreamWriter fileStream = File.CreateText(filePath);
                await fileStream.WriteAsync(data);
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "WriteFileAsync");
            }
            return false;
        }
    }
}
