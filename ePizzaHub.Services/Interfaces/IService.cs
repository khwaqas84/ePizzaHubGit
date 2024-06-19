using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Interfaces
{
    public interface IService<T>where T : class
    {
        IEnumerable<T> GetAll();
        T Find(object id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
