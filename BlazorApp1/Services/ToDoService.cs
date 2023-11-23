using BlazorApp1.Models;
using Newtonsoft.Json;

namespace BlazorApp1.Services
{
    public interface IToDoService
    {
        List<ToDoItem> Get();
        ToDoItem Get(Guid ID);
        List<ToDoItem> Add(ToDoItem toDoItem);
        List<ToDoItem> Toggle(Guid id);
        List<ToDoItem> Delete(Guid ID);

    }

    public class ToDoService: IToDoService
    {
        private readonly IFileService _fileService;
        private List<ToDoItem> _toDoItems;

        public ToDoService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public List<ToDoItem> Get()
        {
            string json = _fileService.ReadFromFile();
            _toDoItems = JsonConvert.DeserializeObject<List<ToDoItem>>(json);
            return _toDoItems;
        }

        public ToDoItem Get(Guid ID)
        {
            return _toDoItems.First(t => t.ID == ID);
        }

        public List<ToDoItem> Add (ToDoItem item)
        {
            _toDoItems.Add(item);
            _fileService.SaveToFile(_toDoItems);
            return _toDoItems;
        }


        public List<ToDoItem> Toggle(Guid ID)
        {
            var toDoItemToUpdate = Get(ID);

            if (toDoItemToUpdate != null)
            {
                toDoItemToUpdate.IsComplete = !toDoItemToUpdate.IsComplete;
                _fileService.SaveToFile(_toDoItems);
            }

            return _toDoItems;
        }

        public List<ToDoItem> Delete(Guid ID)
        {
            var toDoItemToRemove = Get(ID);

            if (toDoItemToRemove != null)
            {
                _toDoItems.Remove(Get(ID));
                _fileService.SaveToFile(_toDoItems);
            }

            return _toDoItems;
        }

    }
}
