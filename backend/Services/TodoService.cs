using System.Collections.Concurrent;
using TodoApi.Models;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly ConcurrentDictionary<int, TodoItem> _items = new();
    private int _nextId = 1;

    public IEnumerable<TodoItem> GetAll() =>
        _items.Values.OrderBy(t => t.Id);

    public TodoItem Add(string title)
    {
        var id = Interlocked.Increment(ref _nextId) - 1;
        var item = new TodoItem { Id = id, Title = title };
        _items[id] = item;
        return item;
    }

    public bool Delete(int id) => _items.TryRemove(id, out _);
}