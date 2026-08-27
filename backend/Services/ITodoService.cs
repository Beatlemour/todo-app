using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoService
{
    IEnumerable<TodoItem> GetAll();
    TodoItem Add(string title);
    bool Delete(int id);
}