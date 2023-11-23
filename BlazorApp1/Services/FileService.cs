using BlazorApp1.Models;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BlazorApp1.Services
{
    public interface IFileService
    {
        string ReadFromFile();
        void SaveToFile(List<ToDoItem> toDoItems);
    }

    public class FileService : IFileService
    {
        private readonly IConfiguration _config;

        public FileService(IConfiguration config)
        {
            _config = config;
        }

        public string ReadFromFile()
        {
            return File.ReadAllText(_config["SampleDataFile"]);
        }

        // Luu danh sach vao file moi khi co thay doi
        public void SaveToFile(List<ToDoItem> toDoItems)
        {
            string json = JsonConvert.SerializeObject(toDoItems);
            File.WriteAllText(_config["SampleDataFile"], json);
        }


    }
}
