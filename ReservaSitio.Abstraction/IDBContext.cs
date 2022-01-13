using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSitio.Abstraction
{
    public interface IDBContext<T> : ICrud<T>
    {
    }
}
