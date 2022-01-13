using System;
using System.Collections.Generic;

namespace ReservaSitio.Abstraction
{
    public interface ICrud<T>
    {
        T Save(T entity);
        IList<T> GetAll();
        T GetById(int id);
        void Delete(int id);

    }
}
