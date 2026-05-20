using System;
using System.Collections.Generic;
using System.Text;

namespace RecipePlanner.Domain.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();

        void SaveAll(List<T> items);
    }
}
