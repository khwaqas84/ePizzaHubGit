using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Implementation
{
    public class Service<T> : IService<T> where T : class
    {
        protected IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }
        public void Add(T entity)
        {
           _repository.Add(entity);
            _repository.SaveChanges();
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
            _repository.SaveChanges();
        }

        public T Find(object id)
        {
           return _repository.GetById(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
            _repository.SaveChanges();
        }
    }
}
