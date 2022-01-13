using System;
using System.Collections.Generic;
using ReservaSitio.Abstraction;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ReservaSitio.DataAccess
{
    public class ApiDbContext<T> : IDBContext<T> where T : class, IEntity
    {
        DbSet<T> _items;
        ApiDbContext _ctx;
        public ApiDbContext(ApiDbContext ctx)  
        {
            _ctx = ctx;
            _items = ctx.Set<T>();
        }

        public void Delete(int id)
        {
            
        }

        public IList<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(int id)
        {
            return _items.FirstOrDefault(e => e.seguc_iid.Equals(id));
        }

        public T Save(T entity)
        {
            _items.Add(entity);
            _ctx.SaveChanges();
            return entity;
        }
    }
}
