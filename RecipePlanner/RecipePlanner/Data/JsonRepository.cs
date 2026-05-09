using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using RecipePlanner.Core.Interfaces;

namespace RecipePlanner.Data;

public class JsonRepository<T> : IRepository<T> where T : class
{
    private readonly string _filePath;
    private List<T> _items;

    public JsonRepository(string fileName)
    {
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        _items = Load();
    }

    public IEnumerable<T> GetAll() => _items;

    public T GetById(Guid id)
    {
        return _items.Find(x => (Guid)x.GetType().GetProperty("Id")?.GetValue(x) == id);
    }

    public void Add(T item) => _items.Add(item);

    public void Update(T item)
    {
        var id = (Guid)item.GetType().GetProperty("Id")?.GetValue(item);
        Delete(id);
        Add(item);
    }

    public void Delete(Guid id)
    {
        var item = GetById(id);
        if (item != null) _items.Remove(item);
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    private List<T> Load()
    {
        if (!File.Exists(_filePath)) return new List<T>();
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }
}
