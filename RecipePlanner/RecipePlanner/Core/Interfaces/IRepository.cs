using System;
using System.Collections.Generic;
using System.Text;
namespace RecipePlanner.Core.Interfaces;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(Guid id);
    void Add(T item);
    void Update(T item);
    void Delete(Guid id);
    void Save();
}